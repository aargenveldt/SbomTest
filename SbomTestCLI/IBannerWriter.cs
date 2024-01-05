
namespace de.Aargenveldt.SbomTest.CLI
{
    /// <summary>
    /// Interface für Klassen, die ein Banner (=Anwendungstitel) ausgeben können.
    /// </summary>
    public interface IBannerWriter
    {
        /// <summary>
        /// Ausgabe des Banners (=Anwendungstitel).
        /// </summary>
        /// <param name="showVersion">Optional: Version ausgeben; Standardwert ist <see langword="true"/></param>
        /// <param name="prependNewline">Optional: Eine führende Leerzeile ausgeben; Standardwert ist <see langword="true"/></param>
        /// <param name="appendNewline">Optional: Eine folgende Leerzeile ausgeben; Standardwert ist <see langword="true"/></param>
        void WriteBanner(bool showVersion = true, bool prependNewline = true, bool appendNewline = true);
    }
}
