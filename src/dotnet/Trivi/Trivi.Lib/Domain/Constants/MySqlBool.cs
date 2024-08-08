using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trivi.Lib.Domain.Constants;

public class MySqlBool
{
    public const int True = 1;
    public const int False = 0;
}

public static class MySqlBoolValueExtensions
{
    public static bool ToNativeBool(this int value)
    {
        return value == MySqlBool.True;
    }
}
