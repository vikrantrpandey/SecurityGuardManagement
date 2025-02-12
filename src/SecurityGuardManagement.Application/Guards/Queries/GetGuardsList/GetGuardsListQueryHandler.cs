using MediatR;
using SecurityGuardManagement.Application.Common.Interfaces;
using SecurityGuardManagement.Application.DTOs;
using SecurityGuardManagement.Application.DTOs.Common;

namespace SecurityGuardManagement.Application.Guards.Queries.GetGuardsList;

public class GetGuardsListQueryHandler : IRequestHandler<GetGuardsListQuery, IEnumerable<GuardDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetGuardsListQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<GuardDto>> Handle(GetGuardsListQuery request, CancellationToken cancellationToken)
    {
        var guards = await _unitOfWork.Guards.GetAllAsync();
        
        return guards.Select(guard => new GuardDto
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
        });
    }
}
