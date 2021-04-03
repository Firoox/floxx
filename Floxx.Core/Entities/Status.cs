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
    public class Status : AuditableEntity<string>
    {
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
                    return DisplayNameGerman ?? Id;
                }
                else
                {
                    return DisplayNameEnglish ?? Id;
                }
            }
        }

        public override string ToString()
        {
            return DisplayName;
        }

        public override bool Equals(object obj)
        {
            Status otherStatus = obj as Status;

            if (otherStatus == null)
            {
                return false;
            }

            if (this.Id == otherStatus.Id)
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
            return Id?.GetHashCode() ?? base.GetHashCode();
        }
    }
}
