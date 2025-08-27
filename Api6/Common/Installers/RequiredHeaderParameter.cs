namespace Api.Installers
{
    using Microsoft.OpenApi.Any;
    using Microsoft.OpenApi.Models;
    using Swashbuckle.AspNetCore.SwaggerGen;
    using System.Collections.Generic;
    using Util.Common;

    public class RequiredHeaderParameter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Parameters = operation.Parameters ?? new List<OpenApiParameter>();

            operation.Parameters.Add(new OpenApiParameter()
            {
                Name = "CodeClient",
                In = ParameterLocation.Header,
                Required = false,
                Example = new OpenApiString("6ff993d5-08e8-498b-88bf-a036bdd1288d")
            });
        }
    }
}


