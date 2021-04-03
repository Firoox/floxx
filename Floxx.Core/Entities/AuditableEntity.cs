using Floxx.Core.Interfaces;
using Floxx.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Floxx.Core.Entities
{
    public abstract class AuditableEntity<TId> : BaseEntity<TId>, IAuditableEntity
    { 
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
    }
}
