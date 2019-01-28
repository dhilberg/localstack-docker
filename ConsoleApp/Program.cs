using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        private const string S3_URL = "http://localhost:4572";
        private const string BUCKET_NAME = "TestBucket";
        private const string UPLOAD_FILEPATH = "UploadMe.txt";

        static async Task Main(string[] args)
        {
            var program = new Program();
            await program.UploadFile(BUCKET_NAME, UPLOAD_FILEPATH);
        }

        public async Task UploadFile(string bucketName, string filePath)
        {
            // Even though Localstack ignores AWS credentials, if you don't have any set up, e.g. via `aws configure` (~/.aws/credentials), and you
            // try to invoke any methods on the S3 client you will get an HttpRequestException with a 'No route to host' error message, which is not helpful.
            // We are using anonymous credentials here to simply avoid this issue.
            // The AWS SDK will use any configured credentials if set up.
            // TODO: Add logic to use local AWS creds if set up, otherwise fall back to anonymous ones.
            var creds = new AnonymousAWSCredentials();
            var config = new AmazonS3Config
            {
                ServiceURL = S3_URL,
                // Unless configured otherwise, by default Localstack only supports http.
                UseHttp = true,
                // Force the use of hostname/bucket/file pattern, instead of the default bucket.hostname/file, which will cause
                // domain name resolution issues.
                ForcePathStyle = true
            };

            using (var client = new AmazonS3Client(creds, config))
            {
                // Create the bucket if it doesn't already exist
                var bucket = await client.PutBucketAsync(new PutBucketRequest
                {
                    BucketName = bucketName,
                    UseClientRegion = true
                });

                var list = await client.ListBucketsAsync();

                var transferUtility = new TransferUtility(client);
                var exists = File.Exists(filePath);
                await transferUtility.UploadAsync(filePath, bucketName);

                var objects = await client.ListObjectsAsync(bucketName);
            }
        }
    }
}
