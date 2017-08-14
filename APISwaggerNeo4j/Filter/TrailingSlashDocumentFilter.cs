using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Swashbuckle.Swagger;
using System.Web.Http.Description;

namespace APISwaggerNeo4j.Filter
{ 
    // TrailingSlashDocumentFilter
    /// <summary>
    /// 
    /// </summary>
    public class TrailingSlashDocumentFilter : IDocumentFilter
    {
        public void Apply(SwaggerDocument swaggerDoc, SchemaRegistry schemaRegistry, IApiExplorer apiExplorer)
        {
            swaggerDoc.paths = swaggerDoc.paths.ToDictionary(
                kvp => kvp.Key + "/",
                kvp => kvp.Value
            );
        }
    }
}