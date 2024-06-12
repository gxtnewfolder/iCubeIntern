using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iCubeTrain.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace iCubeTrain.Controllers
{
    [ApiController]
    [Route("api/ftp")]
    public class FTPController : ControllerBase
    {
        private readonly IFTPService _ftpService;

        public FTPController(IFTPService ftpService)
        {
            _ftpService = ftpService;
        }

        [HttpGet("connection")]
        public async Task<IActionResult> TestConnection([FromQuery] string ftpServerUrl, [FromQuery] string ftpUsername = null, [FromQuery] string ftpPassword = null)
        {
            var isConnected = await _ftpService.TestConnectionAsync(ftpServerUrl, ftpUsername, ftpPassword);
            return Ok(new { isConnected });
        }

        [HttpGet("list")]
        public async Task<IActionResult> ListDirectory([FromQuery] string ftpServerUrl, [FromQuery] string path, [FromQuery] string ftpUsername = null, [FromQuery] string ftpPassword = null)
        {
            var result = await _ftpService.ListDirectoryAsync(ftpServerUrl, path, ftpUsername, ftpPassword);
            return Ok(result);
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile([FromQuery] string ftpServerUrl, [FromQuery] string path, [FromQuery] string ftpUsername = null, [FromQuery] string ftpPassword = null)
        {
            using var stream = new MemoryStream();
            await Request.Body.CopyToAsync(stream);
            stream.Seek(0, SeekOrigin.Begin);

            await _ftpService.UploadFileAsync(ftpServerUrl, path, stream, ftpUsername, ftpPassword);
            return Ok();
        }
    }
}