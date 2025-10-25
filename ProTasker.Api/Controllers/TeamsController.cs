using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProTasker.Application.DTOs;
using ProTasker.Application.Interfaces;
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
        public async Task<IActionResult> GetAll()
        {
            var teams = await _service.GetAllTeamsAsync();
            var teamDtos = _mapper.Map<IEnumerable<TeamDTO>>(teams);
            return Ok(teamDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var team = await _service.GetTeamByIdAsync(id);
            if (team == null) return NotFound();
            var teamDto = _mapper.Map<TeamDTO>(team);
            return Ok(teamDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TeamDTO teamDto)
        {
            var team = _mapper.Map<Team>(teamDto); // DTO -> Entity dönüşümü
            await _service.AddTeamAsync(team);
            var createdTeamDto = _mapper.Map<TeamDTO>(team); // Entity -> DTO dönüşümü
            return CreatedAtAction(nameof(GetById), new { id = createdTeamDto.Id }, createdTeamDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] TeamDTO teamDto)
        {
            var team = await _service.GetTeamByIdAsync(id);
            if (team == null) return NotFound();
            team.Id = id; // Id'yi koruyoruz         
            _mapper.Map(teamDto, team); // DTO -> Entity dönüşümü
            await _service.UpdateTeamAsync(team);
            return NoContent();

            
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var team = await _service.GetTeamByIdAsync(id);
            if (team == null) return NotFound();
            await _service.DeleteTeamAsync(id);
            return NoContent();
        }
    }
}
