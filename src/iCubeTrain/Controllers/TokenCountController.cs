using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iCubeTrain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace iCubeTrain.Controllers
{
    public class TokenCountController : ControllerBase
    {
        private readonly TokenCountService _tokenCountService;
        private readonly IHubContext<TokenCountHub> _hubContext;

        public TokenCountController(TokenCountService tokenCountService, IHubContext<TokenCountHub> hubContext)
        {
            _tokenCountService = tokenCountService;
            _hubContext = hubContext;
        }

        [HttpPost("count")]
        public async Task<IActionResult> CountTokens([FromBody] TokenRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.Text))
            {
                return BadRequest("Text is required.");
            }

            try
            {
                var tokenCount = await _tokenCountService.GetTokenCountAsync(request.Text);
                return Ok(new { tokenCount });
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        public class TokenRequest
        {
            public string Text { get; set; }
        }

        public class TokenCountHub : Hub
        {

        }
    }
}