using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ProjektNeveBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        IWebHostEnvironment _env;
        public FileUploadController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [Route("BackEndServer")]
        [HttpPost]

        public IActionResult FileUploadBackEnd()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string fileName = postedFile.FileName;
                string subFolder = "/Files/";
                var filePath = _env.ContentRootPath + subFolder + fileName;
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }
                return Ok(fileName);
            }
            catch (Exception)
            {
                return Ok("default.jpg");
            }
        }


        [Route("FtpServer")]
        [HttpPost]

        public async Task<IActionResult> FileUploadFtp()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string fileName = postedFile.FileName;
                string subFolder = ""; // innen kivettünk dolgokat :)

                var url = "ftp://ftp.nethely.hu" + subFolder + "/" + fileName;
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(url);
                request.Credentials = new NetworkCredential("balazska", "Balazska");
                request.Method = WebRequestMethods.Ftp.UploadFile;
                await using (Stream ftpStream = request.GetRequestStream())
                {
                    postedFile.CopyTo(ftpStream);
                }
                return Ok(fileName);

            }
            catch (Exception)
            {
                return Ok("default.jpg");
            }
        }
    }
}
