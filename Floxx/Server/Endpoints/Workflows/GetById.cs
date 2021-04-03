using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using AutoMapper;
using Floxx.Core.Entities;
using Floxx.Core.Interfaces;
using Floxx.Shared.Wrapper;
using Floxx.Web.Shared.DTO.Workflows;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Floxx.Server.Endpoints.Workflows
{
    public class GetById : BaseAsyncEndpoint
        .WithRequest<int>
        .WithResponse<WorkflowResponse>
    {
        private readonly IWorkflowRepository repository;
        private readonly IMapper mapper;

        public GetById(IWorkflowRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet("/api/workflows/{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(
            Summary = "Gets a single Workflow by its Id",
            OperationId = "Workflows.GetById",
            Tags = new[] { "Workflows" })
            ]
        public override async Task<ActionResult<WorkflowResponse>> HandleAsync(int id, CancellationToken cancellationToken)
        {
            Workflow workflow = await repository.GetByIdAsync(id);

            if (workflow == null)
            {
                return NotFound();
            }

            return Ok(await Result<WorkflowResponse>.SuccessAsync(mapper.Map<WorkflowResponse>(workflow)));
        }
    }
}
