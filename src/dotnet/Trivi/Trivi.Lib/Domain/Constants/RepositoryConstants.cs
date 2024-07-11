using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trivi.Lib.Domain.Constants;

public sealed class RepositoryConstants
{
    public const string PAGINATION_CLAUSE = @" LIMIT @pagination_limit OFFSET @pagination_offset ";

    public const int USER_DEFINED_EXCEPTION_NUMBER = 1644;
}
