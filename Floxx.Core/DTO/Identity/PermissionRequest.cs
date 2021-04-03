using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Floxx.Core.DTO.Identity
{
    public class PermissionRequest
    {
        public string RoleId { get; set; }
        public IList<RoleClaimsRequest> RoleClaims { get; set; }
    }
}
