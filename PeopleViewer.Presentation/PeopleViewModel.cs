using PeopleViewer.Common;
using PersonDataReader.Service;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PeopleViewer.Presentation
{
    public class PeopleViewModel : INotifyPropertyChanged
    {
        //protected ServiceReader DataReader;
        protected IPersonReader DataReader;                     // Enable different Data sources (e.g CSV, TXT, SQL-DB, etc.) using a Repository Interface (IPersonReader) without braking the existing code...

        private IEnumerable<Person> _people;

        public IEnumerable<Person> People
        {
            get { return _people; }
            set
            {
                if (_people == value)
                    return;
                _people = value;
                RaisePropertyChanged();
            }
        }

        //public PeopleViewModel()
        //{
        //    DataReader = new ServiceReader();
        //}
        public PeopleViewModel(IPersonReader dataReader)        // Instead of managing the Dependency ourselves, we use Constructor DI. This class is no longer responsible for picking the Data Source (ServiceReader). App.xaml is...
        {
            DataReader = dataReader;
        }


        public void RefreshPeople()
        {
            People = DataReader.GetPeople();
        }

        public void ClearPeople()
        {
            People = new List<Person>();
        }

        public string DataReaderType
        {
            get { return DataReader.GetType().ToString(); }
        }


        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
