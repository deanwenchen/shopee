using System.ComponentModel.DataAnnotations;

namespace ShoppeAPI.Features.Auth.DTOs;

public class RegisterDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [MinLength(8)]
    public string Password { get; set; }

    public string? PhoneNumber { get; set; }
}

public class LoginStep1Dto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}

public class LoginDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}

public class PasswordRecoveryDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [RegularExpression("sms|email")]
    public string Method { get; set; }
}

public class VerifyCodeDto
{
    [Required]
    public string CodeId { get; set; }

    [Required]
    [Length(4, 4)]
    public string Code { get; set; }
}

public class ResetPasswordDto
{
    [Required]
    public string ResetToken { get; set; }

    [Required]
    [MinLength(8)]
    public string NewPassword { get; set; }
}
