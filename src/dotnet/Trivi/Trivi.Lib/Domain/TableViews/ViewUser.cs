using System.Text.Json.Serialization;
using Trivi.Lib.Domain.Attributes;
using Trivi.Lib.Domain.Contracts;
using Trivi.Lib.Domain.Models;

namespace Trivi.Lib.Domain.TableViews;

public class ViewUser : ITableView<ViewUser, User>
{

    [SqlColumn("user_id")]
    [CopyToProperty<User>(nameof(User.Id))]
    public Guid? UserId { get; set; }

    [SqlColumn("user_email")]
    [CopyToProperty<User>(nameof(User.Email))]
    public string? UserEmail { get; set; }

    [SqlColumn("user_password")]
    [CopyToProperty<User>(nameof(User.Password))]
    [JsonIgnore]
    public string? UserPassword { get; set; }

    [SqlColumn("user_created_on")]
    [CopyToProperty<User>(nameof(User.CreatedOn))]
    public DateTime UserCreatedOn { get; set; } = DateTime.UtcNow;


    // ITableView
    public static explicit operator User(ViewUser other) => other.CastToModel<ViewUser, User>();
}
