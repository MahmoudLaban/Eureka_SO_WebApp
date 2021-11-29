﻿using AuthService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly AppSettings _appSettings;
        public AuthController(ApplicationDBContext context, IOptions<AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.Value;
        }

        [HttpPost("auth-user")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var user = _context.Users.Where(x => x.username == model.Username && x.password == model.Password).FirstOrDefault();

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });
            
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var authResponse = new AuthenticateResponse(user, tokenHandler.WriteToken(token));
            return Ok(authResponse);
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(RegisterUserDto model)
        {
            var success = false;
            var user = _context.Users.Where(x => x.username == model.Username).FirstOrDefault();
            if(user == null)
            {
                var newUser = new User
                {
                    username = model.Username,
                    password = model.Password,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email
                };
                await _context.Users.AddAsync(newUser);
                await _context.SaveChangesAsync();
                success = true;
            }
            return Ok(new { success = success });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserDetail(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }
    }
}