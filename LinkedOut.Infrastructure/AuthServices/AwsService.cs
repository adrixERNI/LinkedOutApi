using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using LinkedOutApi.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace LinkedOutApi.Services
{
    public class AwsService : IAwsService
    {
        private readonly IAmazonS3 _s3Client;
        private readonly ILogger<AwsService> _logger;
        private readonly string _bucketName;

        public AwsService(IAmazonS3 s3Client, ILogger<AwsService> logger, IConfiguration configuration)
        {
            _s3Client = s3Client;
            _logger = logger;
            _bucketName = Environment.GetEnvironmentVariable("AWS_BUCKET_NAME")!;
        }

        public async Task<string> UploadImageToS3Async(Stream imageStream, string fileKey, string contentType = "image/jpeg")
        {
            try
            {
                var metadataRequest = new GetObjectMetadataRequest
                {
                    BucketName = _bucketName,
                    Key = fileKey
                };

                try
                {
                    var metadataResponse = await _s3Client.GetObjectMetadataAsync(metadataRequest);
                    _logger.LogWarning($"File with key {fileKey} already exists in {_bucketName}");
                    throw new Exception("File already exists");
                }
                catch (AmazonS3Exception e) when (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    // Object does not exist, proceed with upload
                }

                var uploadRequest = new TransferUtilityUploadRequest
                {
                    InputStream = imageStream,
                    Key = fileKey,
                    BucketName = _bucketName,
                    ContentType = contentType,
                };

                var fileTransferUtility = new TransferUtility(_s3Client);
                await fileTransferUtility.UploadAsync(uploadRequest);

                _logger.LogInformation($"File uploaded successfully to {_bucketName}/{fileKey}");

                return $"https://{uploadRequest.BucketName}.s3.amazonaws.com/{fileKey}";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error uploading file to S3");
                throw;
            }
        }
    }
}