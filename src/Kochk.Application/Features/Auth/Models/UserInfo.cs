namespace Kochk.Application.Features.Auth.Models;

public record UserInfo(
    string Email,
    string FirstName,
    string LastName,
    string Password,
    string PhoneNumber
);
