using ReviewService.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ReviewService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public ReviewController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet("{movie_id}")]
        public async Task<ActionResult<List<Review>>> GetMovieReviews(int movie_id)
        {
            return await _context.Reviews.Where(x => x.movie_id == movie_id).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Review>> AddMovieReview(ReviewLiteDto dto)
        {
            var newReview = new Review
            {
                user_id = dto.user_id,
                movie_id = dto.movie_id,
                review_text = dto.review_text
            };
            await _context.Reviews.AddAsync(newReview);
            await _context.SaveChangesAsync();
            return newReview;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Review>> UpdateMovieReview(int id, ReviewTextDto dto)
        {
            var reviewExist = await _context.Reviews.FindAsync(id);
            if(reviewExist == null)
            {
                return NotFound();
            }
            else
            {
                reviewExist.review_text = dto.review_text;
                _context.Reviews.Update(reviewExist);
                await _context.SaveChangesAsync();
                return reviewExist;
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Review>> DeleteMovieReview(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }
            else
            {
                _context.Reviews.Remove(review);
                await _context.SaveChangesAsync();
                return Ok();
            }
        }
    }
}
