using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace BlobWebsite
{
    public static class ContainerHelper
    {
        public static CloudBlobContainer GetContainer()
        {
            var storage = "DefaultEndpointsProtocol=https;AccountName=ACCOUNT_NAME;AccountKey=ACCOUNT_KEY";

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                storage
            );

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            CloudBlobContainer container = blobClient.GetContainerReference("files");

            container.CreateIfNotExists();

            container.SetPermissions(new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Blob

            });

            return container;
        }
    }
}