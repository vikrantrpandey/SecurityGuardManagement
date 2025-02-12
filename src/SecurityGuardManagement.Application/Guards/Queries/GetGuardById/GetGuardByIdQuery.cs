using MediatR;
using SecurityGuardManagement.Application.DTOs;

namespace SecurityGuardManagement.Application.Guards.Queries.GetGuardById;

public record GetGuardByIdQuery(int Id) : IRequest<GuardDto?>;
