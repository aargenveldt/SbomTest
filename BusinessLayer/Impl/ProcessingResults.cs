using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace de.Aargenveldt.SbomTest.BusinessLayer.Impl
{
    /// <summary>
    /// Container für Ergebnisse der Verarbeitungslogik.
    /// </summary>
    public class ProcessingResults<TResult> : IProcessingResults<TResult> where TResult : class
    {
        /// <inheritdoc/>
        public IList<TResult> Results => this._results ?? Array.Empty<TResult>();
        private TResult[] _results;

        /// <inheritdoc/>
        public bool HasResults => this.Results.Count > 0;

        /// <inheritdoc/>
        public TimeSpan Runtime { get; private set; }

        /// <inheritdoc/>
        public DateTimeOffset ExecutionTimestamp { get; private set; }

        /// <summary>
        /// Vermerkt, ob Daten bereits gesetzt sind (=1).
        /// </summary>
        private int _locked = 0;

        /// <inheritdoc/>
        public void Populate(IEnumerable<TResult> results, TimeSpan runtime, DateTimeOffset? executionTimestamp = null)
        {
            bool locked = (0 != Interlocked.CompareExchange(ref this._locked, 1, 0));

            if (true == locked)
                throw new InvalidOperationException("Results already populated");
            else
            {
                this._results = (results ?? Enumerable.Empty<TResult>()).ToArray();
                this.Runtime = runtime;
                this.ExecutionTimestamp = executionTimestamp ?? DateTimeOffset.UtcNow;
            }

            return;
        }
    }
}
