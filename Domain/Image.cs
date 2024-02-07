using System.ComponentModel.DataAnnotations;

namespace DatingAppProCardoAI.Domain
{
    public class Image
    {

        public int Id { get; set; }
        public string Description { get; set; }

        [Required]
        public string ImageFileName { get; set; }

        public DateOnly publishedDate { get; set; }

        public int ProfileId { get; set; }
        public Profile Profile { get; set; }
    }
}
