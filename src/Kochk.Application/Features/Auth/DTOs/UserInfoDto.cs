using System.ComponentModel.DataAnnotations;

namespace Kochk.Application.Features.Auth.Models;

public class UserInfoDto
{
    [Required]
    public string Email { get; set; } = default!;

    [Required]
    public string FirstName { get; set; } = default!;

    [Required]
    public string LastName { get; set; } = default!;

    [Required]
    public string Password { get; set; } = default!;

    [Required]
    public string PhoneNumber { get; set; } = default!;
}
