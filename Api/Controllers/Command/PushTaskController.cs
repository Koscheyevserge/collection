using Api.Core;
using Api.Core.Corezoid;
using Api.Interfaces;
using Api.Models.Command;
using Data.Contexts;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Api.Controllers.Command
{
    [Produces("application/json")]
    [Route("api/command/pushtask")]
    public class PushTaskController : Controller, ICommand<PushTaskCM>
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly CollectionContext _context;

        public PushTaskController(IHttpClientFactory httpClientFactory, CollectionContext context)
        {
            _httpClientFactory = httpClientFactory;
            _context = context;
        }

        private string GenerateSignature(int unixTime, string secret, string content)
        {
            var text = unixTime.ToString() + secret + content + secret;
            var hash = SHA1.Create();
            hash.ComputeHash(new UTF8Encoding().GetBytes(text));
            return BitConverter.ToString(hash.Hash).Replace("-", string.Empty).ToLower();
        }

        private int GetUnixTime()
        {
            return (int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }

        [HttpPost]
        public async Task<IActionResult> Do([FromBody]PushTaskCM model)
        {
            var login = 99336;
            var conv_id = 438025;
            var secret = "RCd4X3281GCYnohz478SvDuZwbmWzPItTNuxfqb0lCvtYxu8Gf";
            try
            {                
                var clients = _context.Clients.Where(c => !string.IsNullOrWhiteSpace(c.Code))
                    .Select(c => new CorezoidCreateTaskModel(null, conv_id, c.Code));
                var data = new { ops = clients.ToList() };
                var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                var time = GetUnixTime();
                var sign = GenerateSignature(time, secret, await content.ReadAsStringAsync());
                var create_task_url = $"https://www.corezoid.com/api/1/json/{login}/{time}/{sign}";
                var post = await _httpClientFactory.CreateClient().PostAsync(create_task_url, content);
                var key = Guid.NewGuid();
                return Ok(
                    new {
                        success = true,
                        key,
                        code = (int)post.StatusCode,
                        url = create_task_url,
                        request = data,
                        response = JsonConvert.DeserializeObject(post.Content.ReadAsStringAsync().Result)
                    }
                );
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, details = ex, code = 500 });
            }          
        }
        [HttpGet("{key}")]
        public Task<IActionResult> Undo(Guid key)
        {
            throw new NotImplementedException();
        }
    }
}
