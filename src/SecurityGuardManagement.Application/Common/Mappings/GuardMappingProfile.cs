using AutoMapper;
using SecurityGuardManagement.Application.DTOs;
using SecurityGuardManagement.Application.Guards.Commands.CreateGuard;
using SecurityGuardManagement.Domain.Entities;

namespace SecurityGuardManagement.Application.Common.Mappings;

public class GuardMappingProfile : Profile
{
    public GuardMappingProfile()
    {
        CreateMap<CreateGuardCommand, Guard>()
            .ForMember(dest => dest.PersonalInfo, opt => opt.MapFrom(src => new Domain.ValueObjects.PersonalInformation
            {
                FirstName = src.FirstName,
                LastName = src.LastName,
                DateOfBirth = src.DateOfBirth,
                Gender = src.Gender
            }))
            .ForMember(dest => dest.ContactInfo, opt => opt.MapFrom(src => new Domain.ValueObjects.ContactInformation
            {
                Phone = src.Phone,
                Email = src.Email
            }));

        CreateMap<Guard, GuardDto>()
            .ForMember(dest => dest.PersonalInfo, opt => opt.MapFrom(src => new DTOs.Common.PersonalInformationDto
            {
                FirstName = src.PersonalInfo.FirstName,
                LastName = src.PersonalInfo.LastName,
                DateOfBirth = src.PersonalInfo.DateOfBirth,
                Gender = src.PersonalInfo.Gender
            }))
            .ForMember(dest => dest.ContactInfo, opt => opt.MapFrom(src => new DTOs.Common.ContactInformationDto
            {
                Phone = src.ContactInfo.Phone,
                Email = src.ContactInfo.Email
            }));

        CreateMap<EmploymentHistory, EmploymentHistoryDto>().ReverseMap();
        CreateMap<GuardTraining, GuardTrainingDto>().ReverseMap();
        CreateMap<GuardAssignment, GuardAssignmentDto>().ReverseMap();
        CreateMap<GuardAddress, GuardAddressDto>().ReverseMap();
    }
}
