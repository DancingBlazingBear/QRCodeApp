using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QRCodeApp2.Models;
using RestSharp;

namespace QRCodeApp2.Controllers
{
    public class UploadFileController : Controller
    {
        [HttpPost("upload")]
        public async Task<IActionResult> Index(IFormFile formFile)
        {
            if (formFile.Length < 0 || formFile.Length > 1048576) return BadRequest();

            var filePath = Path.GetTempFileName();

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await formFile.CopyToAsync(stream);
            }

            var response = ReadQR(filePath);
            
            return Ok(response);
        }

        private string ReadQR(string filePath)
        {
            var client = new RestClient("http://api.qrserver.com/v1/read-qr-code/");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-type", "multipart/form-data");
            request.AddFile("file", filePath);
            IRestResponse response = client.Execute(request);

            var json = JsonConvert.DeserializeObject<List<RootObject>>(response.Content);
            var result = json[0].Symbol[0].Data;
            
            return result;
        }
    }
}