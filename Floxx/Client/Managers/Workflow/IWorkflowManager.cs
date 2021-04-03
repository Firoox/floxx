using System.Collections.Generic;
using System.Threading.Tasks;
using Floxx.Shared.Interfaces;
using Floxx.Web.Client.Client.Managers;
using Floxx.Web.Shared.DTO.Workflows;

namespace Floxx.Client.Managers.Workflow
{
    public interface IWorkflowManager : IManager
    {
        Task<IResult<WorkflowResponse>> CreateAsync(NewWorkflowRequest request);

        Task<IResult<List<WorkflowResponse>>> GetAllAsync();

        Task<IResult<WorkflowResponse>> GetByIdAsync(int id);

        Task<IResult<WorkflowResponse>> UpdateAsync(UpdateWorkflowRequest request);

        Task<string> DeleteAsync(int id);
    }
}
