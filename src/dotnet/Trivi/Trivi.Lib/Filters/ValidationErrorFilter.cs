using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Trivi.Lib.Domain.Attributes;
using Trivi.Lib.Domain.Configurations;

namespace Trivi.Lib.Filters;


[AutoInject(AutoInjectionType.Scoped, InjectionProject.Always)]
public class ValidationErrorFilter(IConfigs configs) : IAsyncActionFilter
{
    private readonly IConfigs _configs = configs;

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.ModelState.IsValid)
        {
            

            if (_configs.IsProduction)
            {
                context.Result = new UnprocessableEntityResult();
            }
            else
            {
                //context.Result = new UnprocessableEntityResult();
                context.Result = new UnprocessableEntityObjectResult(context.ModelState);
            }


            return;
        }

        await next();
    }
}
