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
        public async Task<IActionResult> GetAll()
        {
            var users = await _service.GetAllUsersAsync();
            var userDto = _mapper.Map<List<UserDTO>>(users);
            return Ok(userDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _service.GetUserByIdAsync(id);
            if (user == null) return NotFound();
            var userDto = _mapper.Map<UserDTO>(user);
            return Ok(userDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserDTO userDto)
        {
            var user = _mapper.Map<User>(userDto);   // DTO -> Entity
            await _service.AddUserAsync(user);       // Entity repository’ye gidiyor
            var createdUserDto = _mapper.Map<UserDTO>(user); // Entity -> DTO
            return CreatedAtAction(nameof(GetById), new { id = createdUserDto.Id }, createdUserDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UserDTO userDto)
        {
            var user = await _service.GetUserByIdAsync(id);
            if (user == null) return NotFound();
            user.Id = id; // Id'yi koruyoruz
            _mapper.Map(userDto, user); //// sadece var olan entity üzerine map et
            await _service.UpdateUserAsync(user);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await _service.GetUserByIdAsync(id);
            if (user == null) return NotFound();
            await _service.DeleteUserAsync(id);
            return NoContent();
        }
    }
}
