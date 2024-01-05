using System;

namespace de.Aargenveldt.SbomTest.CLI
{
    /// <summary>
    /// Interface für Klassen, die einen Fehlertext und ggf. Exception Informationen ausgeben können.
    /// </summary>
    public interface IErrorWriter
    {
        /// <summary>
        /// Ausgabe eines Fehlertexts.
        /// </summary>
        /// <param name="message">Meldungstext</param>
        /// <param name="exception">Optional: Exceptionobjekt, das Detailinformationen für die Ausgabe liefern
        /// soll; darf <see langword="null"/> sein; Standardwert ist <see langword="null"/></param>
        void WriteError(string message, Exception exception = null);
    }
}
