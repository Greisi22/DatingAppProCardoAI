using System.ComponentModel.DataAnnotations;

namespace DatingAppProCardoAI.Dto
{
    public class FriendDto
    {
        [Required]
        public string friendReceiver { get; set; }
    }
}
