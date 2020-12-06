using Microsoft.VisualStudio.TestTools.UnitTesting;
using PeopleViewer.Common;
using System.Collections.Generic;
using System.Linq;

namespace PeopleViewer.Presentation.Tests
{
    [TestClass]
    public class PeopleViewModelTests
    {
        [TestMethod]
        public void People_OnRefreshPeople_IsPopulated()
        {
            // Arrange (set up)
            var dataReader = new FakeReader();
            var peopleViewModel = new PeopleViewModel(dataReader);

            // Act 
            peopleViewModel.RefreshPeople();

            // Assert
            Assert.IsNotNull(peopleViewModel.People);
            Assert.AreEqual(2, peopleViewModel.People.Count());
        }
        
        [TestMethod]
        public void People_OnClearPeople_IsEmpty()
        {
            // Arrange (set up)
            var dataReader = new FakeReader();
            var peopleViewModel = new PeopleViewModel(dataReader);

            peopleViewModel.RefreshPeople();    // just to be sure that the People property was cleared properly...
            Assert.AreNotEqual(0, peopleViewModel.People.Count(), "Invalid Arrangement");

            // Act 
            peopleViewModel.ClearPeople();

            // Assert
            Assert.AreEqual(0, peopleViewModel.People.Count());
        }
    }
}
