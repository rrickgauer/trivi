using Microsoft.AspNetCore.Routing.Constraints;
using Trivi.Lib.Domain.Attributes;
using Trivi.Lib.Domain.Constants;

namespace Trivi.Lib.RouteConstraints;


[ConstraintKey("questionId")]
public class QuestionIdRouteConstraint() : RegexRouteConstraint(NanoIdConstants.QuestionIdRegex)
{
    
}
