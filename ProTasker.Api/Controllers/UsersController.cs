using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProTasker.Application.DTOs;
using ProTasker.Application.Interfaces.Services;
using ProTasker.Application.Models;  // ApplicationUser'ı buradan alıyoruz
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

        //Sadece Admin ve TeamLeader kullanıcıları tüm kullanıcıları listeleyebilsin
        [Authorize(Policy = "AdminOnly")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var users = await _service.GetAllAsync();
            var userDto = _mapper.Map<IEnumerable<UserDTO>>(users);
            return Ok(userDto);
        }

        //Herhangi bir giriş yapmış kullanıcı kendi profilini görüntüleyebilsin
        [Authorize]
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

        //Sadece Admin kullanıcılar yeni kullanıcı oluşturabilsin
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Consumes("application/json")]
        public async Task<IActionResult> Create([FromBody] UserDTO userDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = _mapper.Map<ApplicationUser>(userDto);
            await _service.AddAsync(user);
            var createdUserDto = _mapper.Map<UserDTO>(user);
            return CreatedAtAction(nameof(GetById), new { id = createdUserDto.Id }, createdUserDto);
        }

        //Admin veya TeamLeader kullanıcıları güncelleme yapabilsin
        [Authorize(Policy = "TeamLeaderOnly")]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

        //Yalnızca Admin kullanıcılar silebilsin
        [Authorize(Policy = "AdminOnly")]
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