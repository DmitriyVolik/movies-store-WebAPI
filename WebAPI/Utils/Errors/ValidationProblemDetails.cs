using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace WebApiTasks.Utils.Errors
{
    public class ValidationProblemDetails : ProblemDetails
    {
        public const int ValidationStatusCode = (int)HttpStatusCode.BadRequest;

        public ValidationProblemDetails(ICollection<ValidationError> validationErrors)
        {
            ValidationErrors = validationErrors;

            Status = ValidationStatusCode;
            Title = "Request Validation Error";
            Instance = "CT Portal";
        }

        public ICollection<ValidationError> ValidationErrors { get; }

        public string RequestId => Guid.NewGuid().ToString();
    }
}