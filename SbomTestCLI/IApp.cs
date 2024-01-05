using System.Threading.Tasks;

namespace de.Aargenveldt.SbomTest.CLI
{
    /// <summary>
    /// Interface für Anwendungsklassen.
    /// </summary>
    internal interface IApp
    {
        /// <summary>
        /// Anwendungslogik ausführen.
        /// </summary>
        /// <returns></returns>
        Task Run();
    }
}
