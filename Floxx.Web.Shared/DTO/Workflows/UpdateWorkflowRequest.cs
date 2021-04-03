using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Floxx.Web.Shared.DTO.Workflows
{
    public class UpdateWorkflowRequest
    {
        [Required]
        public int Id
        {
            get;
            set;
        }

        [Required]
        public string DisplayNameGerman
        {
            get;
            set;
        }

        [Required]
        public string DisplayNameEnglish
        {
            get;
            set;
        }
    }
}
