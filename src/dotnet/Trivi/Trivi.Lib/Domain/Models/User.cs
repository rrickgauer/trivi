using Trivi.Lib.Domain.Forms;
using Trivi.Lib.Utility;

namespace Trivi.Lib.Domain.Models;

public class User
{
    public Guid? Id { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;


    public static User FromSignup(LoginForm credentials)
    {
        User result = new()
        {
            Id = GuidUtility.New(),
            Email = credentials.Email,
            Password = credentials.Password,
            CreatedOn = DateTime.UtcNow,
        };

        return result;
    }
}
