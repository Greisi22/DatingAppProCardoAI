using Microsoft.AspNetCore.Identity;

namespace DatingAppProCardoAI.Domain
{
    public class Profile
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }

        public string[] Hobbies { get; set; } 
        public string Description { get; set; } 
        public string Preferences { get; set; } 
        public IdentityUser User { get; set; }
        public ICollection<Image>  images { get; set; }
    }
}
