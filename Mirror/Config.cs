using System;

namespace Mirror
{
    public class Config
    {
        public static string BlobConnection => Environment.GetEnvironmentVariable("BLOB_MIRROR_CONNECTION");
        public static string Enabled => Environment.GetEnvironmentVariable("ENABLE_MIRROR");
    }
}