﻿using Ninject;
using PeopleViewer.Common;
using PersonDataReader.CSV;
using PersonDataReader.Decorators;
using PersonDataReader.Service;
using PersonDataReader.SQL;
using System.Windows;

namespace PeopleViewer.Ninject
{
    public partial class App : Application
    {
        IKernel Container = new StandardKernel();

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ConfigureContainer();

            ComposeObjects();

            Application.Current.MainWindow.Title = "With Dependency Injection - Ninject";
            Application.Current.MainWindow.Show();
        }

        private void ConfigureContainer()
        {
            //Container.Bind<IPersonReader>().To<ServiceReader>().InSingletonScope();
            //Container.Bind<IPersonReader>().To<CSVReader>().InSingletonScope();
            //Container.Bind<IPersonReader>().To<SQLReader>().InSingletonScope();

            Container.Bind<IPersonReader>().To<CachingReader>().InSingletonScope()
                //.WithConstructorArgument<IPersonReader>(new ServiceReader());                 // that would also work, but why not letting the Container manage it...
                .WithConstructorArgument<IPersonReader>(Container.Get<ServiceReader>());
        }

        private void ComposeObjects()
        {
            // Instead of newing the PeopleViewerWindow ourselves: Application.Current.MainWindow = new PeopleViewerWindow(viewModel); 
            // We let the DI-Container to manage this...
            Application.Current.MainWindow = Container.Get<PeopleViewerWindow>();
            // Notice that there is no reference to the viewModel here --> auto-wiring...
        }
    }
}
