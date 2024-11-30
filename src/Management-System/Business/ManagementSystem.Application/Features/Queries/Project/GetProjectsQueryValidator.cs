using FluentValidation;
using Packages.Pipelines.Validation;

namespace ManagementSystem.Application.Features.Queries.Project;
public class GetProjectsQueryValidator : AbstractValidator<GetProjectsQuery>, IRequestValidator{
    public GetProjectsQueryValidator()
    {
        RuleFor(p => p.Name).MaximumLength(100);
    }
}