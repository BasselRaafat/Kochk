using FluentValidation;

namespace Kochk.Application.Features.Auth.Commands.CustomerSignUp;

public class CustomerSignUpCommandValidator : AbstractValidator<CustomerSignUpCommand>
{
    public CustomerSignUpCommandValidator() { }
}
