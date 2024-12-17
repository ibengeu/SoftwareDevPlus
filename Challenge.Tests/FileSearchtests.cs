using FileSearch.test1;

namespace Challenge.Tests
{
    [TestClass]
    public class FileSearcherTests
    {
        private string testDirectory;

        [TestInitialize]
        public void Setup()
        {
            testDirectory = Path.Combine(Path.GetTempPath(), "FileSearcherTests");
            Directory.CreateDirectory(testDirectory);

            File.WriteAllText(Path.Combine(testDirectory, "test1.txt"), "This is a test file");
            File.WriteAllText(Path.Combine(testDirectory, "test2.txt"), "Another test file");
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (Directory.Exists(testDirectory))
            {
                Directory.Delete(testDirectory, true);
            }
        }

        [TestMethod]
        public void ProcessFiles_WithValidInput_FindsMatchingFiles()
        {
            // Arrange
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            // Act
            var methodInfo = typeof(FileSearcher).GetMethod("ProcessFiles",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
            methodInfo.Invoke(null, new object[] { testDirectory, "test" });

            // Assert
            var output = stringWriter.ToString();
            Assert.IsTrue(output.Contains("Present:"));
            Assert.IsTrue(output.Contains("test1.txt"));
            Assert.IsTrue(output.Contains("test2.txt"));
        }

        [TestMethod]
        public void ProcessFiles_WithNonMatchingTerm_ShowsAbsent()
        {
            // Arrange
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            // Act
            var methodInfo = typeof(FileSearcher).GetMethod("ProcessFiles",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
            methodInfo.Invoke(null, new object[] { testDirectory, "nonexistent" });

            // Assert
            var output = stringWriter.ToString();
            Assert.IsTrue(output.Contains("Absent:"));
            Assert.IsTrue(output.Contains("No matches found in any files."));
        }

        [TestMethod]
        public void GetSearchDirectory_WithValidDirectory_ReturnsDirectory()
        {
            // Arrange
            var stringReader = new StringReader(testDirectory + Environment.NewLine);
            Console.SetIn(stringReader);
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            // Act
            var methodInfo = typeof(FileSearcher).GetMethod("GetSearchDirectory",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
            var result = methodInfo.Invoke(null, null) as string;

            // Assert
            Assert.AreEqual(testDirectory, result);
        }

        [TestMethod]
        public void GetSearchDirectory_WithInvalidDirectory_ReturnsNull()
        {
            // Arrange
            var invalidPath = Path.Combine(Path.GetTempPath(), "NonExistentDirectory");
            var stringReader = new StringReader(invalidPath + Environment.NewLine);
            Console.SetIn(stringReader);
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            // Act
            var methodInfo = typeof(FileSearcher).GetMethod("GetSearchDirectory",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
            var result = methodInfo.Invoke(null, null) as string;

            // Assert
            Assert.IsNull(result);
            Assert.IsTrue(stringWriter.ToString().Contains("Directory does not exist"));
        }
    }
}