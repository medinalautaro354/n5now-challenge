using N5NowApi.Domain.Entities;
using N5NowApi.Domain.Repositories;

namespace Infrastructure.Persistance.Repositories;

public class PermissionTypeRepository : Repository<PermissionType>, IPermissionTypeRepository
{
    private readonly ApplicationDbContext _context;
    public PermissionTypeRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
}
