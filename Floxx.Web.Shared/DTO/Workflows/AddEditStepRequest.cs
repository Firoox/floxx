using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Floxx.Web.Shared.DTO.Workflows
{
    public class AddEditStepRequest
    {
        [Required]
        public Guid Id
        {
            get;
            set;
        }

        [Required]
        public int StatusId
        {
            get;
            set;
        }

        [Required]
        public int WorkflowId
        {
            get;
            set;
        }

        [Required]
        public ICollection<AddEditWorkflowPathRequest> WorkflowPaths
        {
            get;
            set;
        }
    }
}
