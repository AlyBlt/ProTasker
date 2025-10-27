using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProTasker.Application.DTOs;
using ProTasker.Application.Helpers;
using ProTasker.Application.Interfaces.Services;
using ProTasker.Application.Services;
using ProTasker.Domain.Entities;
using System.Threading.Tasks;

namespace ProTasker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly ITeamService _service;
        private readonly IMapper _mapper;

        public TeamsController(ITeamService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var teams = await _service.GetAllAsync();
            var teamDtos = _mapper.Map<IEnumerable<TeamDTO>>(teams);
            return Ok(teamDtos);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var team = await _service.GetByIdAsync(id);
            if (team == null) return NotFound(new { Message = "The team with the provided Id could not be found." });
            var teamDto = _mapper.Map<TeamDTO>(team);
            return Ok(teamDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [Consumes("application/json")]
        public async Task<IActionResult> Create([FromBody] TeamDTO teamDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var team = _mapper.Map<Team>(teamDto); // DTO -> Entity 
            await _service.AddAsync(team);
            var createdTeamDto = _mapper.Map<TeamDTO>(team); // Entity -> DTO 
            return CreatedAtAction(nameof(GetById), new { id = createdTeamDto.Id }, createdTeamDto);
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Consumes("application/json")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] TeamDTO teamDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var team = await _service.GetByIdAsync(id);
            if (team == null) return NotFound(new { Message = "The team with the provided Id could not be found." });

            team.Id = id; // Ensure we preserve the ID         
            _mapper.Map(teamDto, team); // DTO -> Entity 
            await _service.UpdateAsync(team);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted)
                return NotFound(new { Message = $"Team with Id {id} could not be deleted because it does not exist." });
            return NoContent();
        }
    }
}
