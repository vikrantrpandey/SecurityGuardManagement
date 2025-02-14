using FluentValidation;
using SecurityGuardManagement.Application.Common.Interfaces;

namespace SecurityGuardManagement.Application.Guards.Commands.CreateGuard;

public class CreateGuardCommandValidator : AbstractValidator<CreateGuardCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateGuardCommandValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required")
            .MaximumLength(50).WithMessage("First name must not exceed 50 characters");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required")
            .MaximumLength(50).WithMessage("Last name must not exceed 50 characters");

        RuleFor(x => x.DateOfBirth)
            .NotEmpty().WithMessage("Date of birth is required");

        RuleFor(x => x.Gender)
            .IsInEnum().WithMessage("Invalid gender value");

        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("Phone number is required");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email format");

        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("Invalid status value");

        // Essential validation: Ensure GuardLevelId exists
        RuleFor(x => x.GuardLevelId)
            .NotEmpty().WithMessage("Guard level is required")
            .MustAsync(async (id, cancellation) =>
            {
                var guardLevel = await _unitOfWork.GuardLevels.GetByIdAsync(id);
                return guardLevel != null;
            }).WithMessage("Selected guard level does not exist");

        RuleFor(x => x.GuardIdNumber)
            .NotEmpty().WithMessage("Guard ID number is required");

        RuleFor(x => x.MaritalStatus)
            .IsInEnum().WithMessage("Invalid marital status value");

        RuleFor(x => x.Nationality)
            .NotEmpty().WithMessage("Nationality is required")
            .MaximumLength(50).WithMessage("Nationality must not exceed 50 characters");

        RuleFor(x => x.ApplicationDate)
            .NotEmpty().WithMessage("Application date is required");

        RuleFor(x => x.InterviewDate)
            .NotEmpty().WithMessage("Interview date is required");

        // Essential validation: Ensure PostId exists in assignments
        RuleFor(x => x.GuardAssignments)
            .Must(assignments => assignments == null || assignments.All(a => a.PostId > 0))
            .WithMessage("All assignments must have a valid post ID")
            .MustAsync(async (assignments, cancellation) =>
            {
                if (assignments == null || !assignments.Any()) return true;
                
                foreach (var assignment in assignments)
                {
                    var post = await _unitOfWork.Posts.GetByIdAsync(assignment.PostId);
                    if (post == null) return false;
                }
                return true;
            })
            .WithMessage("One or more assigned posts do not exist");

        RuleFor(x => x.PANNumber)
            .MaximumLength(20).WithMessage("PAN number must not exceed 20 characters");

        RuleFor(x => x.SocialSecurityNumber)
            .MaximumLength(20).WithMessage("Social security number must not exceed 20 characters");
    }
}
