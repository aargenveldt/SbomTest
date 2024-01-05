using de.Aargenveldt.SbomTest.TypeInspection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace de.Aargenveldt.SbomTest.BusinessLayer.Impl
{
    /// <summary>
    /// Implementierung der Geschäftslogik; Standardimplementierung
    /// von <see cref="IBusinessLogic"/>.
    /// </summary>
    public class BusinessLogic : IBusinessLogic
    {
        /// <inheritdoc/>
        public bool IsRunning => this._processingCounter > 0;




        /// <summary>
        /// Interner Zähler für aktuell laufende Verarbeitungen.
        /// </summary>
        private int _processingCounter = 0;

        /// <summary>
        /// Factory für Ergebnisobjekte.
        /// </summary>
        private readonly IProcessingResultsFactory _processingResultsFactory;

        /// <summary>
        /// Provider für Assembly Informationen zu Typen.
        /// </summary>
        private readonly IAssemblyInfoProvider _assemblyInfoProvider;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="assemblyInfoProvider">Provider für Assembly Informationen zu Typen</param>
        /// <param name="processingResultsFactory">Factory für Ergebnisobjekte</param>
        /// <exception cref="ArgumentNullException">
        ///     <list type="bullet">
        ///         <item><description><paramref name="assemblyInfoProvider"/> ist <see langword="null"/></description></item>
        ///         <item><description><paramref name="processingResultsFactory"/> ist <see langword="null"/></description></item>
        ///     </list>
        /// </exception>
        public BusinessLogic(IAssemblyInfoProvider assemblyInfoProvider, IProcessingResultsFactory processingResultsFactory)
        {
            this._assemblyInfoProvider = assemblyInfoProvider ?? throw new ArgumentNullException(nameof(assemblyInfoProvider));
            this._processingResultsFactory = processingResultsFactory ?? throw new ArgumentNullException(nameof(processingResultsFactory));
            return;
        }

        /// <inheritdoc/>
        public Task<IProcessingResults<AssemblyInfo>> DoSomeWork()
        {
            Interlocked.Increment(ref this._processingCounter);
            Task<IProcessingResults<AssemblyInfo>> task = Task.Run(() =>
            {
                IProcessingResults<AssemblyInfo> taskResults;
                try
                {
                    Stopwatch sw = Stopwatch.StartNew();
                    DateTimeOffset executionTimestamp = DateTimeOffset.UtcNow;
                    IList<AssemblyInfo> assemblyInfos = this.DetermineAssemblyInfos();
                    sw.Stop();

                    taskResults = this._processingResultsFactory?.Create<AssemblyInfo>();
                    taskResults.Populate(assemblyInfos, sw.Elapsed, executionTimestamp);
                }
                finally
                {
                    Interlocked.Decrement(ref this._processingCounter);
                }
                return taskResults;
            });

            return task;
        }

        /// <summary>
        /// Ermittelt Assembly Informationen zu einigen ausgewählten Typen...
        /// </summary>
        /// <returns></returns>
        private IList<AssemblyInfo> DetermineAssemblyInfos()
        {
            Type[] electedTypes = new Type[]
            {
                typeof(DateTimeOffset),
                typeof(Castle.Core.IServiceProviderEx),
                typeof(Newtonsoft.Json.Linq.JToken),
                typeof(System.Data.Entity.DbContext),
                typeof(Polly.Policy),

                typeof(Snowflake.Data.Core.FastParser),

                typeof(Serilog.Core.Logger)
            };

            IList<AssemblyInfo> assemblyInfos = this._assemblyInfoProvider?.GetAssemblyInfos(electedTypes);

            return assemblyInfos;
        }
    }
}
