using DatingAppProCardoAI.Domain;
using FluentValidation;

namespace DatingAppProCardoAI.Validations
{
    public class ProfileValidator : AbstractValidator<Profile>
    {
        public ProfileValidator() 
        {

            RuleFor(profile => profile.UserName).NotEmpty().
                WithMessage("Username is required.").
                MaximumLength(50).
                WithMessage("Username must not exceed 50 characters.");

            RuleFor(profile => profile.Hobbies).Must(hobbies => hobbies != null && hobbies.Length > 0).
                WithMessage("Hobbies are required.").
                Must(hobbies => hobbies.Length > 3)
               .WithMessage("There shouldn't be less than 3 hobbies");

            RuleFor(profile => profile.Description).NotEmpty().
                WithMessage("Description is required.").
                MaximumLength(255).
                WithMessage("Description must not exceed 255 characters.");


            RuleFor(profile => profile.Preferences).NotEmpty().
                WithMessage("Preferences is required.").
                MaximumLength(255).
                WithMessage("Preferences must not exceed 255 characters.");
        }
    }
}
