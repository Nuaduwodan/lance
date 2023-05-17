namespace LanceServer.Core.Workspace;

/// <summary>
/// Provides some helper functions
/// </summary>
public class FileUtil
{
    public static IEnumerable<Uri> GetFilesInDirectory(Uri path, string filter)
    {
        var dir = new DirectoryInfo(path.LocalPath);
        return dir.GetFiles(filter, SearchOption.AllDirectories).Select(info => new Uri(info.FullName));
    }

    public static string ReadContent(Uri uri)
    {
        return ReadFileContent(uri.LocalPath);
    }

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

    public static Uri UriStringToUri(string escapedUriString)
    {
        return new Uri(Uri.UnescapeDataString(escapedUriString).Replace("#", "%23"));
    }

    public static string UriToUriString(Uri uri)
    {
        return Uri.EscapeDataString(uri.LocalPath);
    }

    public static string FileExtensionFromUri(Uri uri)
    {
        return Path.GetExtension(uri.LocalPath).ToLower();
    }
}