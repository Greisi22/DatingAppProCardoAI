using Microsoft.EntityFrameworkCore;

namespace DatingAppProCardoAI.Domain
{
    public class UserFriendship
    {
        
        public int Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public string friendId { get; set; }
        public ApplicationUser Friend { get; set; }
    }
}
