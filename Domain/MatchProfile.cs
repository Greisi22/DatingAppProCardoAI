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



        public bool IsMatch(Profile currentprofile, Profile otherProfile)
        {
            if (currentprofile == null || otherProfile == null)
            {
                return false;
            }


            string[] currentWords = currentprofile.Description.Split(' ');
            string[] otherWords = otherProfile.Preferences.Split(' ');

            foreach (string word in otherWords)
            {
                if (currentWords.Contains(word))
                {
                    return true;
                }
            }

            return false;

        }
    }
}
