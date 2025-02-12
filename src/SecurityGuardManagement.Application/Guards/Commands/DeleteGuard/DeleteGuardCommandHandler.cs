using MediatR;
using SecurityGuardManagement.Application.Common.Interfaces;

namespace SecurityGuardManagement.Application.Guards.Commands.DeleteGuard;

public class DeleteGuardCommandHandler : IRequestHandler<DeleteGuardCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteGuardCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteGuardCommand request, CancellationToken cancellationToken)
    {
        var guard = await _unitOfWork.Guards.GetByIdAsync(request.Id);
        
        if (guard == null)
            throw new KeyNotFoundException($"Guard with ID {request.Id} not found.");

        await _unitOfWork.Guards.DeleteAsync(guard);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
