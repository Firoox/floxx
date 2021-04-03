using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Floxx.Client.Routes
{
    public class WorkflowEndpoint
    {
        public static string GetAll = "api/workflows";

        public static string Get(int workflowId)
        {
            return $"api/workflows/{workflowId}";
        }

        public static string Delete(int workflowId)
        {
            return $"api/workflows/{workflowId}";
        }

        public static string Save = "api/workflows";
    }
}
