using Microsoft.AspNetCore.Routing.Constraints;
using Trivi.Lib.Domain.Constants;

namespace Trivi.Lib.RouteConstraints;

public class QuestionIdRouteConstraint() : RegexRouteConstraint(NanoIdConstants.QuestionIdRegex)
{

}

public class TrueFalseRouteConstraint() : RegexInlineRouteConstraint(NanoIdConstants.TrueFalseRegex)
{

}


public class MultipleChoiceConstraint() : RegexInlineRouteConstraint(NanoIdConstants.MultipleChoiceRegex)
{

}

public class ShortAnswerConstraint() : RegexInlineRouteConstraint(NanoIdConstants.ShortAnswerRegex)
{

}