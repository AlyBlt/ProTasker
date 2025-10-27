using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProTasker.Application.DTOs;
using ProTasker.Application.Helpers;
using ProTasker.Application.Interfaces.Services;
using ProTasker.Application.Services;
using ProTasker.Domain.Entities;

namespace ProTasker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectTasksController : ControllerBase
    {
        private readonly IProjectTaskService _service;
        private readonly IMapper _mapper;
        public ProjectTasksController(IProjectTaskService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var tasks = await _service.GetAllAsync();
            var taskDtos = _mapper.Map<IEnumerable<ProjectTaskDTO>>(tasks); // Entity -> DTO 
            return Ok(taskDtos);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var task = await _service.GetByIdAsync(id);
            if (task == null) return NotFound(new { Message = "The project task with the provided Id could not be found." });
            var taskDto = _mapper.Map<ProjectTaskDTO>(task);
            return Ok(taskDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [Consumes("application/json")]
        public async Task<IActionResult> Create([FromBody] ProjectTaskDTO taskDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var task = _mapper.Map<ProjectTask>(taskDto); // DTO -> Entity
            await _service.AddAsync(task); 
            var createdTaskDto = _mapper.Map<ProjectTaskDTO>(task); // Entity -> DTO
            return CreatedAtAction(nameof(GetById), new { id = createdTaskDto.Id }, createdTaskDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Consumes("application/json")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] ProjectTaskDTO taskDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var task = await _service.GetByIdAsync(id);
            if (task == null) return NotFound(new { Message = "The project task with the provided Id could not be found." });

            task.Id = id; // Preserve the existing Id
            _mapper.Map(taskDto, task); // DTO -> Entity dönüşümü
            await _service.UpdateAsync(task); 
            return NoContent();
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deleted = await _service.DeleteAsync(id); 
            if (!deleted)
                return NotFound(new { Message = $"Project task with Id {id} could not be deleted because it does not exist." });
            return NoContent();
        }
    }
}

