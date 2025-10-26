using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProTasker.Application.DTOs;
using ProTasker.Application.Helpers;
using ProTasker.Application.Interfaces;
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
        public async Task<IActionResult> GetAll()
        {
            var histories = await _service.GetAllHistoriesAsync();
            var historyDtos = _mapper.Map<IEnumerable<TaskHistoryDTO>>(histories); // Entity -> DTO dönüşümü
            return Ok(historyDtos);
        }

        //GET: api/TaskHistories/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var history = await _service.GetHistoryByIdAsync(id);
            if (history == null) return NotFound();
            var historyDto = _mapper.Map<TaskHistoryDTO>(history);
            return Ok(historyDto);
        }

        //POST: api/TaskHistories
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TaskHistoryDTO historyDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //-----------mapping profile'a alındı---------------
            //historyDto.PerformedByUserName = StringHelpers.CapitalizeWords(historyDto.PerformedByUserName);
         
            var history = _mapper.Map<TaskHistory>(historyDto); // DTO -> Entity dönüşümü
            await _service.AddHistoryAsync(history);
            var createdHistoryDto = _mapper.Map<TaskHistoryDTO>(history); // Entity -> DTO dönüşümü
            return CreatedAtAction(nameof(GetById), new { id = createdHistoryDto.Id }, createdHistoryDto);

        }

        //PUT: api/TaskHistories/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] TaskHistoryDTO historyDto)
        {
            var history = await _service.GetHistoryByIdAsync(id);
            if (history == null) return NotFound();
            history.Id = id;

           //historyDto.PerformedByUserName = StringHelpers.CapitalizeWords(historyDto.PerformedByUserName);

            _mapper.Map(historyDto, history); // DTO -> Entity dönüşümü
            await _service.UpdateHistoryAsync(history);
            return NoContent();

        }

        //DELETE: api/TaskHistories/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var history = await _service.GetHistoryByIdAsync(id);
            if (history == null) return NotFound();
            await _service.DeleteHistoryAsync(id);
            return NoContent();
        }
    }
}

