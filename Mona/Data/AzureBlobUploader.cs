using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Amazon;
using Amazon.S3.Transfer;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;

namespace Mona.Data
{
    public class AzureBlobUploader
    {
        private string _connectionString;
        private string _containerName;

        public AzureBlobUploader(IConfiguration config)
        {
            _connectionString = config["AZURE_STORAGE_CONNECTION_STRING"];
            _containerName = config["AZURE_STORAGE_CONTAINER_NAME"];
        }

        public async Task<string> UploadFileAsync(Stream stream, string keyName)
        {
            var container = new BlobContainerClient(_connectionString, _containerName);
            await container.CreateIfNotExistsAsync();
            var blob = container.GetBlobClient(keyName);
            var response = await blob.UploadAsync(stream);
            return blob.Uri.AbsoluteUri;
        }
    }
}
