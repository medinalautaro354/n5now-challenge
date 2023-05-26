using N5NowApi.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Persistance;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private readonly IPermissionRepository _permissionRepository;
    private readonly IPermissionTypeRepository _permissionTypeRepository;
    public UnitOfWork(ApplicationDbContext context, IPermissionRepository permissionRepository, IPermissionTypeRepository permissionTypeRepository)
    {
        _context = context;
        _permissionRepository = permissionRepository;
        _permissionTypeRepository = permissionTypeRepository;
    }

    public IPermissionTypeRepository PermissionTypeRepository { get { return _permissionTypeRepository; } }
    public IPermissionRepository PermissionRepository { get { return _permissionRepository; } }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }
}

