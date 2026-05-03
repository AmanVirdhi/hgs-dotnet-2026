using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HgsApi.Data;
using HgsApi.Models;

namespace HgsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public UserController(ApiDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            return Ok(users);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound("User not found");

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(User user)
        {
            //normalize email to lowercase
            var email = user.Email.ToLower();

            //duplicate check
            if (await _context.Users.AnyAsync(u => u.Email.ToLower() == email))
            {
                return BadRequest(new { message = "User already exists" });
            }

            //assign normalized email
            user.Email = email;

            //password hash
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return BadRequest(new { message = "User already exists" });
            }

            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, new {
                id = user.Id,
                username = user.Username,
                email = user.Email
            });
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateUser(int id, User updatedUser)
        {
            if (id != updatedUser.Id)
                return BadRequest();

            var existingUser = await _context.Users.FindAsync(id);
            if (existingUser == null)
                return NotFound();

            existingUser.Username = updatedUser.Username;
            existingUser.Email = updatedUser.Email;
            existingUser.Password = BCrypt.Net.BCrypt.HashPassword(updatedUser.Password);

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound("User not found");

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Email) ||
                string.IsNullOrWhiteSpace(dto.Password))
            {
                return BadRequest(new { message = "Email and password required" });
            }

            var email = dto.Email.ToLower();

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
                return Unauthorized(new { message = "Invalid email or password" });

            bool isValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.Password);

            if (!isValid)
                return Unauthorized(new { message = "Invalid email or password" });

            return Ok(new {
                id = user.Id,
                username = user.Username,
                email = user.Email
            });
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Signup(SignUpDto dto)
        {
            // basic validation
            if (string.IsNullOrWhiteSpace(dto.Email) ||
                string.IsNullOrWhiteSpace(dto.Password) ||
                string.IsNullOrWhiteSpace(dto.Username))
            {
                return BadRequest(new { message = "All fields are required" });
            }

            var email = dto.Email.ToLower();

            // duplicate check
            if (await _context.Users.AnyAsync(u => u.Email == email))
            {
                return BadRequest(new { message = "User already exists" });
            }

            // map DTO → Entity
            var user = new User
            {
                Username = dto.Username,
                Email = email,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // response (no password)
            return Ok(new {
                id = user.Id,
                username = user.Username,
                email = user.Email
            });
        }
    }
}
