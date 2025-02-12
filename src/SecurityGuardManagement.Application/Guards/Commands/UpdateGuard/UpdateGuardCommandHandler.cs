using MediatR;
using SecurityGuardManagement.Application.Common.Interfaces;
using SecurityGuardManagement.Domain.ValueObjects;

namespace SecurityGuardManagement.Application.Guards.Commands.UpdateGuard;

public class UpdateGuardCommandHandler : IRequestHandler<UpdateGuardCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateGuardCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(UpdateGuardCommand request, CancellationToken cancellationToken)
    {
        var guard = await _unitOfWork.Guards.GetByIdAsync(request.Id);
        
        if (guard == null)
            throw new KeyNotFoundException($"Guard with ID {request.Id} not found.");

        guard.PersonalInfo = new PersonalInformation
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            DateOfBirth = request.DateOfBirth,
            Gender = request.Gender
        };

        guard.ContactInfo = new ContactInformation
        {
            Phone = request.Phone,
            Email = request.Email
        };

        guard.Status = request.Status;
        guard.GuardLevelId = request.GuardLevelId;
        guard.GuardIdNumber = request.GuardIdNumber;
        guard.MaritalStatus = request.MaritalStatus;
        guard.Nationality = request.Nationality;
        guard.ApplicationDate = request.ApplicationDate;
        guard.InterviewDate = request.InterviewDate;
        guard.DeployedDate = request.DeployedDate;
        guard.DependentContactDetails = request.DependentContactDetails;
        guard.DependentRelations = request.DependentRelations;
        guard.BankAccountDetails = request.BankAccountDetails;
        guard.PANNumber = request.PANNumber;
        guard.SocialSecurityNumber = request.SocialSecurityNumber;
        guard.SkillsCertifications = request.SkillsCertifications;
        guard.PoliceReportStatus = request.PoliceReportStatus;
        guard.DocumentsSubmittedStatus = request.DocumentsSubmittedStatus;

        await _unitOfWork.Guards.UpdateAsync(guard);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
