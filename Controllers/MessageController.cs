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
        public async Task<IActionResult> GetChatMessages(String receiverId)
        {
            var sender = await _userManager.GetUserAsync(User);
            if (sender == null)
            {
                return BadRequest("User not found!");
            }

            var senderId = sender.Id;

            var chatMessages = await _dataContext.Message
                .Where(m => (m.SenderId == senderId && m.ReceiverId == receiverId) ||
                            (m.SenderId == receiverId && m.ReceiverId == senderId))
                .OrderBy(m => m.timeSend)
                .ToListAsync();


            var chatMessageContents = new List<string>();

            foreach (var chatMessage in chatMessages)
            {
                chatMessageContents.Add(chatMessage.ContentOfMessage + " " + chatMessage.timeSend);
            }
            return Ok(chatMessageContents);

        }



    }
}
