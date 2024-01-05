namespace de.Aargenveldt.SbomTest.BusinessLayer
{
    /// <summary>
    /// Factory für die Erstellung von <see cref="IProcessingResults{TResults}"/>
    /// Instanzen.
    /// </summary>
    public interface IProcessingResultsFactory
    {
        /// <summary>
        /// Erzeugt eine geschlossene <see cref="IProcessingResults{TResults}"/> Instanz.
        /// </summary>
        /// <typeparam name="TResult">Typ des Containers für die Verarbeitungsergebnisse</typeparam>
        /// <returns>Neue Instanz von <see cref="IProcessingResults{TResults}"/></returns>
        IProcessingResults<TResult> Create<TResult>()
            where TResult : class;
    }
}
