using System.Data;
using Trivi.Lib.Mapping.Tables;

namespace Trivi.Lib.Services.Contracts;

public interface ITableMapperService
{
    public T ToModel<T>(DataRow dataRow);
    public List<T> ToModels<T>(DataTable dataTable);
    public TableMapper<T> GetMapper<T>();
}
