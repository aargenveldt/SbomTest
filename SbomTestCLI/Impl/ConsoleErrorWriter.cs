using System;
using System.Drawing;

using Console = Colorful.Console;

namespace de.Aargenveldt.SbomTest.CLI.Impl
{
    /// <summary>
    /// Ausgabe eines Fehlertexts auf der Konsole.
    /// </summary>
    internal class ConsoleErrorWriter : IErrorWriter
    {
        /// <inheritdoc/>
        /// <remarks>
        ///     <para>
        ///         Die Ausgabe erfolgt auf der Konsole.
        ///     </para>
        /// </remarks>
        public void WriteError(string message, Exception exception = null)
        {
            if ((false == string.IsNullOrWhiteSpace(message))
                || (exception != null))
            {
                Console.WriteLine();

                if (false == string.IsNullOrWhiteSpace(message))
                {
                    Console.WriteLine(message, Color.OrangeRed);
                }
                if (exception != null)
                {
                    Console.WriteLine(exception.ToString(), Color.OrangeRed);
                }
                Console.WriteLine();
            }

            return;
        }
    }
}
