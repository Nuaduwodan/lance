namespace LanceServer.DataModels
{
    /// <summary>
    /// Represents a single file and is used to cache data about it.
    /// </summary>
    public class Document
    {
        private string _content;

        public bool Changed
        {
            get;
            set;
        }
        public string Code
        {
            get
            {
                return _content;
            }
            set
            {
                if (_content == value)
                {
                    return;
                }
                Changed = true;
                _content = value;
            }
        }
        public string Encoding { get; set; }
        public string FullPath { get; private set; }

        public Document(string path, string encoding = "utf8")
        {
            FullPath = Util.GetProperFilePathCapitalization(path);
            Encoding = encoding;
            Changed = false;
        }

        public Document FindDocument(string path)
        {
            var normalized_path = Util.GetProperFilePathCapitalization(path);

            if (normalized_path == this.FullPath)
            {
                return this;
            }

            return null;
        }
    }
}