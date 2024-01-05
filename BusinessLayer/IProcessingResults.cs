using System;
using System.Collections.Generic;

namespace de.Aargenveldt.SbomTest.BusinessLayer
{
    /// <summary>
    /// Interface für Container, die Ergebnisse der Verarbeitungslogik transportieren.
    /// </summary>
    public interface IProcessingResults<TResults> where TResults : class
    {
        /// <summary>
        /// Die &quot;Ergebnisse&quot; - oder eine leere Liste.
        /// </summary>
        IList<TResults> Results { get; }

        /// <summary>
        /// Gibt an, ob Ergebnisobjekte verfügbar sind.
        /// </summary>
        bool HasResults { get; }

        /// <summary>
        /// Laufzeit zur Ermittlung der Ergebnisse.
        /// </summary>
        TimeSpan Runtime { get; }

        /// <summary>
        /// Zeitstempel der Ausführung
        /// </summary>
        DateTimeOffset ExecutionTimestamp { get; }

        /// <summary>
        /// Setzt die Ergebniswerte.
        /// </summary>
        /// <param name="results">Ergebnisse</param>
        /// <param name="runtime">Laufzeit</param>
        /// <param name="executionTimestamp">Optional:
        ///     Zeitstempel der Ausführung; wenn <see langword="null"/>, dann wird der
        ///     Zeitstempel vom Zeitpunkt des Aufrufs der Methode gesetzt; Standardwert
        ///     ist <see langword="null"/>.
        /// </param>
        /// <exception cref="InvalidOperationException">
        ///     Mehrfacher Aufruf der Methode - Ergebniswerte wurden bereits gesetzt.
        /// </exception>
        void Populate(IEnumerable<TResults> results, TimeSpan runtime, DateTimeOffset? executionTimestamp = null);
    }
}
