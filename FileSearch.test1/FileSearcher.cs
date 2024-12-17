namespace FileSearch.test1;

/// <summary>
/// Provides functionality to search for files in a specified directory based on search terms.
/// </summary>
public static class FileSearcher
{
    private static CancellationTokenSource _cancellationTokenSource = new();

    /// <summary>
    /// Starts the file search operation by prompting the user for a directory and search term.
    /// </summary>
    public static void SearchFiles()
    {
        Console.CancelKeyPress += (sender, e) =>
        {
            e.Cancel = true;
            _cancellationTokenSource.Cancel();
            Console.WriteLine("\nSearch operation cancelled by user.");
            Environment.Exit(0);
        };

        string? directory;
        string? searchTerm;

        do
        {
            directory = GetSearchDirectory();
        } while (string.IsNullOrEmpty(directory));

        do
        {
            searchTerm = GetSearchTerms();
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                Console.WriteLine("Please enter a valid search term.");
            }
        } while (string.IsNullOrWhiteSpace(searchTerm));

        ProcessFiles(directory, searchTerm);
    }

    /// <summary>
    /// Prompts the user to enter the directory path for the search.
    /// </summary>
    /// <returns>The directory path entered by the user, or null if invalid.</returns>
    private static string? GetSearchDirectory()
    {
        Console.WriteLine("Hello!, what directory would you like to search?");
        var directory = Console.ReadLine();

        if (string.IsNullOrEmpty(directory))
        {
            Console.WriteLine("Please enter a valid directory.");
            return null;
        }

        if (!Directory.Exists(directory))
        {
            Console.WriteLine("Directory does not exist. Please enter a valid directory.");
            return null;
        }

        return directory;
    }

    /// <summary>
    /// Prompts the user to enter search term.
    /// </summary>
    /// <returns>The search terms entered by the user, or null if empty or invalid.</returns>
    private static string? GetSearchTerms()
    {
        Console.WriteLine("Enter search term:");
        var input = Console.ReadLine();
        return string.IsNullOrEmpty(input) ? null : input.Trim();
    }

    /// <summary>
    /// Processes the files in the specified directory and checks if the content contains the search term.
    /// </summary>
    /// <param name="directory">The directory to search in.</param>
    /// <param name="searchTerm">The term to search for in each file.</param>
    private static void ProcessFiles(string? directory, string searchTerm)
    {
        string[] files = Directory.GetFiles(directory);
        var foundAnyMatches = false;

        foreach (var file in files)
        {
            try
            {
                var content = File.ReadAllText(file);

                if (content.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"Present: {file}");
                    Console.WriteLine("-------------------");

                    foundAnyMatches = true;
                }
                else
                {
                    Console.WriteLine($"Absent: {file}");
                    Console.WriteLine("-------------------");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing {file}: {ex.Message}");
            }
        }

        if (!foundAnyMatches)
        {
            Console.WriteLine("No matches found in any files.");
        }
    }
}