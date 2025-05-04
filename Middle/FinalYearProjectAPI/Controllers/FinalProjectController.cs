using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FinalYearProjectAPI.Controllers
{
    [Route("enhance")]
    [ApiController]
    public class FinalProjectController : ControllerBase
    {
        [HttpPost("enlighten")]
        public async Task<FileStreamResult> enlighten([FromForm]IFormFile inputImage)
        {
            using (var stream = System.IO.File.Create("F:\\Old Desktop\\FinalYearProjectAPI\\Temp\\" + inputImage.FileName))
            {
                await inputImage.CopyToAsync(stream);
            }
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = "F:\\Old Desktop\\PycharmProjects\\FinalYearProject\\venv\\Scripts\\python.exe";
            start.Arguments = "F:\\Old Desktop\\PycharmProjects\\FinalYearProject\\main.py " + "F:\\\\Old Desktop\\\\FinalYearProjectAPI\\\\Temp\\\\" + inputImage.FileName + " F:\\\\Old Desktop\\\\FinalYearProjectAPI\\\\Temp\\\\output_" + inputImage.FileName;
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            using (Process process = Process.Start(start))
            {
                using (StreamReader sr = process.StandardOutput)
                {
                    string res = sr.ReadToEnd();
                    System.Diagnostics.Debug.WriteLine(res);
                }
                process.WaitForExit();
            }
            return new FileStreamResult(System.IO.File.OpenRead("F:\\Old Desktop\\FinalYearProjectAPI\\Temp\\output_" + inputImage.FileName), new MediaTypeHeaderValue("image/png"));

        }

    }
}
