using System;
using System.Drawing;

using Console = Colorful.Console;

namespace de.Aargenveldt.SbomTest.CLI.Impl
{
    /// <summary>
    /// Benutzerinteraktion erwarten - Hinweistext auf der Konsole ausgeben, Betätigung der ENTER Taste abwarten.
    /// </summary>
    internal class ConsoleWaitForUser : IWaitForUser
    {
        /// <inheritdoc/>
        /// <remarks>
        ///     <para>
        ///         Ausgabe des Hinweistexts erfolgt auf der Konsole;
        ///         es wird auf die Betätigung der ENTER Taste (Standardeingabeknala, STDIN)
        ///         der Konsole gewartet.
        ///     </para>
        /// </remarks>
        public void AwaitUserAction(bool showPrompt, bool prependNewline = true)
        {
            if (true == prependNewline)
                Console.WriteLine();

            if (true == showPrompt)
                Console.Write("Press <ENTER> to exit...", Color.LightGray);

            while (Console.ReadKey(false).Key != ConsoleKey.Enter) { }
            return;
        }
    }
}
