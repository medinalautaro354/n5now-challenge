using System;
using System.Threading;
using System.Threading.Tasks;

namespace N5NowApi.Domain.Repositories;
public interface IUnitOfWork 
{
    Task SaveChangesAsync(CancellationToken cancellationToken= default);
}
