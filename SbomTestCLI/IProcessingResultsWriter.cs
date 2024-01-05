using de.Aargenveldt.SbomTest.BusinessLayer;
using de.Aargenveldt.SbomTest.TypeInspection;

namespace de.Aargenveldt.SbomTest.CLI
{
    /// <summary>
    /// Interface für Klassen, die die Daten von <see cref="AssemblyInfo"/> (formatiert)
    /// ausgeben können.
    /// </summary>
    public interface IProcessingResultsWriter<TResult> where TResult : class
    {
        /// <summary>
        /// (Formatierte) Ausgabe von Verarbeitungsergebnissen.
        /// </summary>
        /// <param name="processingResults">Verarbeitungsergebnisse, die ausgegeben
        /// werden sollen; kann <see langword="null"/> sein - oder eine leere Liste von
        /// Ergebnisobjekten beinhalten</param>
        void WriteProcessingResults(IProcessingResults<TResult> processingResults);
    }
}
