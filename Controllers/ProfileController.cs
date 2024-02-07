using AutoMapper;
using DatingAppProCardoAI.Data;
using DatingAppProCardoAI.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingAppProCardoAI.Controllers
{
   
    [ApiController]
    [Route("api/[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public ProfileController(UserManager<IdentityUser> userManager, DataContext dataContext, IMapper mapper)
        {
            _userManager = userManager;
            _dataContext = dataContext;
            _mapper = mapper;

        }


        
        [HttpPost("profile")]
        [Authorize]
        [Consumes("application/json")]
        public async Task<IActionResult> CreateProfile([FromBody] ProfileDto profiledto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return BadRequest("User Not found");
            }
        
            var existingProfile = await _dataContext.Profile.FirstOrDefaultAsync(p => p.UserId == user.Id);
            if (existingProfile != null)
            {
                return Conflict("Profile already exists");
            }

            var profile = _mapper.Map<Domain.Profile>(profiledto);

            profile.UserId = user.Id;
            profile.UserName = user.UserName;

            _dataContext.Profile.Add(profile);
            await _dataContext.SaveChangesAsync();


            return Ok(profile.Id);
         
        }

        [HttpGet("myprofile")]
        public async Task<IActionResult> GetMyProfile()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return BadRequest("User Not found");
            }

            var profile = await _dataContext.Profile.FirstOrDefaultAsync(p => p.UserId == user.Id);
            if (profile == null)
            {
                return NotFound("Profile not found");
            }

            return Ok(profile);
        }
    }
}
