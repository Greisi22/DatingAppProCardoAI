using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace DatingAppProCardoAI.Dto
{
    public class MessageDto
    {
        [Required]
        public string ReceiverId { get; set; }

        [Required]
        public string ContentOfMessage { get; set; }

        public DateTime timeSend { get; set; }
    }
}
