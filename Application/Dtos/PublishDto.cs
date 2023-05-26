using Domain.Enums;

namespace Application.Dtos
{
    public record struct PublishDto(string Id, OperationName OperationName)
    {
    }
}
