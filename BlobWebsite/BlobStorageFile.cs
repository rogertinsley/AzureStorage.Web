using System;

namespace BlobWebsite
{
    public class BlobStorageFile
    {
        public string Name { get; set; }
        public Uri Uri { get; set; }
        public long Size { get; set; }
    }
}