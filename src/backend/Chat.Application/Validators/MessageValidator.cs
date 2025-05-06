using Chat.Application.Dtos;
using FluentValidation;

namespace Chat.Application.Validators;

public class MessageValidator : AbstractValidator<MessageDto>
{
    public MessageValidator()
    {
        RuleFor(message => message.Content)
            .NotEmpty()
                .WithMessage("Content cannot be empty.")
            .MaximumLength(300)
                .WithMessage("Content must not exceed three hundred characters.");

        RuleFor(message => message.CreatedAt)
            .NotEmpty()
                .WithMessage("Date cannot be empty.")
            .LessThanOrEqualTo(DateTime.UtcNow)
                .WithMessage("Date cannot be in the future.");
    }
}