using MediatR;
using SecurityGuardManagement.Application.DTOs;

namespace SecurityGuardManagement.Application.Guards.Queries.GetGuardsList;

public record GetGuardsListQuery : IRequest<IEnumerable<GuardDto>>;
