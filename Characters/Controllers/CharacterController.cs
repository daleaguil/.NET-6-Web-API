using Characters.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Characters.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private readonly DataContext _context;
        public CharacterController(DataContext context)
        {
            _context = context;
        }

        private static List<Character> characters = new List<Character>
        {
            new Character
            {
                Id = 1,
                FirstName = "Tony",
                LastName = "Stark",
                Age = 40
            },
            new Character
            {
                Id = 2,
                FirstName = "Killua",
                LastName = "Zoldyck",
                Age = 12
            },
            new Character
            {
                Id = 3,
                FirstName = "Tanjiro",
                LastName = "Kamado",
                Age = 13
            }
        };

        [HttpGet]
        public async Task<ActionResult<List<Character>>> GetCharacters()
        {
            return Ok(await _context.Characters.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Character>> GetCharacter(int id)
        {
            var character = await _context.Characters.FindAsync(id);

            if (character == null)
                return BadRequest("Character not found.");

            return Ok(character);
        }

        [HttpPost]
        public async Task<ActionResult<List<Character>>> CreateCharacter(Character character)
        {
            _context.Characters.Add(character);
            await _context.SaveChangesAsync();

            return Ok(await _context.Characters.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Character>>> UpdateCharacter(Character request)
        {
            var character = await _context.Characters.FindAsync(request.Id);

            if (character == null)
                return BadRequest("Character not found.");

            character.FirstName = request.FirstName;
            character.LastName = request.LastName;
            character.Age = request.Age;

            await _context.SaveChangesAsync();

            return Ok(await _context.Characters.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Character>>> DeleteCharacter(int id)
        {
            var character = await _context.Characters.FindAsync(id);

            if (character == null)
                return BadRequest("Character not found.");

            _context.Characters.Remove(character);
            await _context.SaveChangesAsync();

            return Ok(await _context.Characters.ToListAsync());
        }
    }
}
