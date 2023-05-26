using Application.Common.Services;
using Application.Dtos;
using Domain.Enums;
using MediatR;
using N5NowApi.Application.Classes;
using N5NowApi.Domain.Repositories;

namespace Application.UseCases.V1.PermissionOperation.Queries.GetAll
{
    public record struct GetAllPermissions : IRequest<Response<List<PermissionDto>>>
    {
    }

    public class GetAllPermissionsHandler : IRequestHandler<GetAllPermissions, Response<List<PermissionDto>>>
    {
        private readonly IPermissionRepository _repository;
        private readonly IPublisherService _publisherService;
        public GetAllPermissionsHandler(IPermissionRepository repository, IPublisherService publisherService)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _publisherService = publisherService ?? throw new ArgumentNullException(nameof(publisherService));
        }

        public async Task<Response<List<PermissionDto>>> Handle(GetAllPermissions request, CancellationToken cancellationToken)
        {
            var results = await _repository.GetAllAsync();

            var guid = Guid.NewGuid().ToString();

            await _publisherService.Publish(new PublishDto(guid, OperationName.Get), cancellationToken);

            return new Response<List<PermissionDto>>
            {
                Content = results.Select(x =>
                    new PermissionDto(
                        x.Id,
                        x.EmployeeForename.Value,
                        x.EmployeeSurname.Value,
                        new PermissionTypeDto(
                            x.PermissionType.Id,
                            x.PermissionType.Description.Value)
                        )).ToList(),
                StatusCode = System.Net.HttpStatusCode.OK,
            };
        }
    }
}
