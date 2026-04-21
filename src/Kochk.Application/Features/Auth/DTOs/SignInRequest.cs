using System.ComponentModel.DataAnnotations;

namespace Kochk.Application.Features.Auth.Models;

public class SignInRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = default!;

    [Required]
    public string Password { get; set; } = default!;
}
