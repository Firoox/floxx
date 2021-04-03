using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Floxx.Core.Entities
{
    public class Step : AuditableEntity<Guid>
    {
        public Step()
        {
            WorkflowPaths = new List<WorkflowPath>();
        }

        [Required]
        public int StatusId
        {
            get;
            set;
        }

        public Status Status
        {
            get;
            set;
        }

        [Required]
        public int WorkflowId
        {
            get;
            set;
        }

        public Workflow Workflow
        {
            get;
            set;
        }

        public ICollection<WorkflowPath> WorkflowPaths
        {
            get;
            set;
        }

        public override string ToString()
        {
            return $"Workflow: {WorkflowId} - Step: {Id} - Status: {StatusId}";
        }

        public override bool Equals(object? obj)
        {
            Step otherStep = obj as Step;

            if (otherStep == null)
            {
                return false;
            }

            if (this.Id.Equals(otherStep.Id))
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
