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

        RuleFor(x => x.GuardIdNumber)
            .NotEmpty().WithMessage("Guard ID number is required")
            .MaximumLength(20).WithMessage("Guard ID number must not exceed 20 characters");

        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("Phone number is required")
            .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Phone number must be in a valid format");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Email must be in a valid format");

        RuleFor(x => x.GuardAssignments)
            .MustAsync(async (assignments, cancellation) =>
            {
                if (assignments == null || !assignments.Any())
                    return true;

                foreach (var assignment in assignments)
                {
                    var post = await _unitOfWork.Posts.GetByIdAsync(assignment.PostId);
                    if (post == null)
                        return false;
                }
                return true;
            })
            .WithMessage("One or more Posts do not exist in the system");

        RuleFor(x => x.GuardLevelId)
            .MustAsync(async (guardLevelId, cancellation) =>
            {
                var guardLevel = await _unitOfWork.GuardLevels.GetByIdAsync(guardLevelId);
                return guardLevel != null;
            })
            .WithMessage("The specified Guard Level does not exist");
    }
}
