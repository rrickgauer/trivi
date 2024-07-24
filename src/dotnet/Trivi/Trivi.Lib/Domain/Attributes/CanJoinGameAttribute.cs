using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trivi.Lib.Domain.Attributes;

[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
public class CanJoinGameAttribute : Attribute
{

}
