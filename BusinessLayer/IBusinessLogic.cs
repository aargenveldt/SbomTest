using de.Aargenveldt.SbomTest.TypeInspection;
using System.Threading.Tasks;

namespace de.Aargenveldt.SbomTest.BusinessLayer
{
    /// <summary>
    /// Interface für Klassen, die die Business Logik implementieren...
    /// </summary>
    public interface IBusinessLogic
    {
        /// <summary>
        /// Gibt an, ob aktuell eine Verarbeitung läuft.
        /// </summary>
        bool IsRunning { get; }

        /// <summary>
        /// Verarbeitungslogik... (liefert eine Liste von Assembly Informationen
        /// zu einer Auswahl von intern verwendeten Typen).
        /// </summary>
        /// <returns>
        ///     Waitable Task, der die Ausführung der Verarbeitungslogik repräsentiert;
        ///     liefert eine Liste von Assembly Informationen zu ausgewählten, intern
        ///     verwendeten Typen.
        /// </returns>
        Task<IProcessingResults<AssemblyInfo>> DoSomeWork();
    }
}
