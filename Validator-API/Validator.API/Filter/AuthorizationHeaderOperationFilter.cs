using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Validator.API.Filter
{
    public class AuthorizationHeaderOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation == null) return;

            if (operation.Parameters == null)
                operation.Parameters = new List<OpenApiParameter>();

            if (context.ApiDescription.CustomAttributes().Any(attr => attr.GetType() == typeof(AllowAnonymousAttribute)))
            {
                return;
            }

            var parameter = new OpenApiParameter
            {
                In = ParameterLocation.Header,
                Description = "The authorization Token by Valgroup",
                Name = "Token",
                Required = true
            };

            operation.Parameters.Add(parameter);

        }
    }
}
