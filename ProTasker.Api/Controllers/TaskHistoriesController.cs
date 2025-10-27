using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProTasker.Application.DTOs;
using ProTasker.Application.Helpers;
using ProTasker.Application.Interfaces.Services;
using ProTasker.Domain.Entities;
using System.Threading.Tasks;

namespace ProTasker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskHistoriesController : ControllerBase
    {
        private readonly ITaskHistoryService _service;
        private readonly IMapper _mapper;
        public TaskHistoriesController(ITaskHistoryService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        //GET: api/TaskHistories
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var histories = await _service.GetAllAsync(); 
            var historyDtos = _mapper.Map<IEnumerable<TaskHistoryDTO>>(histories); // Entity -> DTO
            return Ok(historyDtos);
        }

        //GET: api/TaskHistories/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var history = await _service.GetByIdAsync(id); 
            if (history == null) return NotFound(new { Message = "The task history with the provided Id could not be found." });
            var historyDto = _mapper.Map<TaskHistoryDTO>(history);
            return Ok(historyDto);
        }

        //POST: api/TaskHistories
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [Consumes("application/json")]
        public async Task<IActionResult> Create([FromBody] TaskHistoryDTO historyDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var history = _mapper.Map<TaskHistory>(historyDto); // DTO -> Entity
            await _service.AddAsync(history); 
            var createdHistoryDto = _mapper.Map<TaskHistoryDTO>(history); // Entity -> DTO
            return CreatedAtAction(nameof(GetById), new { id = createdHistoryDto.Id }, createdHistoryDto);
        }


        //PUT: api/TaskHistories/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Consumes("application/json")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] TaskHistoryDTO historyDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var history = await _service.GetByIdAsync(id); 
            if (history == null) return NotFound(new { Message = "The task history with the provided Id could not be found." });

            history.Id = id; // Preserve the existing Id
            _mapper.Map(historyDto, history); // DTO -> Entity
            await _service.UpdateAsync(history); 
            return NoContent();
        }

        //DELETE: api/TaskHistories/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deleted = await _service.DeleteAsync(id); 
            if (!deleted)
                return NotFound(new { Message = $"Task history with Id {id} could not be deleted because it does not exist." });
            return NoContent();
        }
    }
}

