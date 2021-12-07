using EurekaClient.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Steeltoe.Common.Discovery;
using Steeltoe.Discovery;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

/*references:
-https://faun.pub/restful-web-api-using-c-net-core-3-1-with-sqlite-f020d76c9b89 
-https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-5.0&tabs=visual-studio
-https://docs.microsoft.com/en-us/aspnet/core/web-api/?view=aspnetcore-6.0
-https://github.com/dotnet/AspNetCore.Docs/tree/main/aspnetcore/web-api/index/samples/3.x
-https://www.vitoshacademy.com/c-create-a-web-api-with-asp-net-core-video/
*/

namespace EurekaClient.Controllers
{
    [ApiController] //From VS .Net template. ApiController attributes applies web API behavior
    [Route("[controller]")] 
    public class ReviewController : ControllerBase //Instantiating from Controllerbase (the base class for a MVC controller without view support)
    {

        #region Constructor

        private readonly DiscoveryHttpClientHandler _handler;
        private readonly HttpClient _httpClient; //The System.Net.Http.HttpClient class instance acts as a session to send HTTP requests. An HttpClient instance is a collection of settings applied to all requests executed by that instance.
        private readonly string baseDiscoveryUrl = "http://ReviewService/Review";

        public ReviewController(IDiscoveryClient client)
        {
            _handler = new DiscoveryHttpClientHandler(client);
            _httpClient = new HttpClient(_handler, false); // HttpClient is a class for sending HTTP requests and receiving HTTP responses from a resource identified by a URI.  It is intended to be instantiated once per application, rather than per-use
        }

        #endregion

        [HttpGet("{movie_id}")]
        public async Task<ActionResult<List<Review>>> GetMovieReviews(int movie_id)
        {
            var response = await _httpClient.GetFromJsonAsync<List<Review>>(baseDiscoveryUrl + "/" + movie_id.ToString());
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<Review>> AddMovieReview(ReviewLiteDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync(baseDiscoveryUrl, dto);
            var temp = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<Review>(temp);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Review>> UpdateMovieReview(int id, ReviewTextDto dto)
        {
            var response = await _httpClient.PutAsJsonAsync(baseDiscoveryUrl + "/" + id.ToString(), dto);
            if (response.IsSuccessStatusCode)
            {
                var temp = response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<Review>(temp);
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Review>> DeleteMovieReview(int id)
        {
            var response = await _httpClient.DeleteAsync(baseDiscoveryUrl + "/" + id.ToString());
            if (response.IsSuccessStatusCode)
            {
                var temp = response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject(temp);
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

    }
}
