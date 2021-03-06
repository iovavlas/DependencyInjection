﻿using Autofac;
using PeopleViewer.Common;
using PeopleViewer.Presentation;
using PersonDataReader.Decorators;
using PersonDataReader.Service;
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

            //builder.RegisterType<ServiceReader>().As<IPersonReader>().SingleInstance();
            //builder.RegisterType<CSVReader>().As<IPersonReader>().SingleInstance();
            //builder.RegisterType<SQLReader>().As<IPersonReader>().SingleInstance();

            builder.RegisterType<ServiceReader>().Named<IPersonReader>("dataReader").SingleInstance();
            builder.RegisterDecorator<IPersonReader>(
                (container, innerType) => new CachingReader(innerType), fromKey: "dataReader"
            );

            //builder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());          // auto-registering has an impact on the performance (start up time) --> not recommended...
            builder.RegisterType<PeopleViewerWindow>().InstancePerDependency();                 // Manual Registrations...
            builder.RegisterType<PeopleViewModel>().InstancePerDependency();

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
