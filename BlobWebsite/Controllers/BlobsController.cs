using BlobWebsite;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace BlobStorage.Web.Controllers
{
    public class BlobsController : ApiController
    {
        public HttpResponseMessage Get(string blobName)
        {
            CloudBlobContainer container = ContainerHelper.GetContainer();
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(blobName);

            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(blockBlob.OpenRead()),
            };

            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            
            return result;
        }
    }
}
