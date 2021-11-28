using MovieService.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MovieService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public MovieController(ApplicationDBContext context)
        {
            _context = context;
            
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetAllMovies()
        {
            return await _context.Movies.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return movie;
        }

        [HttpPost]
        public async Task<ActionResult<Movie>> AddMovie(MovieRegisterDto movie)
        {
            var newMovie = new Movie
            {
                Title = movie.Title,
                Genre = movie.Genre,
                Year = movie.Year
            };
            await _context.AddAsync(newMovie);
            await _context.SaveChangesAsync();
            return newMovie;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Movie>> CreateOrUpdateMovie(int id, MovieRegisterDto movie)
        {
            var movieExist = await _context.Movies.FindAsync(id);
            if(movieExist == null)
            {
                movieExist = new Movie
                {
                    Title = movie.Title,
                    Genre = movie.Genre,
                    Year = movie.Year
                };
                await _context.AddAsync(movieExist);
            }
            else
            {
                movieExist.Title = movie.Title;
                movieExist.Genre = movie.Genre;
                movieExist.Year = movie.Year;
                _context.Movies.Update(movieExist);
            }
            await _context.SaveChangesAsync();
            return movieExist;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Movie>> DeleteMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            else
            {
                _context.Movies.Remove(movie);
                await _context.SaveChangesAsync();
                return Ok();
            }
        }
    }
}
