using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public class List : BaseAsyncEndpoint
        .WithoutRequest
        .WithResponse<List<WorkflowResponse>>
    {
        private readonly IWorkflowRepository repository;
        private readonly IMapper mapper;

        public List(IWorkflowRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet("/api/workflows")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(
            Summary = "Gets all Workflows",
            OperationId = "Workflows.List",
            Tags = new[] { "Workflows" })
        ]
        public override async Task<ActionResult<List<WorkflowResponse>>> HandleAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            IEnumerable<WorkflowResponse> response = (await repository.ListAsync()).Select(w => mapper.Map<WorkflowResponse>(w));

            return Ok(await Result<List<WorkflowResponse>>.SuccessAsync(response.ToList()));
        }
    }
}
