using AutoMapper;
using DatingAppProCardoAI.Data;
using DatingAppProCardoAI.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingAppProCardoAI.Controllers
{

    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;


        public MessageController(UserManager<IdentityUser> userManager, DataContext dataContext, IMapper mapper)
        {
            _userManager = userManager;
            _dataContext = dataContext;
            _mapper = mapper;
        }


        [HttpPost("Message")]
        public async Task<IActionResult> CreateMessage([FromBody] MessageDto messagedto)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return BadRequest("User not found!");
            }

            var message = _mapper.Map<Domain.Message>(messagedto);

            message.SenderId = user.Id;

            _dataContext.Message.Add(message);
            await _dataContext.SaveChangesAsync();

            return Ok(message.Id);
        }

        [HttpGet("Message")]
        public async Task<IActionResult> GetImage(int id)
        {
            var message = await _dataContext.Message.FindAsync(id);
            if (message == null)
            {
                return NotFound("Image not found");
            }

            return Ok(message);
        }



    }
}
