using PeopleViewer.Common;
using PersonDataReader.Decorators;
using PersonDataReader.Service;
using System.Windows;
using Unity;
using Unity.Injection;

namespace PeopleViewer.Unity
{
    public partial class App : Application
    {
        IUnityContainer Container = new UnityContainer();

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ConfigureContainer();

            ComposeObjects();

            Application.Current.MainWindow.Title = "With Dependency Injection - Unity";
            Application.Current.MainWindow.Show();
        }

        private void ConfigureContainer()
        {
            //Container.RegisterType<IPersonReader, ServiceReader>(TypeLifetime.Singleton);
            //Container.RegisterType<IPersonReader, CSVReader>(TypeLifetime.Singleton);
            //Container.RegisterType<IPersonReader, SQLReader>(TypeLifetime.Singleton);

            //Container.RegisterType<IPersonReader, CachingReader>(new InjectionConstructor(new ResolvedParameter<ServiceReader>()));
            //Container.RegisterFactory<IPersonReader>(f => new CachingReader(new ServiceReader()), FactoryLifetime.Singleton);
            Container.RegisterFactory<IPersonReader>(f => new CachingReader(f.Resolve<ServiceReader>()), FactoryLifetime.Singleton);
        }

        private void ComposeObjects()
        {
            // Instead of newing the PeopleViewerWindow ourselves: Application.Current.MainWindow = new PeopleViewerWindow(viewModel); 
            // We let the DI-Container to manage this...
            Application.Current.MainWindow = Container.Resolve<PeopleViewerWindow>();
        }
    }
}
