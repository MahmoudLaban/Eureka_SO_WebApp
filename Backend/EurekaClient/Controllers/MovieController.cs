using EurekaClient.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Steeltoe.Common.Discovery;
using Steeltoe.Discovery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;


namespace EurekaClient.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly DiscoveryHttpClientHandler _handler;
        private readonly HttpClient _httpClient;
        private readonly string baseDiscoveryUrl = "http://MovieService/Movie";

        public MovieController(IDiscoveryClient client)
        {
            _handler = new DiscoveryHttpClientHandler(client);
            _httpClient = new HttpClient(_handler, false);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetAllMovies()
        {
            var response = await _httpClient.GetFromJsonAsync<IEnumerable<Movie>>(baseDiscoveryUrl);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
            var response = await _httpClient.GetAsync(baseDiscoveryUrl + "/" + id.ToString());
            if (response.IsSuccessStatusCode)
            {
                var temp = response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<Movie>(temp);
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult<Movie>> AddMovie(MovieRegisterDto movie)
        {
            var response = await _httpClient.PostAsJsonAsync(baseDiscoveryUrl, movie);
            var temp = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<Movie>(temp);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Movie>> CreateOrUpdateMovie(int id, MovieRegisterDto movie)
        {
            var response = await _httpClient.PutAsJsonAsync(baseDiscoveryUrl + "/" + id.ToString(), movie);
            var temp = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<Movie>(temp);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Movie>> DeleteMovie(int id)
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
