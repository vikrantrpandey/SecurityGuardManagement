using SecurityGuardManagement.Domain.Entities;

namespace SecurityGuardManagement.Application.Common.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IRepository<Guard> Guards { get; }
    IRepository<GuardLevel> GuardLevels { get; }
    IRepository<GuardAddress> GuardAddresses { get; }
    IRepository<GuardEducation> GuardEducation { get; }
    IRepository<GuardTraining> GuardTraining { get; }
    IRepository<EmploymentHistory> EmploymentHistory { get; }
    IRepository<Client> Clients { get; }
    IRepository<Post> Posts { get; }
    IRepository<GuardAssignment> GuardAssignments { get; }
    IRepository<AssignmentHistory> AssignmentHistory { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}
