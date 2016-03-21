using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;

namespace MNF4.Controllers
{
    //http://rivdiv.wordpress.com/2012/06/13/uploading-a-file-using-asp-net-web-api/
    //http://www.asp.net/web-api/overview/working-with-http/sending-html-form-data,-part-2
    public class FilesController : ApiController
    {
        [System.Web.Mvc.Authorize(Roles = "mnf_admin")]
        public List<string> GetDirectories()
        {
            var directories = new List<string>();

            Directory.GetDirectories(HttpContext.Current.Server.MapPath("~/Content/")).ToList()
                .ForEach(d => directories.Add((new DirectoryInfo(d)).Name));

            return new List<string>(directories);
        } 

        [System.Web.Mvc.Authorize(Roles = "mnf_admin")]
        public Task<HttpResponseMessage> UploadFile([FromUri] string folder, [FromUri] string newFileName)
        {
            #region ASP.Net example code

//            // Check if the request contains multipart/form-data.
//            if (!Request.Content.IsMimeMultipartContent())
//            {
//                throw new HttpResponseException(new HttpResponseMessage()
//                    { Content = new StringContent(string.Format("Unsupported Media Type for {0}", newFileName)) });
//            }
//
//            // Save file
//            string path = HttpContext.Current.Server.MapPath(string.Format("~/{0}", folder));
//            var provider = new MultipartFormDataStreamProvider(path);
//
//            try
//            {
//                // Read the form data.
//                await Request.Content.ReadAsMultipartAsync(provider);
//
//                // This illustrates how to get the file names.
//                foreach (MultipartFileData file in provider.FileData)
//                {
//                    Trace.WriteLine(file.Headers.ContentDisposition.FileName);
//                    Trace.WriteLine("Server file path: " + file.LocalFileName);
//                }
//                return Request.CreateResponse(HttpStatusCode.OK);
//            }
//            catch (System.Exception e)
//            {
//                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
//            }

//            var task = Request.Content.ReadAsMultipartAsync(provider).
//                ContinueWith<HttpResponseMessage>(t =>
//                {
//                    if (t.IsFaulted || t.IsCanceled)
//                    {
//                      //   Log t.Exception;
//                        var error = "Error Occurred";
//                        Request.CreateErrorResponse(HttpStatusCode.InternalServerError, t.Exception);
//                    }
//
//                    //  handle the files here
//                    var bodyPart = provider.BodyPartFileNames.FirstOrDefault();
//                    string savedFile = bodyPart.Value;
//                    string originalFile = bodyPart.Key.TrimStart('"').TrimEnd('"');
//                    string newFile = string.Format("{0}{1}", newFileName, Path.GetExtension(originalFile));
//
//                    // Copy file and rename with new file name and correct extension
//                    FileInfo fileInfo = new FileInfo(savedFile);
//                    fileInfo.CopyTo(Path.Combine(path, newFile), true);
//                    fileInfo.Delete();
//
//                    return new HttpResponseMessage()
//                    {
//                        Content = new StringContent(string.Format("File saved in {0} as {1}.", folder, newFile))
//                    };
//                });

            #endregion

            #region RivDiv code, not used
            // Save file
            string path = HttpContext.Current.Server.MapPath(string.Format("~/{0}", folder));
            var provider = new MultipartFormDataStreamProvider(path);

            // Read the form data and return an async task.
            var task = Request.Content.ReadAsMultipartAsync(provider);

            // Log exceptions
            task.ContinueWith(t =>
                {
                    if (t.IsFaulted || t.IsCanceled)
                    {
                        // Log t.Exception
                        var error = "Error Occurred";

                    }
                });

            return task.ContinueWith<HttpResponseMessage>(t =>
                {
                var bodyPart = provider.BodyPartFileNames.FirstOrDefault();
                string savedFile = bodyPart.Value;
                string originalFile = bodyPart.Key.TrimStart('"').TrimEnd('"');
                string newFile = string.Format("{0}{1}", newFileName, Path.GetExtension(originalFile));

                // Copy file and rename with new file name and correct extension
                FileInfo file = new FileInfo(savedFile);
                file.CopyTo(Path.Combine(path, newFile), true);
                file.Delete();

                // Return ResponseMessage
                return new HttpResponseMessage()
                {
                    Content = new StringContent(string.Format("File saved in {0} as {1}.", folder, newFile))
                };
                }, TaskScheduler.FromCurrentSynchronizationContext());

            #endregion
        }
    }
}
