using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HgsApi.Data;
using HgsApi.Models;

namespace HgsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HgsInfoController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public HgsInfoController(ApiDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetHgsInfos()
        {
            var hgsInfos = await _context.HgsInfos.ToListAsync();
            return Ok(hgsInfos);
        }

        [HttpGet("user/{email}")]
        public async Task<IActionResult> GetHgsInfosByEmail(string email)
        {
            var hgsInfos = await _context.HgsInfos
                .Where(h => h.UserEmail == email)
                .ToListAsync();
            return Ok(hgsInfos);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetHgsInfoById(int id)
        {
            var hgsInfo = await _context.HgsInfos.FindAsync(id);
            if (hgsInfo == null)
                return NotFound("HgsInfo not found");

            return Ok(hgsInfo);
        }

        [HttpPost]
        public async Task<IActionResult> CreateHgsInfo(HgsInfo hgsInfo)
        {
            _context.HgsInfos.Add(hgsInfo);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetHgsInfoById), new { id = hgsInfo.Id }, hgsInfo);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateHgsInfo(int id, HgsInfo updatedHgsInfo)
        {
            if (id != updatedHgsInfo.Id)
                return BadRequest();

            var existing = await _context.HgsInfos.FindAsync(id);
            if (existing == null)
                return NotFound();

            existing.Name = updatedHgsInfo.Name;
            existing.Grievancetypes = updatedHgsInfo.Grievancetypes;
            existing.Room = updatedHgsInfo.Room;
            existing.Course = updatedHgsInfo.Course;
            existing.Mobile = updatedHgsInfo.Mobile;
            existing.Description = updatedHgsInfo.Description;
            existing.UserEmail = updatedHgsInfo.UserEmail;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteHgsInfo(int id)
        {
            var hgsInfo = await _context.HgsInfos.FindAsync(id);
            if (hgsInfo == null)
                return NotFound("HgsInfo not found");

            _context.HgsInfos.Remove(hgsInfo);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}