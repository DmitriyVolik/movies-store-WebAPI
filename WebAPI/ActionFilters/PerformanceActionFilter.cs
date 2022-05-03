using System.Diagnostics;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebAPI.ActionFilters;

public class PerformanceActionFilter : ActionFilterAttribute
{
    private readonly Stopwatch _stopwatch;

    public PerformanceActionFilter()
    {
        _stopwatch = new Stopwatch();
    }

    // Called before Action method
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        _stopwatch.Start();
        base.OnActionExecuting(context);
    }

    // Called after Action method
    public override void OnActionExecuted(ActionExecutedContext context)
    {
        _stopwatch.Stop();
        var time = _stopwatch.Elapsed.TotalMilliseconds.ToString(CultureInfo.CurrentCulture);
        context.HttpContext.Response.Headers.Add("processing_time", time);
        base.OnActionExecuted(context);
    }
}