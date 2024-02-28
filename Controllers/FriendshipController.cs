using AutoMapper;
using DatingAppProCardoAI.Data;
using DatingAppProCardoAI.Domain;
using DatingAppProCardoAI.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace DatingAppProCardoAI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FriendshipController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        public FriendshipController(UserManager<IdentityUser> userManager, DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _userManager = userManager;
            _mapper = mapper;
        }

        [Authorize]
        [HttpPost("")]
        public async Task<IActionResult> createfriendship([FromBody] FriendDto frienddto)
        {
            var authenticateduser = await _userManager.GetUserAsync(User);
            if (authenticateduser == null)
            {
                return BadRequest("user not found.");
            }

            var frienduser = await _userManager.FindByIdAsync(frienddto.friendReceiver);
            if (frienduser == null)
            {
                return BadRequest("friend user not found.");
            }

            var existingfriendship = await _dataContext.UserFriendship
           .AnyAsync(uf => (uf.UserId == authenticateduser.Id && uf.friendId == frienduser.Id));


            if (existingfriendship)
            {
                return BadRequest("friendship already exists.");
            }

            var userfriendship = _mapper.Map<UserFriendship>(frienddto);
            userfriendship.UserId = authenticateduser.Id;

            _dataContext.UserFriendship.Add(userfriendship);
            await _dataContext.SaveChangesAsync();

            return Ok("friendship created successfully.");
        }

        [Authorize]
        [HttpGet("")]
        public async Task<IActionResult> GetFriends()
        {
            var authenticatedUser = await _userManager.GetUserAsync(User);
            if (authenticatedUser == null)
            {
                return BadRequest("User not found.");
            }

            var friendships = _dataContext.UserFriendship
                .Where(uf => uf.UserId == authenticatedUser.Id )
                .Select(uf => new FriendshipsResponseDto
                {
                    _userId =  authenticatedUser.Id ,
                    _friendId =  uf.friendId
                })
                .Distinct()
                .ToList();

            return Ok(friendships);
        }

    }
}
