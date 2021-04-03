using System;
using System.ComponentModel.DataAnnotations;

namespace Floxx.Web.Shared.DTO.Workflows
{
    public class AddEditWorkflowPathRequest
    {
        [Required]
        public Guid StepId
        {
            get;
            set;
        }

        public Guid? SuccessorStepId
        {
            get;
            set;
        }

        [Required]
        [StringLength(100)]
        public string DisplayNameGerman
        {
            get;
            set;
        }

        [Required]
        [StringLength(100)]
        public string DisplayNameEnglish
        {
            get;
            set;
        }
    }
}