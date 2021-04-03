using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using AutoMapper;
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
    public class Update : BaseAsyncEndpoint
        .WithRequest<UpdateWorkflowRequest>
        .WithResponse<WorkflowResponse>
    {
        private readonly IWorkflowRepository repository;
        private readonly IMapper mapper;

        public Update(IWorkflowRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpPut("/api/workflows")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(
            Summary = "Updates a Workflow",
            OperationId = "Workflows.Update",
            Tags = new[] { "Workflows" })
        ]
        public override async Task<ActionResult<WorkflowResponse>> HandleAsync(UpdateWorkflowRequest request, CancellationToken cancellationToken)
        {
            var workflow = await repository.GetByIdAsync(request.Id);

            if (workflow == null)
            {
                return NotFound();
            }

            workflow.DisplayNameGerman = request.DisplayNameGerman;
            workflow.DisplayNameEnglish = request.DisplayNameEnglish;

            await repository.UpdateAsync(workflow);

            WorkflowResponse response = mapper.Map<WorkflowResponse>(workflow);

            return Ok(await Result<WorkflowResponse>.SuccessAsync(response));
        }
    }
}
