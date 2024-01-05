namespace de.Aargenveldt.SbomTest.CLI
{
    /// <summary>
    /// Interface für Klassen, die auf eine Reaktion des Benutzers warten (ggf. mit Hinweistext).
    /// </summary>
    public interface IWaitForUser
    {
        /// <summary>
        /// Auf eine Benutzerinteraktion warten; optional einen Hinweistext ausgeben.
        /// </summary>
        /// <param name="showPrompt">Hinweistext ausgeben Ja/Nein</param>
        /// <param name="prependNewline">Optional: Eine führende Leerzeile ausgeben; Standardwert ist <see langword="true"/></param>
        void AwaitUserAction(bool showPrompt, bool prependNewline = true);
    }
}
