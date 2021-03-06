﻿using PeopleViewer.Presentation;
using System.Windows;

namespace PeopleViewer
{
    public partial class PeopleViewerWindow : Window
    {
        PeopleViewModel viewModel;

        //public PeopleViewerWindow()
        //{
        //    InitializeComponent();
        //    viewModel = new PeopleViewModel();          // 'new' --> requires a compile-time reference, Lifetime responisibility of the object. This pattern (Tight Coupling) goes along all App layers (projects)...
        //    this.DataContext = viewModel;
        //}
        public PeopleViewerWindow(PeopleViewModel peopleViewModel)      // Instead of managing the Dependency ourselves, we use Constructor DI. This class is no longer responsible for the PeopleViewModel. App.xaml is...
        {
            InitializeComponent();
            viewModel = peopleViewModel;
            this.DataContext = viewModel;
        }

        private void FetchButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.RefreshPeople();
            ShowRepositoryType();
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.ClearPeople();
            ClearRepositoryType();
        }

        private void ShowRepositoryType()
        {
            RepositoryTypeTextBlock.Text = viewModel.DataReaderType;
        }

        private void ClearRepositoryType()
        {
            RepositoryTypeTextBlock.Text = string.Empty;
        }
    }
}
