using System;
using System.Threading.Tasks;
using GraphQL;
using GraphApi.Client.Models.Schemas;
using GraphQL.Execution;
using GraphQL.SchemaGenerator;
using GraphQL.Validation;
using GraphQL.Validation.Complexity;
using Microsoft.AspNetCore.Mvc;

namespace GraphQLApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GraphQLController : ControllerBase
    {
        private readonly IServiceProvider serviceProvider;

        public GraphQLController(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Return data filtered by <paramref name="query"/>.
        /// </summary>
        /// <example>
        /// query = @"{
        /// projects {
        ///           projectName,
        ///           users{
        ///                 firstName
        ///           }
        ///         }
        ///     }
        ///     OR
        ///  project (id: {value}) {
        ///           projectName,
        ///           users{
        ///                 firstName
        ///           }
        ///         }
        ///     }"
        /// </example>
        [HttpGet]
        public async Task<IActionResult> GetProjects([FromQuery] string query)
        {
            var schemaGenerator = new SchemaGenerator(this.serviceProvider);
            var schema = schemaGenerator.CreateSchema(typeof(UserSchema));
            var exec = new DocumentExecuter(new GraphQLDocumentBuilder(), new DocumentValidator(), new ComplexityAnalyzer());
            var result = await exec.ExecuteAsync(schema, null, query, null);

            return Ok(result);
        }
    }
}