namespace LanceServer.Core.Workspace;

/// <summary>
/// Provides some helper functions
/// </summary>
public class FileUtil
{
    /// <summary>
    /// Returns uris of all files in the given directory matching the filter.
    /// </summary>
    /// <param name="path">The uri of the target directory</param>
    /// <param name="filter">The glob filter</param>
    public static IEnumerable<Uri> GetFilesInDirectory(Uri path, string filter)
    {
        var dir = new DirectoryInfo(path.LocalPath);
        return dir.GetFiles(filter, SearchOption.AllDirectories).Select(info => new Uri(info.FullName));
    }

    /// <summary>
    /// Returns the content of the file.
    /// </summary>
    /// <param name="uri">The uri of the file.</param>
    public static string ReadContent(Uri uri)
    {
        return ReadFileContent(uri.LocalPath);
    }

    /// <summary>
    /// Returns the content of the file.
    /// </summary>
    /// <param name="path">The path of the file.</param>
    public static string ReadFileContent(string path)
    {
        var result = String.Empty;
        try
        {
            using (StreamReader sr = new StreamReader(path))
            {
                result = sr.ReadToEnd();
            }
        }
        catch (IOException exception)
        {
            Console.Error.WriteLine(exception.StackTrace);
        }

        return result;
    }

    /// <summary>
    /// Converts an escaped string to a <see cref="Uri"/>.
    /// </summary>
    public static Uri UriStringToUri(string escapedUriString)
    {
        return new Uri(Uri.UnescapeDataString(escapedUriString).Replace("#", "%23"));
    }

    /// <summary>
    /// Converts a uri to an escaped string.
    /// </summary>
    public static string UriToUriString(Uri uri)
    {
        return Uri.EscapeDataString(uri.LocalPath);
    }

    /// <summary>
    /// Returns 
    /// </summary>
    /// <param name="uri"></param>
    /// <returns></returns>
    public static string FileExtensionFromUri(Uri uri)
    {
        return Path.GetExtension(uri.LocalPath).ToLower();
    }
}