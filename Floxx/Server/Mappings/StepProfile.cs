using AutoMapper;
using Floxx.Core.Entities;
using Floxx.Web.Shared.DTO.Workflows;

namespace Floxx.Server.Mappings
{
    public class StepProfile : Profile
    {
        public StepProfile()
        {
            CreateMap<AddEditStepRequest, Step>().ReverseMap();
        }
    }
}
