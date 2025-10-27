using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProTasker.Application.DTOs;
using ProTasker.Application.Services;
using ProTasker.Domain.Entities;
using System.Threading.Tasks;
using ProTasker.Application.Helpers;
using ProTasker.Application.Interfaces.Services;

namespace ProTasker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly IMapper _mapper;

        public UsersController(IUserService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var users = await _service.GetAllAsync();
            var userDto = _mapper.Map<IEnumerable<UserDTO>>(users);
            return Ok(userDto);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var user = await _service.GetByIdAsync(id);
            if (user == null) return NotFound(new { Message = "The user with the provided Id could not be found." });
            var userDto = _mapper.Map<UserDTO>(user);
            return Ok(userDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [Consumes("application/json")]
        public async Task<IActionResult> Create([FromBody] UserDTO userDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = _mapper.Map<User>(userDto);
            await _service.AddAsync(user);
            var createdUserDto = _mapper.Map<UserDTO>(user);
            return CreatedAtAction(nameof(GetById), new { id = createdUserDto.Id }, createdUserDto);
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Consumes("application/json")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UserDTO userDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _service.GetByIdAsync(id);
            if (user == null) return NotFound(new { Message = "The user with the provided Id could not be found." });

            user.Id = id; // Ensure we preserve the ID  
            _mapper.Map(userDto, user);
            await _service.UpdateAsync(user);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted)
                return NotFound(new { Message = $"User with Id {id} could not be deleted because it does not exist." });
            return NoContent();
        }
    }
}
