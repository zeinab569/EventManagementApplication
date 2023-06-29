using Azure.Storage.Blobs;

namespace EventManagementApp.Helpers
{
    public class UploadImage
    {
        private readonly BlobContainerClient _blobServiceClient;

        public UploadImage(BlobContainerClient blobContainerClient)
        {
            _blobServiceClient = blobContainerClient;
        }

        public async Task<string> UploadToCloud(IFormFile file)
        {
            string filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            BlobClient blobClient = _blobServiceClient.GetBlobClient(filename);
            await blobClient.UploadAsync(file.OpenReadStream(), true);
            return blobClient.Uri.ToString();
        }
    }

}
