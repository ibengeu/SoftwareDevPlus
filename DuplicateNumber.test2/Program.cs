

using DuplicateNumber.test2;

var collectionA = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
var collectionS = new List<int> { 5, 15, 3, 19, 35, 50, -1, 0 };

var finder = new DuplicateFinder<int>(collectionA);

var results = finder.FindDuplicates(collectionS);
var formattedResults = finder.FormatResults(results);

Console.WriteLine(formattedResults);