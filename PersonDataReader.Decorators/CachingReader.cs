using PeopleViewer.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PersonDataReader.Decorators
{
    public class CachingReader : IPersonReader                                  // The CachingReader must also implement the IPersonReader Interface, so that it can be used from the PeopleViewModel...
    {                                                                           // We use the Decorator pattern -> we wrap the IPersonReader Interface to add functionality...
        private IPersonReader _wrappedReader;
        private TimeSpan _cacheDuration = new TimeSpan(0, 0, 20);               // cache is valid for 20 sec...

        private IEnumerable<Person> _cachedItems;
        private DateTime _dataDateTime;

        public CachingReader(IPersonReader wrappedReader)
        {
            _wrappedReader = wrappedReader;
        }

        public IEnumerable<Person> GetPeople()
        {
            ValidateCache();
            return _cachedItems;
        }

        public Person GetPerson(int id)
        {
            ValidateCache();
            return _cachedItems.FirstOrDefault(p => p.Id == id);
        }

        private bool IsCacheValid
        {
            get
            {
                if (_cachedItems == null)
                    return false;
                var _cacheAge = DateTime.Now - _dataDateTime;
                return _cacheAge < _cacheDuration;
            }
        }

        private void ValidateCache()
        {
            if (IsCacheValid)
                return;

            try
            {
                _cachedItems = _wrappedReader.GetPeople();                  // We can use any DataReader we want. We just have to configure it in the App.xaml. We can e.g. start and then stop the ServiceReader to test the cache...
                _dataDateTime = DateTime.Now;
            }
            catch
            {
                _cachedItems = new List<Person>()
                {
                    new Person()
                    {
                        GivenName = "No Data Available",
                        FamilyName = string.Empty,
                        Rating = 0,
                        StartDate = DateTime.Today,
                    }
                };
                InvalidateCache();
            }
        }

        private void InvalidateCache()
        {
            _dataDateTime = DateTime.MinValue;
        }
    }
}
