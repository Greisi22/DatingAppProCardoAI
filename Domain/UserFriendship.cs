namespace DatingAppProCardoAI.Domain
{
    public class UserFriendship
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public int FriendshipsId { get; set; }
        public Friendships Friendships { get; set; }
    }
}
