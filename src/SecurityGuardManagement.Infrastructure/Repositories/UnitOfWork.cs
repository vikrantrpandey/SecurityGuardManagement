using SecurityGuardManagement.Application.Common.Interfaces;
using SecurityGuardManagement.Domain.Entities;
using SecurityGuardManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace SecurityGuardManagement.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly SecurityGuardDbContext _context;
    private bool _disposed;

    private IRepository<Guard>? _guards;
    private IRepository<GuardLevel>? _guardLevels;
    private IRepository<GuardAddress>? _guardAddresses;
    private IRepository<GuardEducation>? _guardEducation;
    private IRepository<GuardTraining>? _guardTraining;
    private IRepository<EmploymentHistory>? _employmentHistory;
    private IRepository<Client>? _clients;
    private IRepository<Post>? _posts;
    private IRepository<GuardAssignment>? _guardAssignments;
    private IRepository<AssignmentHistory>? _assignmentHistory;

    public UnitOfWork(SecurityGuardDbContext context)
    {
        _context = context;
    }

    public IRepository<Guard> Guards => _guards ??= new GenericRepository<Guard>(_context);
    public IRepository<GuardLevel> GuardLevels => _guardLevels ??= new GenericRepository<GuardLevel>(_context);
    public IRepository<GuardAddress> GuardAddresses => _guardAddresses ??= new GenericRepository<GuardAddress>(_context);
    public IRepository<GuardEducation> GuardEducation => _guardEducation ??= new GenericRepository<GuardEducation>(_context);
    public IRepository<GuardTraining> GuardTraining => _guardTraining ??= new GenericRepository<GuardTraining>(_context);
    public IRepository<EmploymentHistory> EmploymentHistory => _employmentHistory ??= new GenericRepository<EmploymentHistory>(_context);
    public IRepository<Client> Clients => _clients ??= new GenericRepository<Client>(_context);
    public IRepository<Post> Posts => _posts ??= new GenericRepository<Post>(_context);
    public IRepository<GuardAssignment> GuardAssignments => _guardAssignments ??= new GenericRepository<GuardAssignment>(_context);
    public IRepository<AssignmentHistory> AssignmentHistory => _assignmentHistory ??= new GenericRepository<AssignmentHistory>(_context);

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task BeginTransactionAsync()
    {
        await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        await _context.Database.CommitTransactionAsync();
    }

    public async Task RollbackTransactionAsync()
    {
        await _context.Database.RollbackTransactionAsync();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            _context.Dispose();
        }
        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
