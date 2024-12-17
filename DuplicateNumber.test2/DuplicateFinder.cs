using System.Text;

namespace DuplicateNumber.test2;

public class DuplicateFinder<T> where T : IEquatable<T>
{
    /// <summary>
    /// The reference collection against which we'll check for duplicates.
    /// </summary>
    private readonly List<T> collectionA;

    /// <summary>
    /// Initializes a new instance of the <see cref="DuplicateFinder{T}"/> class.
    /// </summary>
    /// <param name="referenceCollection">The collection to be used for duplicate checks.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="referenceCollection"/> is null.</exception>
    public DuplicateFinder(List<T> referenceCollection)
    {
        collectionA = referenceCollection ?? throw new ArgumentNullException(nameof(referenceCollection));
    }

    /// <summary>
    /// Identifies duplicates from the search collection by checking against the reference collection.
    /// </summary>
    /// <param name="searchCollection">The collection in which to search for duplicates.</param>
    /// <returns>A dictionary with elements from the search collection as keys and a boolean indicating if they are duplicates as values.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="searchCollection"/> is null.</exception>
    public Dictionary<T, bool> FindDuplicates(List<T> searchCollection)
    {
        if (searchCollection == null || searchCollection.Count == 0)
            throw new ArgumentNullException(nameof(searchCollection), "Search collection is null or empty.");

        Dictionary<T, bool> results = new Dictionary<T, bool>();

        foreach (var element in searchCollection)
        {
            var isDuplicate = collectionA.Any(x => x.Equals(element));
            results[element] = isDuplicate;
        }

        return results;
    }

    /// <summary>
    /// Formats the results of duplicate checking into a string representation.
    /// </summary>
    /// <param name="results">The dictionary of results to format.</param>
    /// <returns>A formatted string of the results where each element and its duplicate status is on a new line.</returns>
    public string FormatResults(Dictionary<T, bool> results)
    {
        var output = new StringBuilder();

        foreach (var result in results)
        {
            output.AppendLine($"{result.Key}:{result.Value.ToString().ToLower()}");
        }

        return output.ToString().TrimEnd();
    }
}