using MediatR;
using SecurityGuardManagement.Application.Common.Interfaces;
using SecurityGuardManagement.Application.DTOs;
using SecurityGuardManagement.Application.DTOs.Common;

namespace SecurityGuardManagement.Application.Guards.Queries.GetGuardById;

public class GetGuardByIdQueryHandler : IRequestHandler<GetGuardByIdQuery, GuardDto?>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetGuardByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GuardDto?> Handle(GetGuardByIdQuery request, CancellationToken cancellationToken)
    {
        var guard = await _unitOfWork.Guards.GetByIdAsync(request.Id);
        
        if (guard == null)
            return null;

        return new GuardDto
        {
            Id = guard.Id,
            PersonalInfo = new PersonalInformationDto
            {
                FirstName = guard.PersonalInfo.FirstName,
                LastName = guard.PersonalInfo.LastName,
                DateOfBirth = guard.PersonalInfo.DateOfBirth,
                Gender = guard.PersonalInfo.Gender
            },
            ContactInfo = new ContactInformationDto
            {
                Phone = guard.ContactInfo.Phone,
                Email = guard.ContactInfo.Email
            },
            Status = guard.Status,
            GuardLevelId = guard.GuardLevelId,
            GuardIdNumber = guard.GuardIdNumber,
            MaritalStatus = guard.MaritalStatus,
            Nationality = guard.Nationality,
            ApplicationDate = guard.ApplicationDate,
            InterviewDate = guard.InterviewDate,
            DeployedDate = guard.DeployedDate,
            DependentContactDetails = guard.DependentContactDetails,
            DependentRelations = guard.DependentRelations,
            BankAccountDetails = guard.BankAccountDetails,
            PANNumber = guard.PANNumber,
            SocialSecurityNumber = guard.SocialSecurityNumber,
            SkillsCertifications = guard.SkillsCertifications,
            PoliceReportStatus = guard.PoliceReportStatus,
            DocumentsSubmittedStatus = guard.DocumentsSubmittedStatus
        };
    }
}
