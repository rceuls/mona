using System;
using System.IO;
using System.Threading.Tasks;
using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Transfer;
using Microsoft.Extensions.Configuration;

namespace Mona.Data
{
    public class AwsS3Uploader
    {
        private readonly AmazonS3Client _s3Client;
        private readonly string _bucketName;

        public AwsS3Uploader(IConfiguration config)
        {
            AWSConfigs.AWSRegion = config["AWS_REGION"];
            _bucketName = config["AWS_BUCKET_NAME"];
            _s3Client = new AmazonS3Client(new BasicAWSCredentials(config["AWS_ACCESS_KEY"], config["AWS_ACCESS_SECRET"]));

         }

        public async Task<string> UploadFileAsync(Stream stream, string keyName)
        {
            var fileTransferUtility =  new TransferUtility(_s3Client);
            await fileTransferUtility.UploadAsync(stream, _bucketName, keyName);
            return $"https://{_bucketName}.s3.{AWSConfigs.AWSRegion}.amazonaws.com/{keyName}";
        }
    }
}
