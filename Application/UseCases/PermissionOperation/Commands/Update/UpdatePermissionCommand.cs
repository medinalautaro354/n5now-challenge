using Domain.DomainEvents;
using MediatR;
using N5NowApi.Application.Classes;
using N5NowApi.Domain.Dtos;
using N5NowApi.Domain.Entities;
using N5NowApi.Domain.Repositories;
using N5NowApi.Domain.ValueObjects.Permission;

namespace Application.UseCases.V1.PermissionOperation.Commands.Update
{
    public record struct UpdatePermissionCommand(
        int Id, 
        string EmployeeForename, 
        string EmployeeSurname,
        int PermissionTypeId) : IRequest<Response<UpdatePermissionResponse>>
    {
    }

    public class UpdatePermissionCommandHandler : IRequestHandler<UpdatePermissionCommand, Response<UpdatePermissionResponse>>
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly IPermissionTypeRepository _permissionTypeRepository;
        private readonly IUnitOfWork _unitOfWork;
        public UpdatePermissionCommandHandler(IPermissionRepository permissionRepository, IUnitOfWork unitOfWork, IPermissionTypeRepository permissionTypeRepository)
        {
            _permissionRepository = permissionRepository ?? throw new ArgumentNullException(nameof(permissionRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _permissionTypeRepository = permissionTypeRepository ?? throw new ArgumentNullException(nameof(permissionTypeRepository));
        }

        public async Task<Response<UpdatePermissionResponse>> Handle(UpdatePermissionCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<UpdatePermissionResponse>();

            var existsPermission = await _permissionRepository.GetById(request.Id);
            var existsPermissionType = await _permissionTypeRepository.GetById(request.PermissionTypeId);

            if (existsPermission is null)
            {
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                response.AddNotification("Entity not Found", $"{nameof(request.Id)}", "Permission not found, a valid Id is required.");

                return response;
            }

            if (existsPermissionType is null)
            {
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                response.AddNotification("Entity not Found", $"{nameof(request.PermissionTypeId)}", "PermissionType not found, a valid PermissionTypeId is required.");

                return response;
            }

            var permissionUpdatedProperties = Permission.UpdateProperties(existsPermission, new UpdatePermissionDto(request.Id, EmployeeForename.Create(request.EmployeeForename), EmployeeSurname.Create(request.EmployeeSurname), request.PermissionTypeId));

            permissionUpdatedProperties.RaiseEvent(new PermissionUpdatedDomainEvent());

            await _permissionRepository.Update(permissionUpdatedProperties);

            await _unitOfWork.SaveChangesAsync(cancellationToken);


            response.Content = new UpdatePermissionResponse(request.Id);
            return response;
        }
    }
}
