using AutoMapper;
using Floxx.Core.Entities;
using Floxx.Web.Shared.DTO.Workflows;

namespace Floxx.Server.Mappings
{
    public class WorkflowProfile : Profile
    {
        public WorkflowProfile()
        {
            CreateMap<WorkflowResponse, Workflow>().ReverseMap();
            CreateMap<NewWorkflowRequest, Workflow>().ReverseMap();
        }
    }
}
