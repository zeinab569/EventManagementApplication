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

        public async Task<string> UpdateInCloud(string existingImageUrl, IFormFile newFile)
        {
            // Delete the existing image
            await DeleteFromCloud(existingImageUrl);
            // Upload the new image
            return await UploadToCloud(newFile);
        }

        public async Task DeleteFromCloud(string imageUrl)
        {
            if (Uri.TryCreate(imageUrl, UriKind.Absolute, out Uri uri) && uri.Scheme == Uri.UriSchemeHttp)
            {
                string filename = Path.GetFileName(uri.LocalPath);
                BlobClient blobClient = _blobServiceClient.GetBlobClient(filename);
                await blobClient.DeleteIfExistsAsync();
            }
            else if (Uri.TryCreate(imageUrl, UriKind.RelativeOrAbsolute, out Uri relativeUri))
            {
                string filename = Path.GetFileName(relativeUri.OriginalString);
                BlobClient blobClient = _blobServiceClient.GetBlobClient(filename);
                await blobClient.DeleteIfExistsAsync();
            }
        }
    }
    /*
     public class UploadImage

    public async Task<string> UpdateInCloud(string existingImageUrl, IFormFile newFile)
    {
        // Delete the existing image
        await DeleteFromCloud(existingImageUrl);

        // Upload the new image
        return await UploadToCloud(newFile);
    }

    public async Task DeleteFromCloud(string imageUrl)
    {
        Uri uri = new Uri(imageUrl);
        string filename = Path.GetFileName(uri.LocalPath);
        BlobClient blobClient = _blobContainerClient.GetBlobClient(filename);
        await blobClient.DeleteIfExistsAsync();
    }
}

     */
}
