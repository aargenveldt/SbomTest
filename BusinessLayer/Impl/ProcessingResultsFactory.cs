namespace de.Aargenveldt.SbomTest.BusinessLayer.Impl
{
    /// <summary>
    /// Factory für <see cref="IProcessingResults{TResults}"/> Instanzen;
    /// Standardimplementierung von <see cref="IProcessingResultsFactory"/>.
    /// </summary>
    public class ProcessingResultsFactory : IProcessingResultsFactory
    {
        /// <inheritdoc/>
        /// <remarks>
        ///     <para>
        ///         Liefert <see cref="ProcessingResults{TResult}"/> Instanzen.
        ///     </para>
        /// </remarks>
        public IProcessingResults<TResult> Create<TResult>() where TResult : class
        {
            return new ProcessingResults<TResult>();
        }
    }
}
