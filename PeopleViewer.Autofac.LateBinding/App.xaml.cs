using Autofac;
using Autofac.Configuration;
using Autofac.Features.ResolveAnything;
using Microsoft.Extensions.Configuration;
using System.Windows;

namespace PeopleViewer.Autofac.LateBinding
{
    public partial class App : Application
    {
        IContainer Container;

        protected override void OnStartup(StartupEventArgs e)                                   // See the last Pre-Build event (copy Assembly References)...
        {
            base.OnStartup(e);

            ConfigureContainer();                                   

            ComposeObjects();

            Application.Current.MainWindow.Title = "With Dependency Injection - Autofac Late Binding";
            Application.Current.MainWindow.Show();
        }

        private void ConfigureContainer()
        {
            var config = new ConfigurationBuilder();

            config.AddJsonFile("autofac.json");                                                 // This file contains the registrations. We can change the configuration there (pick another DataReader) without building the entire App...

            var module = new ConfigurationModule(config.Build());
            var builder = new ContainerBuilder();
            builder.RegisterModule(module);

            builder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());            // auto-registering has an impact on the performance (start up time) --> not recommended...

            Container = builder.Build();
        }

        private void ComposeObjects()
        {
            // Instead of newing the PeopleViewerWindow ourselves: Application.Current.MainWindow = new PeopleViewerWindow(viewModel); 
            // We let the DI-Container to manage this...
            Application.Current.MainWindow = Container.Resolve<PeopleViewerWindow>();
        }
    }
}
