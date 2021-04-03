using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Floxx.Core.Entities;

namespace Floxx.Tests.Builders
{
    public class WorkflowBuilder
    {
        private Workflow workflow = new Workflow();

        public WorkflowBuilder Id(int id)
        {
            workflow.Id = id;
            return this;
        }

        public WorkflowBuilder DisplayNames(string displayNameEnglish, string displayNameGerman)
        {
            workflow.DisplayNameEnglish = displayNameEnglish;
            workflow.DisplayNameGerman = displayNameGerman;

            return this;
        }

        public WorkflowBuilder WithDefaultValues()
        {
            workflow = new Workflow() { Id = 1, DisplayNameEnglish = "English", DisplayNameGerman =  "Deutsch"};

            return this;
        }

        public Workflow Build() => workflow;
    }
}
