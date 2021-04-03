using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using AutoMapper;
using Floxx.Core.Entities;
using Floxx.Core.Interfaces;
using Floxx.Shared.Interfaces;
using Floxx.Shared.Wrapper;
using Floxx.Web.Shared.DTO.Workflows;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MudBlazor;
using Swashbuckle.AspNetCore.Annotations;

namespace Floxx.Server.Endpoints.Workflows
{
    public class Create : BaseAsyncEndpoint.WithRequest<NewWorkflowRequest>.WithResponse<WorkflowResponse>
    {
        private readonly IWorkflowRepository repository;
        private readonly IMapper mapper;

        public Create(IWorkflowRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpPost("/api/workflows")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(IResult<WorkflowResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(
            Summary = "Creates a new Workflow",
            OperationId = "Workflows.Create",
            Tags = new[] { "Workflows" })
        ]
        public override async Task<ActionResult<WorkflowResponse>> HandleAsync(NewWorkflowRequest request, CancellationToken cancellationToken = default)
        {
            Workflow workflow = mapper.Map<Workflow>(request);

            workflow = await repository.AddAsync(workflow);

            WorkflowResponse response = mapper.Map<WorkflowResponse>(workflow);

            return Created($"api/workflows/{response.Id}", await Result<WorkflowResponse>.SuccessAsync(response));
        } 
    }
}
