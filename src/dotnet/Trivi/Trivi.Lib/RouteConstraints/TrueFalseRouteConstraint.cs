using Microsoft.AspNetCore.Routing.Constraints;
using Trivi.Lib.Domain.Attributes;
using Trivi.Lib.Domain.Constants;

namespace Trivi.Lib.RouteConstraints;

[ConstraintKey("trueFalseQuestion")]
public class TrueFalseRouteConstraint() : RegexInlineRouteConstraint(NanoIdConstants.TrueFalseRegex)
{
    
}
