using de.Aargenveldt.SbomTest.BusinessLayer;
using de.Aargenveldt.SbomTest.TypeInspection;
using System;
using System.Threading.Tasks;

namespace de.Aargenveldt.SbomTest.CLI.Impl
{
    /// <summary>
    /// Anwendungsklasse (implementiert den Programablauf).
    /// </summary>
    internal class App : IApp
    {
        /// <summary>
        /// Verarbeitungsobjekt für die Geschäftslogik
        /// </summary>
        private readonly IBusinessLogic _businessLogic;

        /// <summary>
        /// Ausgabeobjet für ein Banner (=Anwendungstitel)
        /// </summary>
        private readonly IBannerWriter _bannerWriter;

        /// <summary>
        /// Ausgabeobjekt für die Ergebnisse
        /// </summary>
        private readonly IProcessingResultsWriter<AssemblyInfo> _processingResultsWriter;

        /// <summary>
        /// Hilfsobjekt zur Erwartung einer Benutzerinteraktion.
        /// </summary>
        private readonly IWaitForUser _waitForUser;

        /// <summary>
        /// Hilfsobjekt zur Ausgabe von Fehlermeldungen.
        /// </summary>
        private readonly IErrorWriter _errorWriter;


        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="businessLogic">Verarbeitungsobjekt für die Geschäftslogik</param>
        /// <param name="bannerWriter">Ausgabeobjekt für ein Banner (=Anwendungstitel); darf <see langword="null"/> sein</param>
        /// <param name="processingResultsWriter">Ausgabeobjekt für die Ergebnisse</param>
        /// <param name="errorWriter">Hilfsobjekt zur Ausgabe von Fehlermeldungen.</param>
        /// <param name="waitForUser">Hilfsobjekt zur Erwartung einer Benutzerinteraktion.</param>
        /// <exception cref="ArgumentNullException">
        ///     <list type="bullet">
        ///         <item><description><paramref name="businessLogic"/> ist <see langword="null"/></description></item>
        ///         <item><description><paramref name="processingResultsWriter"/> ist <see langword="null"/></description></item>
        ///         <item><description><paramref name="errorWriter"/> ist <see langword="null"/></description></item>
        ///         <item><description><paramref name="waitForUser"/> ist <see langword="null"/></description></item>
        ///     </list>
        /// </exception>
        public App(
            IBusinessLogic businessLogic,
            IBannerWriter bannerWriter,
            IProcessingResultsWriter<AssemblyInfo> processingResultsWriter,
            IErrorWriter errorWriter,
            IWaitForUser waitForUser)
        {
            this._businessLogic = businessLogic ?? throw new ArgumentNullException(nameof(businessLogic));
            this._bannerWriter = bannerWriter;
            this._processingResultsWriter = processingResultsWriter ?? throw new ArgumentNullException(nameof(processingResultsWriter));
            this._errorWriter = errorWriter ?? throw new ArgumentNullException(nameof(errorWriter));
            this._waitForUser = waitForUser ?? throw new ArgumentNullException(nameof(waitForUser));

            return;
        }

        /// <inheritdoc/>
        public async Task Run()
        {
            try
            {
                this.ShowBanner();

                IProcessingResults<AssemblyInfo> processingResults = await this._businessLogic.DoSomeWork();
                this.ShowProcessingResults(processingResults);

                this.WaitForUser(true);
            }
            catch (Exception ex)
            {
                this._errorWriter?.WriteError("Execution failed:", ex);
                this.WaitForUser(true);
            }

            return;
        }

        /// <summary>
        /// Banner (Titel) der Anwendung auf der Konsole ausgeben.
        /// </summary>
        private void ShowBanner()
        {
            this._bannerWriter?.WriteBanner(showVersion: true, prependNewline: true, appendNewline: true);
            return;
        }

        /// <summary>
        /// Gibt die Ergebnisse eines Verarbeitungslaufs aus.
        /// </summary>
        /// <param name="processingResults">Ergebnisobjekt</param>
        private void ShowProcessingResults(IProcessingResults<AssemblyInfo> processingResults)
        {
            this._processingResultsWriter?.WriteProcessingResults(processingResults);
            return;
        }

        /// <summary>
        /// Auf die Betätigung der ENTER Taste warten; optional einen Hinweistext ausgeben.
        /// </summary>
        /// <param name="showPrompt">Hinweistext ausgeben Ja/Nein</param>
        /// <param name="prependNewline">Optional: Eine führende Leerzeile ausgeben; Standardwert ist <see langword="true"/></param>
        private void WaitForUser(bool showPrompt, bool prependNewline = true)
        {
            this._waitForUser?.AwaitUserAction(showPrompt: showPrompt, prependNewline: prependNewline);
            return;
        }

    }
}
