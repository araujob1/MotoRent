using FluentValidation;
using MotoRent.Communication.Requests;
using MotoRent.Exceptions;

namespace MotoRent.Application.Validators;

public class PagedRequestValidator<TRequest> : AbstractValidator<TRequest>
    where TRequest : PagedRequest
{
    protected const int MaxPageSize = 100;

    public PagedRequestValidator()
    {
        RuleFor(x => x.PageSize)
            .LessThanOrEqualTo(MaxPageSize)
            .WithMessage(ErrorMessages.INVALID_PAGE_SIZE);
    }
}
