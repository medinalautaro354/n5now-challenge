using Application.Dtos;
using Application.UseCases.V1.PermissionOperation.Commands.Create;
using Application.UseCases.V1.PermissionOperation.Commands.Update;
using Application.UseCases.V1.PermissionOperation.Queries.GetAll;
using Microsoft.AspNetCore.Mvc;
using N5NowApi.Application.Classes;

namespace N5NowApi.Controllers.V1
{
    public class PermissionsController : ApiControllerBase
    {
        /// <summary>
        /// Get all permissions.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<PermissionDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll() => Result(await Mediator.Send(new GetAllPermissions()));

        /// <summary>
        /// Add a new permission
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(CreatePermissionResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(List<Notify>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] AddPermissionDto dto)
        {
            var request = new CreatePermissionCommand(dto.EmployeeForename, dto.EmployeeSurname, dto.PermissionTypeId);
            return Result(await Mediator.Send(request));
        }

        /// <summary>
        /// Update a permission.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(UpdatePermissionResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(List<Notify>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put([FromRoute(Name = "id")] int id, [FromBody] UpdatePermissionDto dto)
        {
            var request = new UpdatePermissionCommand()
            {
                Id = id,
                EmployeeForename = dto.EmployeeForename,
                EmployeeSurname = dto.EmployeeSurname,
                PermissionTypeId = dto.PermissionTypeId
            };

            return Result(await Mediator.Send(request));
        }
    }
}
