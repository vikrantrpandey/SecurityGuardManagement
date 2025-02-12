using MediatR;

namespace SecurityGuardManagement.Application.Guards.Commands.DeleteGuard;

public record DeleteGuardCommand(int Id) : IRequest<Unit>;
