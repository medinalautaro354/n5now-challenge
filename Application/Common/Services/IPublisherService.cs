using Application.Dtos;
using System.Threading;

namespace Application.Common.Services
{
    public interface IPublisherService
    {
        Task Publish(PublishDto publishDto, CancellationToken cancellationToken);
    }
}
