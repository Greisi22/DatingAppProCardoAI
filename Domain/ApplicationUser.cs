using Microsoft.AspNetCore.Identity;

namespace DatingAppProCardoAI.Domain
{
    public class ApplicationUser : IdentityUser
    {

        public ICollection<UserFriendship> UserFriendships { get; set; }
    }
}
