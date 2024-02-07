using System.ComponentModel.DataAnnotations;

namespace DatingAppProCardoAI.Dto
{
    public class ImageDto
    {
        public string Description { get; set; }

        [Required]
        public string ImageFileName { get; set; }

        public DateOnly publishedDate {  get; set; }
    }
}
