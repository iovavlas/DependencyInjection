using Autofac;
using Autofac.Features.ResolveAnything;
using PeopleViewer.Common;
using PersonDataReader.CSV;
using PersonDataReader.Service;
using PersonDataReader.SQL;
using System.Windows;

namespace PeopleViewer.Autofac
{
    public partial class App : Application
    {
        IContainer Container;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ConfigureContainer();

            ComposeObjects();

            Application.Current.MainWindow.Title = "With Dependency Injection - Autofac";
            Application.Current.MainWindow.Show();
        }

        private void ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<ServiceReader>().As<IPersonReader>().SingleInstance();
            //builder.RegisterType<CSVReader>().As<IPersonReader>().SingleInstance();
            //builder.RegisterType<SQLReader>().As<IPersonReader>().SingleInstance();

            builder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());        // auto-registering has an impact on the performance (start up time) --> not recommended...

            Container = builder.Build();
        }

        private void ComposeObjects()
        {
            // Instead of newing the PeopleViewerWindow ourselves: Application.Current.MainWindow = new PeopleViewerWindow(viewModel); 
            // We let the DI-Container to manage this...
            Application.Current.MainWindow = Container.Resolve<PeopleViewerWindow>();
            // Notice that there is no reference to the viewModel here --> auto-registration...

        }
    }
}
