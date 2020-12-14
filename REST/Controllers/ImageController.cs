using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.IO;
using System.Net;
using System.Web.Hosting;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net.Http.Headers;

namespace REST.Controllers
{
    public class ImageController : ApiController
    {
        // GET: Image
        [System.Web.Http.Route("Images/{ImageName}")]
        [System.Web.Http.HttpGet]
        public HttpResponseMessage Get(string ImageName)
        {
            ////Byte[] b = System.IO.File.ReadAllBytes(@"C:\sprint4\WebUI\images");   // You can use your own method over here.
            //var b = System.IO.File.OpenRead("C:\\sprint4\\WebUI\\images\\bun.jpg");
            //return File(b, "images/jpeg");
            var result = new HttpResponseMessage(HttpStatusCode.OK);
            String filePath = HostingEnvironment.MapPath($"~/images/{ImageName}");
            FileStream fileStream = new FileStream(filePath, FileMode.Open);
            Image image = Image.FromStream(fileStream);
            MemoryStream memoryStream = new MemoryStream();
            image.Save(memoryStream, ImageFormat.Jpeg);
            result.Content = new ByteArrayContent(memoryStream.ToArray());
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");

            return result;
        }
    }
}