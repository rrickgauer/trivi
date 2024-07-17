using Trivi.Lib.Domain.Attributes;
using Trivi.Lib.Domain.Contracts;
using Trivi.Lib.Domain.Models;

namespace Trivi.Lib.Domain.TableViews;

public class ViewCollection : ITableView<ViewCollection, Collection>, IUriGui
{
    [CopyToProperty<Collection>(nameof(Collection.Id))]
    [SqlColumn("collection_id")]
    public Guid? Id { get; set; }

    [CopyToProperty<Collection>(nameof(Collection.Name))]
    [SqlColumn("collection_name")]
    public string? Name { get; set; }

    [CopyToProperty<Collection>(nameof(Collection.UserId))]
    [SqlColumn("collection_user_id")]
    public Guid? UserId { get; set; }

    [CopyToProperty<Collection>(nameof(Collection.CreatedOn))]
    [SqlColumn("collection_created_on")]
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

    public string UriGui => $"/app/collections/{Id}";

    public static explicit operator Collection(ViewCollection other) => other.CastToModel();
}
