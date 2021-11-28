using EurekaClient.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Steeltoe.Common.Discovery;
using Steeltoe.Discovery;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace EurekaClient.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly DiscoveryHttpClientHandler _handler;
        private readonly HttpClient _httpClient;
        private readonly string baseDiscoveryUrl = "http://AuthService/Auth";
        public AuthController(IDiscoveryClient client)
        {
            _handler = new DiscoveryHttpClientHandler(client);
            _httpClient = new HttpClient(_handler, false);
        }

        [HttpPost("auth-user")]
        public async Task<ActionResult<AuthenticateResponse>> Authenticate(AuthenticateRequest model)
        {
            var response = await _httpClient.PostAsJsonAsync(baseDiscoveryUrl + "/auth-user", model);
            if (response.IsSuccessStatusCode)
            {
                var temp = response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<AuthenticateResponse>(temp);
                return Ok(result);
            }
            else
            {
                var temp = response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject(temp);
                return BadRequest(result);
            }
            
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(RegisterUserDto model)
        {
            var response = await _httpClient.PostAsJsonAsync(baseDiscoveryUrl, model);
            var temp = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<User>(temp);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserDetail(int id)
        {
            var response = await _httpClient.GetAsync(baseDiscoveryUrl + "/" + id.ToString());
            if (response.IsSuccessStatusCode)
            {
                var temp = response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<User>(temp);
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
