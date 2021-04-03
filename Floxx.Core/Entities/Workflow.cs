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
    public class Workflow : AuditableEntity<int>
    {
        public Workflow()
        {
            Steps = new List<Step>();
        }

        [Required]
        [StringLength(100)]
        public string DisplayNameEnglish
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

        [NotMapped]
        public string DisplayName
        {
            get
            {
                if (CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "de")
                {
                    return DisplayNameGerman ?? Id.ToString();
                }
                else
                {
                    return DisplayNameEnglish ?? Id.ToString();
                }
            }
        }

        public ICollection<Step> Steps
        {
            get;
            set;
        }

        public override string ToString()
        {
            return DisplayName;
        }

        public override bool Equals(object obj)
        {
            Workflow otherWorkflow = obj as Workflow;

            if (otherWorkflow == null)
            {
                return false;
            }

            if (this.Id.Equals(otherWorkflow.Id))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
