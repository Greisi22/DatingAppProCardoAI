using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DatingAppProCardoAI.Domain
{
    public class Message
    {
        public int Id { get; set; }

        [Required]
        public string ContentOfMessage { get; set; }

        [Required]
        public string SenderId { get; set; }
        public IdentityUser Sender { get; set; }

        [Required]
        public string ReceiverId { get; set; }
        public IdentityUser Receiver { get; set; }

        public DateTime timeSend { get; set; }

    }
}