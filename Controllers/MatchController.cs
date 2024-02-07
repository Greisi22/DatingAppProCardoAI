﻿using DatingAppProCardoAI.Data;
using DatingAppProCardoAI.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



namespace DatingAppProCardoAI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class MatchController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly DataContext _dataContext;

        public MatchController(UserManager<IdentityUser> userManager, DataContext dataContext)
        {
            _userManager = userManager;
            _dataContext = dataContext;
        }

        [HttpPost("MatchProfile")]
        public async Task<IActionResult> CreateMatchProfile()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return BadRequest("User not found!");
            }

            var currentUserProfile = await _dataContext.Profile.FirstOrDefaultAsync(p => p.UserId == user.Id);
            if (currentUserProfile == null)
            {
                return BadRequest("Profile not found");
            }

            var allProfiles = await _dataContext.Profile.ToListAsync();

            MatchProfile newMatch = null; 

            foreach (var profile in allProfiles)
            {
                if (profile.Id != currentUserProfile.Id)
                {
                    var match = new MatchProfile(); 

                    if (match.IsMatch(profile))
                    {
                        newMatch = new MatchProfile 
                        {
                            ProfileId = profile.Id
                        };

                        _dataContext.MatchProfile.Add(newMatch); 
                    }
                }
            }

            await _dataContext.SaveChangesAsync();

            if (newMatch != null)
            {
                return Ok(newMatch.Id);
            }
            else
            {
                return NotFound("No matching profiles found.");
            }
        }




        [HttpGet("{id}")]
        public async Task<IActionResult> GetMatchProfile(int id)
        {
            var matchProfile = await _dataContext.MatchProfile.FindAsync(id);
            if (matchProfile == null)
            {
                return NotFound("MatchProfile not found");
            }

            return Ok(matchProfile);
        }
    }
}