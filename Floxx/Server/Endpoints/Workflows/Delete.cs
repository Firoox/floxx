using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Floxx.Core.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Floxx.Server.Endpoints.Workflows
{
    public class Delete : BaseAsyncEndpoint
        .WithRequest<int>
        .WithoutResponse
    {
        private readonly IWorkflowRepository repository;

        public Delete(IWorkflowRepository repository)
        {
            this.repository = repository;
        }

        [HttpDelete("/api/workflows/{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(
            Summary = "Deletes a Workflow",
            OperationId = "Workflows.Delete",
            Tags = new[] { "Workflows" })
        ]
        public override async Task<ActionResult> HandleAsync(int id, CancellationToken cancellationToken)
        {
            var itemToDelete = await repository.GetByIdAsync(id);

            if (itemToDelete == null)
            {
                return NotFound();
            }

            await repository.DeleteAsync(itemToDelete);

            return NoContent();
        }
    }
}
