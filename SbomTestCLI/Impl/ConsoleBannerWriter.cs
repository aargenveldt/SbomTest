using System.Drawing;
using System.Reflection;

using Console = Colorful.Console;

namespace de.Aargenveldt.SbomTest.CLI.Impl
{

    /// <summary>
    /// Ausgabe des Banners (=Anwendungstitels) auf der Konsole.
    /// </summary>
    internal class ConsoleBannerWriter : IBannerWriter
    {
        /// <inheritdoc/>   
        /// <remarks>
        ///     <para>
        ///         Ausgabe erfolgt auf der Konsole.
        ///     </para>
        /// </remarks>
        public void WriteBanner(bool showVersion = true, bool prependNewline = true, bool appendNewline = true)
        {
            Assembly entryAssembly = Assembly.GetEntryAssembly();

            string title = entryAssembly?.GetCustomAttribute<AssemblyTitleAttribute>()?.Title ?? entryAssembly?.GetName()?.Name;
            string renderedTitle = Figgle.FiggleFonts.Slant.Render(title);

            if (true == prependNewline)
                Console.WriteLine();

            //Colorful.Console.WriteLine(renderedTitle, System.Drawing.Color.GreenYellow);
            Console.WriteWithGradient(renderedTitle, System.Drawing.Color.RoyalBlue, System.Drawing.Color.GreenYellow, maxColorsInGradient: 8);

            if (true == showVersion)
                Console.WriteLine($"v{entryAssembly?.GetName()?.Version}", Color.LightGray);

            if (true == appendNewline)
                Console.WriteLine();


            return;
        }
    }
}
