using BlobWebsite;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace BlobStorage.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            CloudBlobContainer container = ContainerHelper.GetContainer();
            var blobs = container.ListBlobs(useFlatBlobListing: true);

            var filesList = new List<BlobStorageFile>();
            foreach (IListBlobItem item in blobs)
            {
                if (item is CloudBlockBlob)
                {
                    var blob = (CloudBlockBlob)item;
                    filesList.Add(new BlobStorageFile
                    {
                        Name = blob.Name, 
                        Size = blob.Properties.Length / 1024, 
                        Uri = blob.Uri
                    });
                }
            }

            return View(filesList);
        }

        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file)
        {            
            CloudBlobContainer container = ContainerHelper.GetContainer();            
            var fileName = Path.GetFileName(file.FileName);
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileName);
            blockBlob.UploadFromStream(file.InputStream);

            return RedirectToAction("Index");            
        }
    }
}
