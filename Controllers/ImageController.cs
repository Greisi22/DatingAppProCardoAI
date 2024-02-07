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
    public class ImageController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public ImageController(UserManager<IdentityUser> userManager, DataContext dataContext, IMapper mapper)
        {
            _userManager = userManager;
            _dataContext = dataContext;
            _mapper = mapper;
        }

        [HttpPost("Image")]
        public async Task<IActionResult> UploadImage([FromBody] ImageDto imagedto)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return BadRequest("User not found!");
            }

            var profile = await _dataContext.Profile.FirstOrDefaultAsync(p => p.UserId == user.Id);
            if (profile == null)
            {
                return BadRequest("Profile not found");
            }

            var image = _mapper.Map<Domain.Image>(imagedto);


            image.ProfileId = profile.Id;

            _dataContext.Image.Add(image);
            await _dataContext.SaveChangesAsync();

           
            return Ok(image.Id);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetImage(int id)
        {
            var image = await _dataContext.Image.FindAsync(id);
            if (image == null)
            {
                return NotFound("Image not found");
            }

            return Ok(image);
        }
        
    }
}