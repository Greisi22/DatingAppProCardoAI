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
        [HttpPost("add")]
        public async Task<IActionResult> CreateFriendship([FromBody] FriendDto friendDto)
        {
            var authenticatedUser = await _userManager.GetUserAsync(User);
            if (authenticatedUser == null)
            {
                return BadRequest("User not found.");
            }

            var friendUser = await _userManager.FindByIdAsync(friendDto.friendReceiver);
            if (friendUser == null)
            {
                return BadRequest("Friend user not found.");
            }

            var existingFriendship = await _dataContext.UserFriendship
                .AnyAsync(uf => uf.UserId == authenticatedUser.Id && uf.Friendships.UserFriendships.Any(f => f.UserId == friendUser.Id));

            if (existingFriendship)
            {
                return BadRequest("Friendship already exists.");
            }

            var userFriendship = _mapper.Map<UserFriendship>(friendDto);
            userFriendship.UserId = authenticatedUser.Id;

            _dataContext.UserFriendship.Add(userFriendship);
            await _dataContext.SaveChangesAsync();

            return Ok("Friendship created successfully.");
        }

      
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFriends()
        {
            var authenticatedUser = await _userManager.GetUserAsync(User);
            if (authenticatedUser == null)
            {
                return BadRequest("User not found.");
            }

            
            var userFriendships = _dataContext.UserFriendship
                .Where(uf => uf.UserId == authenticatedUser.Id)
                .Include(uf => uf.Friendships.UserFriendships) 
                .ToList();

          
            var friendshipsResponse = new List<FriendshipsResponseDto>();

            
            foreach (var userFriendship in userFriendships)
            {
               
                var userId = userFriendship.UserId;
                var friendIds = userFriendship.Friendships.UserFriendships
                    .Select(uf => uf.UserId) 
                    .ToList();


                foreach (var friendId in friendIds)
                {
                    if (userId != friendId)
                    {
                        var friendshipDto = new FriendshipsResponseDto
                        {
                            userId = userId,
                            friendId = friendId
                        };
                        friendshipsResponse.Add(friendshipDto);
                    }
                    

     
                }
            }

            return Ok(friendshipsResponse);
        }
       

    }
}
