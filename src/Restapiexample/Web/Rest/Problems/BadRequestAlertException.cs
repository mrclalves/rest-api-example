using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Compuletra.RestApiExample.Web.Rest.Problems {
    public class BadRequestAlertException : ProblemDetailsException {
        public BadRequestAlertException(string detail, string entityName, string errorKey) : this(
            ErrorConstants.DefaultType, detail, entityName, errorKey)
        {
        }

        public BadRequestAlertException(string type, string detail, string entityName, string errorKey) : base(
            new ProblemDetails {
                Type = type,
                Detail = detail,
                Status = StatusCodes.Status400BadRequest,
                Extensions = {["params"] = entityName, ["message"] = $"error.{errorKey}"}
            })
        {
        }
    }
}
