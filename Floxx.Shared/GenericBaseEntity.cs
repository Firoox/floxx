using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Floxx.Shared
{
    public abstract class BaseEntity<TId> : BaseEntity
    {
        public TId Id { get; set; }
    }
}
