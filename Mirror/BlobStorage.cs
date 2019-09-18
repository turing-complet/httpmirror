using System;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;

namespace Mirror
{
    public class BlobStorage : IStorage
    {
        private const string connectionString = "DefaultEndpointsProtocol=https;AccountName=mirrortest;AccountKey=lwKOcMreWeRKRDGnR6vNvnubbnhqxxZZVAVpVIQcp4rkh9njfPeVZV8nEA+GCW/KTzHL8N8r7Q/AUAwc3MfgYg==;EndpointSuffix=core.windows.net";

        private CloudBlobContainer _blobContainer;
        private string _root;
        
        public BlobStorage() : this("testroot.com")
        {
        }

        public BlobStorage(string root)
        {
            _root = root;
            CloudStorageAccount storageAccount;
            if (CloudStorageAccount.TryParse(connectionString, out storageAccount))
            {
                CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
                _blobContainer = cloudBlobClient.GetContainerReference(root);
                _blobContainer.CreateIfNotExists();
            }
        }

        public string Get(string path)
        {
            var blobIdent = BlobResource.Parse(_root, path);
            throw new System.NotImplementedException();
        }

        public void Put(string path, string content)
        {
            throw new System.NotImplementedException();
        }

        public bool Exists(string path)
        {
            CloudBlockBlob blob = _blobContainer.GetBlockBlobReference(path);
            return blob.Exists();
        }

        private class BlobResource
        {
            public string Path { get; set;}
            public string BlobName { get; set;}

            public static BlobResource Parse(string root, string requestUri)
            {
                Uri uri = new Uri(new Uri(root), requestUri);
                var blob = new BlobResource();
                blob.Path = uri.LocalPath;
                blob.BlobName = encodeQuery(uri.Query);

                return blob;
            }

            private static string encodeQuery(string query)
            {
                var parsed = Helper.ParseQuery(query);
                return "todo";
            }
        }
    }

    public interface IStorage
    {
        bool Exists(string path);
        void Put(string path, string content);
        string Get(string path);
    }
}