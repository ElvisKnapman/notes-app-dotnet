using FluentValidation;
using NotesApp.Api.DTOs.Requests.Notes;

namespace NotesApp.Api.Validators;

public class CreateNoteRequestValidator : AbstractValidator<CreateNoteRequest>
{
    public CreateNoteRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MinimumLength(5).WithMessage("Title must be at least 5 characters long.");

        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("Content is required");
    }
}

