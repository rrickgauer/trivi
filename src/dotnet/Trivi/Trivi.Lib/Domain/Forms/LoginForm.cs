using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Trivi.Lib.Domain.Forms;

public class LoginForm
{
    [BindRequired]
    public required string Email { get; set; }

    [BindRequired]
    public required string Password { get; set; }
}


