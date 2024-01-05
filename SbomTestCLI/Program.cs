using Autofac;
using de.Aargenveldt.SbomTest.BusinessLayer;
using de.Aargenveldt.SbomTest.BusinessLayer.Impl;
using de.Aargenveldt.SbomTest.CLI.Impl;
using de.Aargenveldt.SbomTest.TypeInspection;
using System;
using System.Threading.Tasks;


namespace de.Aargenveldt.SbomTest.CLI
{
    /// <summary>
    /// Programmlogik.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Einstiegspunkt in die Anwendung.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        static async Task Main(string[] args)
        {
            IContainer container = BuildDIContainer();
            using (ILifetimeScope scope = container.BeginLifetimeScope())
            {
                IApp app = scope.Resolve<IApp>();
                await app.Run();
            }
            return;
        }


        /// <summary>
        /// Baut den IOC Container auf.
        /// </summary>
        /// <returns>Erstellter IOC Container</returns>
        private static IContainer BuildDIContainer()
        {
            ContainerBuilder builder = new ContainerBuilder();

            builder.RegisterType<App>().As<IApp>().SingleInstance();

            builder.RegisterType<ProcessingResultsFactory>().As<IProcessingResultsFactory>();
            builder.RegisterType<BusinessLogic>().As<IBusinessLogic>();
            builder.RegisterType<AssemblyInfoProvider>().As<IAssemblyInfoProvider>();

            builder.RegisterType<ConsoleAssemblyInfoProcessingResultsWriter>().As<IProcessingResultsWriter<AssemblyInfo>>();
            builder.RegisterType<ConsoleBannerWriter>().As<IBannerWriter>();
            builder.RegisterType<ConsoleErrorWriter>().As<IErrorWriter>();
            builder.RegisterType<ConsoleWaitForUser>().As<IWaitForUser>();

            IContainer container = builder.Build();
            return container;
        }
    }


}
