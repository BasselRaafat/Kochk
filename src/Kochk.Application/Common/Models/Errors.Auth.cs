namespace Kochk.Application.Common.Models;

public static partial class Errors
{
    public static class Auth
    {
        public static readonly Error DuplicateEmail = new(
            "AUTH.DUPLICATE_EMAIL",
            ErrorType.Validation,
            "Email is already registered"
        );

        public static readonly Error DuplicateUsername = new(
            "AUTH.DUPLICATE_USERNAME",
            ErrorType.Validation,
            "Username is already taken"
        );

        public static readonly Error InvalidCredentials = new(
            "AUTH.INVALID_CREDENTIALS",
            ErrorType.Unauthorized,
            "Invalid email or password"
        );

        public static readonly Error UserWithoutRole = new(
            "AUTH.USER.WITHOUT.ROLE",
            ErrorType.Validation,
            "User without Role"
        );
        public static readonly Error AddingUserToCustomerRole = new(
            "AUTH.CUSOMER.ROLE.FAIlURE",
            ErrorType.Validation,
            "Somthing went wrong pleas try again"
        );
        public static readonly Error BusinessUserCreationFailed = new(
            "AUTH.BUSINESSUSER.CREATION.FAILED",
            ErrorType.Validation,
            "Something went wrong while creating your account. Please try again."
        );

        public static Error Validation(Dictionary<string, List<string>> errors) =>
            new(
                "USER_INPUT_INVALID",
                ErrorType.Validation,
                "There is something wrong in you input",
                errors
            );
    }
}
