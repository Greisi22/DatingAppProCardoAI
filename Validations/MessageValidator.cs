using DatingAppProCardoAI.Domain;
using FluentValidation;

namespace DatingAppProCardoAI.Validations
{
    public class MessageValidator : AbstractValidator<Message>
    {
        public MessageValidator() 
        {
            RuleFor(message => message.ContentOfMessage).NotEmpty()
                .WithMessage("You should fill the message field.");

            RuleFor(message => message.SenderId).NotEmpty()
                .WithMessage("You should log in first.");

            RuleFor(message => message.ReceiverId).NotEmpty()
                .WithMessage("You should send the message to someone.");
        }
    }
}
