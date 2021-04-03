using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Floxx.Shared.Constants.Permission
{
    public static class PermissionModules
    {
        public static List<string> GeneratePermissionsForModule(string module)
        {
            return new List<string>()
            {
                $"Permissions.{module}.Create",
                $"Permissions.{module}.View",
                $"Permissions.{module}.Edit",
                $"Permissions.{module}.Delete"
            };
        }

        public static List<string> GetAllPermissionsModules()
        {
            return new List<string>()
            {
                Users,
                Roles,
                Workflows
            };
        }

        public const string Users = nameof(Users);
        public const string Roles = nameof(Roles);
        public const string Workflows = nameof(Workflows);
    }
}
