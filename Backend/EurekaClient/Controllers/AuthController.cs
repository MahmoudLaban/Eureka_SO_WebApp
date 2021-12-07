using EurekaClient.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Steeltoe.Common.Discovery;
using Steeltoe.Discovery;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

/*references:
-https://faun.pub/restful-web-api-using-c-net-core-3-1-with-sqlite-f020d76c9b89 
-https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-5.0&tabs=visual-studio
-https://docs.microsoft.com/en-us/aspnet/core/web-api/?view=aspnetcore-6.0
-https://github.com/dotnet/AspNetCore.Docs/tree/main/aspnetcore/web-api/index/samples/3.x
*/

namespace EurekaClient.Controllers
{
    [ApiController] //From VS .Net template. ApiController attributes applies web API behavior
    [Route("[controller]")]
    public class AuthController : ControllerBase //Instantiating from Controllerbase (the base class for a MVC controller without view support)
    {
       
        #region Constructor
        
        private readonly DiscoveryHttpClientHandler _handler;
        private readonly HttpClient _httpClient; //The HttpClient class instance acts as a session to send HTTP requests. An HttpClient instance is a collection of settings applied to all requests executed by that instance.
        private readonly string baseDiscoveryUrl = "http://AuthService/Auth";
        public AuthController(IDiscoveryClient client)
        {
            _handler = new DiscoveryHttpClientHandler(client);
            _httpClient = new HttpClient(_handler, false); // HttpClient is a class for sending HTTP requests and receiving HTTP responses from a resource identified by a URI.  It is intended to be instantiated once per application, rather than per-use
        }

        #endregion

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
