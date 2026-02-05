using System.Diagnostics.CodeAnalysis;
using Core.Domain.Enums;

namespace Core.Application.DTOs;

public record GenericCategoryDto
{
    public int Id { init; get; }
    public required string Name { init; get; }

    [SetsRequiredMembers]
    public GenericCategoryDto(RequestStatus status)
    {
        Id = (int)status;
        Name = status.ToString();
    }

    [SetsRequiredMembers]
    public GenericCategoryDto(Article article)
    {
        Id = (int)article;
        Name = article.GetName();
    }
}
