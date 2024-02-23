using AutoMapper;
using DatingAppProCardoAI.Data;

using DatingAppProCardoAI.Dto;
using DatingAppProCardoAI.Validations;
using FluentValidation.Results;
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


            var validator = new MessageValidator();
            ValidationResult result = validator.Validate(message);

            if (!result.IsValid)
            {
                var errors = result.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(new { errors });
            }



            _dataContext.Message.Add(message);
            await _dataContext.SaveChangesAsync();

            return Ok(message.Id);
        }

        [HttpGet("")]
        public async Task<IActionResult> GetChatMessages(string receiverId)
        {
            var sender = await _userManager.GetUserAsync(User);
            if (sender == null)
            {
                return BadRequest("User not found!");
            }

            var senderId = sender.Id;

            var receiver = await _dataContext.Users.FirstOrDefaultAsync(u => u.Id == receiverId);
            var receiverName = receiver?.UserName;
            var senderName = sender.UserName;

            var chatMessages = await _dataContext.Message
                .Include(x => x.Sender)
                .Include(x => x.Receiver)
                .Where(m => (m.SenderId == senderId && m.ReceiverId == receiverId) ||
                            (m.SenderId == receiverId && m.ReceiverId == senderId))
                .OrderBy(m => m.timeSend)
                .ToListAsync();

            var messageResponseDtos = _mapper.Map<List<MessageResponseDto>>(chatMessages);

             foreach (var messageResponseDto in messageResponseDtos)
             {
                messageResponseDto.ReceiverName = receiverName;
                messageResponseDto.SenderName = senderName;
             }
            return Ok(messageResponseDtos);
        }

           




    }
}
