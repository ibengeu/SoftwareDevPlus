using DuplicateNumber.test2;

namespace Challenge.Tests
{
    [TestClass]
    public class DuplicateFinderTests
    {
        private DuplicateFinder<int> finder;
        private List<int> referenceList;

        [TestInitialize]
        public void Initialize()
        {
            referenceList = new List<int> { 1, 2, 3 };
            finder = new DuplicateFinder<int>(referenceList);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_NullCollection_ThrowsException()
        {
            _ = new DuplicateFinder<int>(null);
        }

        [TestMethod]
        public void FindDuplicates_ValidInput_ReturnsCorrectResults()
        {
            var searchList = new List<int> { 1, 4 };
            var results = finder.FindDuplicates(searchList);

            Assert.IsTrue(results[1], "Should identify 1 as a duplicate");
            Assert.IsFalse(results[4], "Should identify 4 as not a duplicate");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FindDuplicates_NullInput_ThrowsException()
        {
            finder.FindDuplicates(null);
        }

        [TestMethod]
        public void FormatResults_ValidInput_ReturnsFormattedString()
        {
            var results = new Dictionary<int, bool>
            {
                { 1, true },
                { 4, false }
            };

            var formatted = finder.FormatResults(results);
            var expected = "1:true\r\n4:false";

            Assert.AreEqual(expected, formatted);
        }
    }
}