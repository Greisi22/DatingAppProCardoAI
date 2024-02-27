namespace DatingAppProCardoAI.Domain
{
    public class Friendships
    {
        public int Id { get; set; }
        public ICollection<UserFriendship> UserFriendships { get; set; }
    }
}
