using System;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using System.Linq;
using System.Text;

namespace Mirror
{
    public class BlobStorage : IStorage
    {
        private const string connectionString = "DefaultEndpointsProtocol=https;AccountName=mirrortest;AccountKey=lwKOcMreWeRKRDGnR6vNvnubbnhqxxZZVAVpVIQcp4rkh9njfPeVZV8nEA+GCW/KTzHL8N8r7Q/AUAwc3MfgYg==;EndpointSuffix=core.windows.net";

        private CloudBlobContainer _blobContainer;
        private Uri _rootUri;
        public BlobStorage() : this("testroot.com")
        {
        }

        public BlobStorage(string root)
        {
            _rootUri = new Uri(root);
            string containerName = string.Join("-", _rootUri.Host.Split('.'));

            CloudStorageAccount storageAccount;
            if (CloudStorageAccount.TryParse(connectionString, out storageAccount))
            {
                CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
                _blobContainer = cloudBlobClient.GetContainerReference(containerName);
                _blobContainer.CreateIfNotExists();
            }
        }

        public string Get(string path)
        {
            var blobIdent = BlobResource.Parse(_rootUri, path);
            CloudBlockBlob blob = _blobContainer.GetBlockBlobReference(blobIdent.Id);
            string content = blob.DownloadText();
            return content;
        }

        public void Put(string path, string content)
        {
            var blobIdent = BlobResource.Parse(_rootUri, path);
            CloudBlockBlob blob = _blobContainer.GetBlockBlobReference(blobIdent.Id);
            blob.UploadText(content);
        }

        public bool Exists(string path)
        {
            var blobIdent = BlobResource.Parse(_rootUri, path);
            CloudBlockBlob blob = _blobContainer.GetBlockBlobReference(blobIdent.Id);
            return blob.Exists();
        }
    }

    internal class BlobResource
    {
        public string Path { get; set;}
        public string BlobName { get; set;}
        public string Id => System.IO.Path.Combine(Path, BlobName);

        public static BlobResource Parse(Uri rootUri, string requestUri)
        {
            Uri uri = new Uri(rootUri, requestUri);
            var blob = new BlobResource();
            blob.Path = uri.LocalPath.Substring(1);
            blob.BlobName = Helper.EncodeQuery(uri.Query);

            return blob;
        }
    }

    public interface IStorage
    {
        bool Exists(string path);
        void Put(string path, string content);
        string Get(string path);
    }
}