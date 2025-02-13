using MediatR;
using Microsoft.Extensions.Logging;
using SecurityGuardManagement.Application.Common.Interfaces;
using SecurityGuardManagement.Application.DTOs;
using SecurityGuardManagement.Domain.Entities;
using SecurityGuardManagement.Domain.Enums;
using SecurityGuardManagement.Domain.ValueObjects;
using FluentValidation;
using System;

namespace SecurityGuardManagement.Application.Guards.Commands.CreateGuard;

public class CreateGuardCommandHandler : IRequestHandler<CreateGuardCommand, GuardDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateGuardCommandHandler> _logger;
    private readonly IValidator<CreateGuardCommand> _validator;

    public CreateGuardCommandHandler(
        IUnitOfWork unitOfWork, 
        ILogger<CreateGuardCommandHandler> logger,
        IValidator<CreateGuardCommand> validator)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _validator = validator;
    }

    public async Task<GuardDto> Handle(CreateGuardCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Validating create guard command");
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
            _logger.LogError("Validation failed: {Errors}", errors);
            throw new ValidationException(validationResult.Errors);
        }

        _logger.LogInformation("Creating new guard with ID: {GuardId}", request.GuardIdNumber);
        
        var guard = new Guard
        {
            PersonalInfo = new PersonalInformation
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                DateOfBirth = request.DateOfBirth,
                Gender = request.Gender
            },
            ContactInfo = new ContactInformation
            {
                Phone = request.Phone,
                Email = request.Email
            },
            Status = request.Status,
            GuardLevelId = request.GuardLevelId,
            GuardIdNumber = request.GuardIdNumber,
            MaritalStatus = request.MaritalStatus,
            Nationality = request.Nationality,
            ApplicationDate = request.ApplicationDate,
            InterviewDate = request.InterviewDate,
            DeployedDate = request.DeployedDate,
            DependentContactDetails = request.DependentContactDetails,
            DependentRelations = request.DependentRelations,
            BankAccountDetails = request.BankAccountDetails,
            PANNumber = request.PANNumber,
            SocialSecurityNumber = request.SocialSecurityNumber,
            SkillsCertifications = request.SkillsCertifications,
            PoliceReportStatus = request.PoliceReportStatus,
            DocumentsSubmittedStatus = request.DocumentsSubmittedStatus,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = "system" // TODO: Get from current user
        };

        try
        {
            _logger.LogInformation("Adding guard to repository");
            await _unitOfWork.Guards.AddAsync(guard);
            
            // Save the guard first to get the ID
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            // Create permanent address
            _logger.LogInformation("Creating permanent address for guard");
            var permanentAddress = new GuardAddress
            {
                GuardId = guard.Id,
                Address = request.PermanentAddress,
                IsCurrent = true,
                FromDate = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.GuardAddresses.AddAsync(permanentAddress);

            // Create current address if different from permanent
            if (request.CurrentAddress != null && !request.CurrentAddress.Equals(request.PermanentAddress))
            {
                _logger.LogInformation("Creating current address for guard (different from permanent)");
                var currentAddress = new GuardAddress
                {
                    GuardId = guard.Id,
                    Address = request.CurrentAddress,
                    IsCurrent = true,
                    FromDate = DateTime.UtcNow,
                    CreatedAt = DateTime.UtcNow
                };

                await _unitOfWork.GuardAddresses.AddAsync(currentAddress);
            }

            // Add employment history
            if (request.EmploymentHistory != null && request.EmploymentHistory.Any())
            {
                _logger.LogInformation("Adding employment history records");
                foreach (var history in request.EmploymentHistory)
                {
                    var employmentHistory = new EmploymentHistory
                    {
                        GuardId = guard.Id,
                        CompanyName = history.CompanyName,
                        Position = history.Position,
                        StartDate = history.StartDate,
                        EndDate = history.EndDate,
                        Responsibilities = history.Responsibilities,
                        ReasonForLeaving = history.ReasonForLeaving,
                        CreatedAt = DateTime.UtcNow,
                        CurrentPosition = history.CurrentPosition
                    };
                    await _unitOfWork.EmploymentHistory.AddAsync(employmentHistory);
                }
            }

            // Add guard training
            if (request.GuardTraining != null && request.GuardTraining.Any())
            {
                _logger.LogInformation("Adding guard training records");
                foreach (var training in request.GuardTraining)
                {
                    var guardTraining = new GuardTraining
                    {
                        GuardId = guard.Id,
                        TrainingName = training.TrainingName,
                        Provider = training.Provider,
                        StartDate = training.StartDate,
                        EndDate = training.EndDate ?? DateTime.UtcNow,
                        CertificationNumber = training.CertificationNumber,
                        CreatedAt = DateTime.UtcNow
                    };
                    await _unitOfWork.GuardTraining.AddAsync(guardTraining);
                }
            }

            // Add guard assignments
            if (request.GuardAssignments != null && request.GuardAssignments.Any())
            {
                _logger.LogInformation("Adding guard assignment records");
                foreach (var assignment in request.GuardAssignments)
                {
                    // Double-check post exists (belt and suspenders)
                    var post = await _unitOfWork.Posts.GetByIdAsync(assignment.PostId);
                    if (post == null)
                    {
                        throw new ValidationException($"Post with ID {assignment.PostId} does not exist");
                    }

                    var guardAssignment = new GuardAssignment
                    {
                        GuardId = guard.Id,
                        PostId = assignment.PostId,
                        StartDate = DateTime.UtcNow,
                        Status = AssignmentStatus.Active,
                        Shift = assignment.Shift,
                        CreatedAt = DateTime.UtcNow,
                        CreatedBy = "system" // TODO: Get from current user
                    };
                    await _unitOfWork.GuardAssignments.AddAsync(guardAssignment);
                }
            }

            // Save all related entities
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Successfully saved guard with ID: {GuardId} and all related entities", guard.Id);

            return new GuardDto
            {
                Id = guard.Id,
                PersonalInfo = new DTOs.Common.PersonalInformationDto
                {
                    FirstName = guard.PersonalInfo.FirstName,
                    LastName = guard.PersonalInfo.LastName,
                    DateOfBirth = guard.PersonalInfo.DateOfBirth,
                    Gender = guard.PersonalInfo.Gender
                },
                ContactInfo = new DTOs.Common.ContactInformationDto
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
                EmploymentHistory = request.EmploymentHistory ?? new List<EmploymentHistoryDto>(),
                GuardTraining = request.GuardTraining ?? new List<GuardTrainingDto>(),
                GuardAssignments = request.GuardAssignments ?? new List<GuardAssignmentDto>()
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error saving guard to database");
            throw;
        }
    }
}
