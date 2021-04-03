using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Floxx.Shared.Constants.Permission
{
    public static class Permissions
    {
        public static class Users
        {
            public const string View = "Permissions.Users.View";
            public const string Create = "Permissions.Users.Create";
            public const string Edit = "Permissions.Users.Edit";
            public const string Delete = "Permissions.Users.Delete";
        }

        public static class Roles
        {
            public const string View = "Permissions.Roles.View";
            public const string Create = "Permissions.Roles.Create";
            public const string Edit = "Permissions.Roles.Edit";
            public const string Delete = "Permissions.Roles.Delete";
        }

        public static class Workflows
        {
            public const string View = "Permissions.Workflows.View";
            public const string Create = "Permissions.Workflows.Create";
            public const string Edit = "Permissions.Workflows.Edit";
            public const string Delete = "Permissions.Workflows.Delete";
        }
    }
}
