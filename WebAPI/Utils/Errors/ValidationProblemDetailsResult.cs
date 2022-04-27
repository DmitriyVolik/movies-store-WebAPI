using Microsoft.AspNetCore.Mvc;
using WebAPI.Utils.Json;

namespace WebAPI.Utils.Errors
{
    public class ValidationProblemDetailsResult : IActionResult
    {
        public async Task ExecuteResultAsync(ActionContext context)
        {
            var modelStateEntries = context.ModelState
                .Where(e => e.Value.Errors.Count > 0)
                .ToArray();

            var errors = new List<ValidationError>();

            if (modelStateEntries.Any())
            {
                foreach (var (key, value) in modelStateEntries)
                {
                    errors.AddRange(value.Errors
                        .Select(modelStateError => new ValidationError(
                            name: key.ToSnakeCase(),
                            description: modelStateError.ErrorMessage)));
                }
            }

            await new JsonErrorResponse<ValidationProblemDetails>(
                context: context.HttpContext,
                error: new ValidationProblemDetails(errors),
                statusCode: ValidationProblemDetails.ValidationStatusCode).WriteAsync();
        }
    }
}