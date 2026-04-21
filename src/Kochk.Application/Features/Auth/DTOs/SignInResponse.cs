using System.ComponentModel.DataAnnotations;

namespace Kochk.Application.Features.Auth.Models;

public class SignInResponse
{
    public string Email { get; set; } = default!;
    public string Token { set; get; } = default!;
    public string DisplayName { set; get; } = default!;
    public string Role { get; set; } = default!;
}
