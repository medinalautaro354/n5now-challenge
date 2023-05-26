using Application.Common.Services;
using Domain.DomainEvents;
using MediatR;
using N5NowApi.Application.Classes;
using N5NowApi.Domain.Dtos;
using N5NowApi.Domain.Entities;
using N5NowApi.Domain.Repositories;
using N5NowApi.Domain.ValueObjects.Permission;

namespace Application.UseCases.V1.PermissionOperation.Commands.Create
{
    public record struct CreatePermissionCommand(string EmployeeForename, string EmployeeSurname, int PermissionTypeId) : IRequest<Response<CreatePermissionResponse>>
    {
    }

    public class CreatePermissionCommandHandler : IRequestHandler<CreatePermissionCommand, Response<CreatePermissionResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPermissionRepository _repository;
        private readonly IPermissionTypeRepository _permissionTypeRepository;

        public CreatePermissionCommandHandler(IUnitOfWork unitOfWork, IPermissionRepository repository, IPermissionTypeRepository permissionTypeRepository)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _permissionTypeRepository = permissionTypeRepository ?? throw new ArgumentNullException(nameof(permissionTypeRepository));
        }

        public async Task<Response<CreatePermissionResponse>> Handle(CreatePermissionCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<CreatePermissionResponse>();

            var existPermissionType = await _permissionTypeRepository.GetById(request.PermissionTypeId);

            if (existPermissionType is null)
            {
                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                response.AddNotification("Entity Not Found", $"{nameof(request.PermissionTypeId)}", "PermissionType not found, a valid PermissionTypeId is required.");

                return response;
            }

            var addPermissionDto = new AddPermissionDto(EmployeeForename.Create(request.EmployeeForename), EmployeeSurname.Create(request.EmployeeSurname), request.PermissionTypeId);

            var entity = Permission.Create(addPermissionDto);

            entity.RaiseEvent(new PermissionCreatedDomainEvent());

            await _repository.Add(entity);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            response.Content = new CreatePermissionResponse(entity.Id);
            response.StatusCode = System.Net.HttpStatusCode.Created;
            return response;
        }

    }
}
