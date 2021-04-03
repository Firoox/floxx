using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Floxx.Core.DTO.Identity
{
    public class PermissionResponse
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public List<RoleClaimsResponse> RoleClaims { get; set; }
    }
}
