using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repository.Interface;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DifficultiesController : Controller
    {
        private readonly IDifficultyRepository _difficulty;
        private readonly IMapper _mapper;

        public DifficultiesController(IDifficultyRepository difficulty, IMapper mapper)
        {
            this._difficulty = difficulty;
            this._mapper = mapper;
        }

        [HttpGet()]
        public async Task<IActionResult> GetAll()
        {
            var difficultiesDomain = await _difficulty.GetAllAsync();

            if (difficultiesDomain.Count == 0)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<List<DifficultyDto>>(difficultiesDomain));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var difficulty = await _difficulty.GetByIdAsync(id);
            if (difficulty == null) { return NotFound(); }

            var difficultyDto = new DifficultyDto
            {
                Id = difficulty.Id,
                Name = difficulty.Name,
            };

            return Ok(difficultyDto);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] AddDifficultyRequestDto difficulty)
        {
            var difficultyDomain = _mapper.Map<Difficulty>(difficulty);

            var result = await _difficulty.CreateAsync(difficultyDomain);

            var difficultyDto = _mapper.Map<DifficultyDto>(result);

            //return CreatedAtAction(nameof(GetById), new { id = difficultyDto.Id }, difficultyDto);
            return Ok(difficultyDto);
        }
    }
}
