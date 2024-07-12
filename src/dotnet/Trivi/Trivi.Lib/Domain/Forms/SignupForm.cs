using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Trivi.Lib.Domain.Forms;

public class SignupForm : LoginForm
{
    [BindRequired]
    public required string PasswordConfirm { get; set; }
}


