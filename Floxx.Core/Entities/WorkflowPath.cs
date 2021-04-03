using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Floxx.Core.Entities
{
    public class WorkflowPath : AuditableEntity<Guid>
    { 
        [Required]
        public Guid StepId
        {
            get;
            set;
        }

        public Step Step
        {
            get;
            set;
        }

        public Guid? SuccessorStepId
        {
            get;
            set;
        }

        public Step SuccessorStep
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

        [NotMapped]
        public string DisplayName
        {
            get
            {
                if (CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "de")
                {
                    return DisplayNameGerman;
                }
                else
                {
                    return DisplayNameEnglish;
                }
            }
        }
    }
}
