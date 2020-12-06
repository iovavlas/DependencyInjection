using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;

namespace PersonDataReader.CSV.Tests
{
    [TestClass]
    public class CSVReaderTests
    {
        [TestMethod]
        public void GetPeople_WithGoodRecords_ReturnsAllRecords()
        {
            // Arrange (set up)
            var cSVReader = new CSVReader();
            cSVReader.FileLoader = new FakeFileLoader("Good");                  // With Property Injection, we can override the default behaviour (real CSV File Reader) with a fake one (FakeFileLoader)...

            // Act 
            var result = cSVReader.GetPeople();

            // Assert
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public void GetPeople_WithNoFile_ThrowsException()
        {
            // Arrange (set up)
            var cSVReader = new CSVReader();

            // Assert
            Assert.ThrowsException<FileNotFoundException>(
                // Act 
                () => cSVReader.GetPeople()
            );
        }

        [TestMethod]
        public void GetPeople_WithSomeBadRecords_ReturnsGoodRecords()
        {
            // Arrange (set up)
            var cSVReader = new CSVReader();
            cSVReader.FileLoader = new FakeFileLoader("Mixed");                  

            // Act 
            var result = cSVReader.GetPeople();

            // Assert
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public void GetPeople_WithOnlyBadRecords_ReturnsEmptyList()
        {
            // Arrange (set up)
            var cSVReader = new CSVReader();
            cSVReader.FileLoader = new FakeFileLoader("Bad");

            // Act 
            var result = cSVReader.GetPeople();

            // Assert
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void GetPeople_WithEmptyFile_ReturnsEmptyList()
        {
            // Arrange (set up)
            var cSVReader = new CSVReader();
            cSVReader.FileLoader = new FakeFileLoader("Empty");

            // Act 
            var result = cSVReader.GetPeople();

            // Assert
            Assert.AreEqual(0, result.Count());
        }
    }
}
