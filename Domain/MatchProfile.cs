using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace DatingAppProCardoAI.Domain
{
    public class MatchProfile
    {
        public int Id { get; set; }

        [Required]
        public int CurrentProfileId { get; set; }
        public Profile currentprofile { get; set; }

        public int MatchProfileId { get; set; }
        public Profile matchprofile { get; set; }



        public bool IsMatch(Profile currentUserProfile, Profile otherProfile)
        {
            if (currentUserProfile == null || otherProfile == null)
            {
                return false;
            }


            if (currentUserProfile.Preferences.SequenceEqual(otherProfile.Description))
            {
                return true;
            }

            return false;
        }

    }
}
