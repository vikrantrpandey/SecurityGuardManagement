using AutoMapper;
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
    private readonly IMapper _mapper;

    public CreateGuardCommandHandler(
        IUnitOfWork unitOfWork, 
        ILogger<CreateGuardCommandHandler> logger,
        IValidator<CreateGuardCommand> validator,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<GuardDto> Handle(CreateGuardCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Starting CreateGuardCommand handler for GuardIdNumber: {GuardIdNumber}", request.GuardIdNumber);
            
            // Validate command
            _logger.LogDebug("Validating create guard command");
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                _logger.LogError("Validation failed: {Errors}", errors);
                throw new ValidationException(validationResult.Errors);
            }

            // Validate GuardLevel exists
            _logger.LogDebug("Validating GuardLevel exists with ID: {GuardLevelId}", request.GuardLevelId);
            var guardLevel = await _unitOfWork.GuardLevels.GetByIdAsync(request.GuardLevelId);
            if (guardLevel == null)
            {
                _logger.LogError("GuardLevel with ID {GuardLevelId} not found", request.GuardLevelId);
                throw new ValidationException($"GuardLevel with ID {request.GuardLevelId} does not exist");
            }

            // Start transaction
            _logger.LogDebug("Beginning database transaction");
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                _logger.LogInformation("Creating new guard entity");
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

                _logger.LogDebug("Adding guard to repository");
                await _unitOfWork.Guards.AddAsync(guard);
                
                _logger.LogDebug("Saving guard to get ID");
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                _logger.LogInformation("Guard saved with ID: {GuardId}", guard.Id);

                // Create permanent address
                _logger.LogDebug("Creating permanent address for guard");
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
                    _logger.LogDebug("Creating current address for guard (different from permanent)");
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
                    _logger.LogDebug("Adding {Count} employment history records", request.EmploymentHistory.Count());
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
                    _logger.LogDebug("Adding {Count} guard training records", request.GuardTraining.Count());
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
                    _logger.LogDebug("Adding {Count} guard assignment records", request.GuardAssignments.Count());
                    foreach (var assignment in request.GuardAssignments)
                    {
                        // Double-check post exists
                        _logger.LogDebug("Validating Post exists with ID: {PostId}", assignment.PostId);
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
                _logger.LogDebug("Saving all related entities");
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                // Commit transaction
                _logger.LogDebug("Committing transaction");
                await _unitOfWork.CommitTransactionAsync();

                _logger.LogInformation("Successfully saved guard with ID: {GuardId} and all related entities", guard.Id);

                return _mapper.Map<GuardDto>(guard);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating guard and related entities. Rolling back transaction.");
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while creating guard: {ErrorMessage}", ex.Message);
            throw;
        }
    }
}
