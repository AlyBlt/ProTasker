using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProTasker.Application.DTOs;
using ProTasker.Application.Helpers;
using ProTasker.Application.Interfaces.Services;
using ProTasker.Application.Services;
using ProTasker.Domain.Entities;
using ProTasker.Application.Exceptions;

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

        [Authorize(Policy = "AdminOnly")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var tasks = await _service.GetAllAsync();
            var dtos = _mapper.Map<IEnumerable<ProjectTaskDTO>>(tasks);
            return Ok(dtos);
        }

        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var task = await _service.GetByIdAsync(id);
            if (task == null)
                return NotFound(new { Message = $"Task with Id {id} could not be found." });

            var dto = _mapper.Map<ProjectTaskDTO>(task);
            return Ok(dto);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Consumes("application/json")]
        public async Task<IActionResult> Create([FromBody] ProjectTaskDTO taskDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var task = _mapper.Map<ProjectTask>(taskDto);

            try
            {
                await _service.AddAsync(task);
                // Task eklendikten sonra DB’den Include ile tekrar çekiyoruz
                var createdTask = await _service.GetByIdAsync(task.Id); // Service tarafında Include eklenmiş olmalı

                if (createdTask == null)
                    return BadRequest(new { Message = "Task could not be retrieved after creation." });

                var createdDto = _mapper.Map<ProjectTaskDTO>(createdTask);
                return CreatedAtAction(nameof(GetById), new { id = createdDto.Id }, createdDto);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }

           
        }



        [Authorize(Roles = "Admin,TeamLeader")]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Consumes("application/json")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] ProjectTaskDTO taskDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var task = _mapper.Map<ProjectTask>(taskDto);
            task.Id = id;

            try
            {
                await _service.UpdateAsync(task);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }


        [Authorize(Policy = "AdminOnly")]
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

