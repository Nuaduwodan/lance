namespace LanceServer.Core.Workspace
{
    /// <summary>
    /// Provides some helper functions
    /// </summary>
    public class FileIoHelper
    {
        private static Dictionary<string, string> _cache = new();

        public static string GetProperDirectoryCapitalization(DirectoryInfo dirInfo)
        {
            DirectoryInfo parentDirInfo = dirInfo.Parent;
            if (null == parentDirInfo)
            {
                return dirInfo.Name.ToUpper();
            }

            return Path.Combine(GetProperDirectoryCapitalization(parentDirInfo),
                parentDirInfo.GetDirectories(dirInfo.Name)[0].Name);
        }

        public static string GetProperFilePathCapitalization(string filename)
        {
            // cached
            if (_cache.TryGetValue(filename, out string newFilename))
                return newFilename;
            
            // doesn't exist
            FileInfo fileInfo = new FileInfo(filename);
            if (!fileInfo.Exists)
            {
                _cache[filename] = filename;
                return filename;
            }
            
            // not in this directory
            DirectoryInfo dirInfo = fileInfo.Directory;
            if (dirInfo.GetFiles(fileInfo.Name).Length == 0)
            {
                _cache[filename] = filename;
                return filename;
            }
            
            // found
            string result = Path.Combine(GetProperDirectoryCapitalization(dirInfo),
                dirInfo.GetFiles(fileInfo.Name)[0].Name);
            _cache[filename] = result;
            return result;
        }

        public static IEnumerable<Uri> GetFilesInDirectory(Uri path, string filter)
        {
            var dir = new DirectoryInfo(path.AbsolutePath);
            return dir.GetFiles(filter).Select(info => new Uri(info.FullName));
        }

        public static Document ReadDocument(Uri uri)
        {
            Document document = new Document(uri);
            try
            {
                using (StreamReader sr = new StreamReader(uri.AbsolutePath))
                {
                    string str = sr.ReadToEnd();
                    document.Content = str;
                }
            }
            catch (IOException exception)
            {
                Console.Error.WriteLine(exception.StackTrace);
            }
            return document;
        }
    }
}