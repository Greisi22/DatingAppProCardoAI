using DatingAppProCardoAI.Domain;
using FluentValidation;

namespace DatingAppProCardoAI.Validations
{
    public class ImageValidator : AbstractValidator<Image>
    {
         
         public ImageValidator()
        {

            RuleFor(image => image.publishedDate).NotEmpty()
                .WithMessage("Published date is required.");

            RuleFor(image => image.MemorySize)
           .GreaterThan(0).WithMessage("Image size must be greater than 0 bytes.")
           .LessThanOrEqualTo(10 * 1024 * 1024).WithMessage("Image size cannot exceed 10 MB.");

            RuleFor(image => image.ImageFileName).NotEmpty()
                .WithMessage("You should Upload an Image.");
        }
    }
}
