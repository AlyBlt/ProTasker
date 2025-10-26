using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProTasker.Application.DTOs;
using ProTasker.Application.Helpers;
using ProTasker.Application.Interfaces;
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
        public async Task<IActionResult> GetAll()
        {
            var tasks = await _service.GetAllTasksAsync();
            var taskDtos = _mapper.Map<IEnumerable<ProjectTaskDTO>>(tasks); // Entity -> DTO dönüşümü
            return Ok(taskDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var task = await _service.GetTaskByIdAsync(id);
            if (task == null) return NotFound();
            var taskDto = _mapper.Map<ProjectTaskDTO>(task);
            return Ok(taskDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProjectTaskDTO taskDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //-----------mapping profile'a alındı---------------
            //taskDto.Title = StringHelpers.CapitalizeWords(taskDto.Title);
            //taskDto.AssignedUserName = StringHelpers.CapitalizeWords(taskDto.AssignedUserName);
            //taskDto.TeamName = StringHelpers.CapitalizeWords(taskDto.TeamName);
            //taskDto.Description = StringHelpers.Capitalize(taskDto.Description);

            var task = _mapper.Map<ProjectTask>(taskDto);   // DTO -> Entity
            await _service.AddTaskAsync(task);     // Entity repository’ye gidiyor
            var createdtaskDto = _mapper.Map<ProjectTaskDTO>(task); // Entity -> DTO
            return CreatedAtAction(nameof(GetById), new { id = createdtaskDto.Id }, createdtaskDto);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] ProjectTaskDTO taskDto)
        {
            var task = await _service.GetTaskByIdAsync(id);
            if (task == null) return NotFound();

            //taskDto.Title = StringHelpers.CapitalizeWords(taskDto.Title);
            //taskDto.AssignedUserName = StringHelpers.CapitalizeWords(taskDto.AssignedUserName);
            //taskDto.TeamName = StringHelpers.CapitalizeWords(taskDto.TeamName);
            //taskDto.Description = StringHelpers.Capitalize(taskDto.Description);

            task.Id = id; // Id'yi koruyoruz
            _mapper.Map(taskDto, task); // DTO -> Entity dönüşümü
            await _service.UpdateTaskAsync(task);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var task = await _service.GetTaskByIdAsync(id);
            if (task == null) return NotFound();
            await _service.DeleteTaskAsync(id);
            return NoContent();
        }
    }
}

