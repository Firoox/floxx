using AutoMapper;
using Floxx.Core.Entities;
using Floxx.Web.Shared.DTO.Workflows;

namespace Floxx.Server.Mappings
{
    public class WorkflowPathProfile : Profile
    {
        public WorkflowPathProfile()
        {
            CreateMap<AddEditWorkflowPathRequest, WorkflowPath>().ReverseMap();
        }
    }
}
