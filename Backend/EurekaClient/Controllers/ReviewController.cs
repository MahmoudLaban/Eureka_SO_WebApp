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


namespace EurekaClient.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly DiscoveryHttpClientHandler _handler;
        private readonly HttpClient _httpClient;
        private readonly string baseDiscoveryUrl = "http://ReviewService/Review";

        public ReviewController(IDiscoveryClient client)
        {
            _handler = new DiscoveryHttpClientHandler(client);
            _httpClient = new HttpClient(_handler, false);
        }

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
        public async Task<ActionResult<Review>> UpdateMovieReview(int id, string review_text)
        {
            var response = await _httpClient.PutAsJsonAsync(baseDiscoveryUrl + "/" + id.ToString(), review_text);
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
