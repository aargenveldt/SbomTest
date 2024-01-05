using Colorful;
using de.Aargenveldt.SbomTest.BusinessLayer;
using de.Aargenveldt.SbomTest.TypeInspection;
using System.Drawing;
using System.Linq;

using Console = Colorful.Console;

namespace de.Aargenveldt.SbomTest.CLI.Impl
{
    /// <summary>
    /// Ausgabe von Assembly informationen auf der Konsole.
    /// </summary>
    internal class ConsoleAssemblyInfoProcessingResultsWriter : IProcessingResultsWriter<AssemblyInfo>
    {
        /// <inheritdoc/>
        /// <remarks>
        ///     <para>
        ///         Ausgabe der Assembly Informationen auf der Konsole.
        ///     </para>
        /// </remarks>
        public void WriteProcessingResults(IProcessingResults<AssemblyInfo> processingResults)
        {
            if (processingResults == null)
            {
                Console.WriteLine("No processing results!", Color.OrangeRed);
            }
            else
            {
                Formatter[] parts;

                parts = new Formatter[]
                {
                        new Formatter($"{processingResults.ExecutionTimestamp.ToLocalTime()}", Color.FloralWhite),
                        new Formatter($"{processingResults.Runtime}", Color.FloralWhite)
                };
                Console.WriteLineFormatted("Processing result @ {0} (Duration: {1})", Color.LightGray, parts);
                Console.WriteLine();

                if (false == processingResults.HasResults)
                {
                    Console.WriteLine($"No results", Color.OrangeRed);
                }
                else
                {
                    StyleSheet styleSheet = new StyleSheet(Color.LightGray);
                    styleSheet.AddStyle(@"\d+", Color.FloralWhite);
                    Console.WriteLineStyled($"Assembly Infos: {processingResults?.Results?.Count}", styleSheet);

                    AssemblyInfo[] infos = (processingResults.Results ?? Enumerable.Empty<AssemblyInfo>())
                                            .Where(item => item != null)
                                            .ToArray();

                    foreach (AssemblyInfo info in infos)
                    {
                        parts = new Formatter[]
                        {
                                new Formatter($"{info.Name ?? "???"}", Color.CornflowerBlue),
                                new Formatter($"v{info.Version?.ToString() ?? "?.?.?.?"}", Color.Goldenrod),
                                new Formatter($"\"{info.Path}\"", Color.Gray)
                        };
                        Console.WriteLineFormatted("  > {0}, {1} @ {2}", Color.LightGray, parts);
                    }

                }
            }

            return;
        }

    }
}
