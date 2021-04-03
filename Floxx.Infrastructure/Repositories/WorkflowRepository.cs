using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Floxx.Core.Entities;
using Floxx.Core.Interfaces;
using Floxx.Infrastructure.Contexts;
using Floxx.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Floxx.Infrastructure.Repositories
{
    public class WorkflowRepository : BaseRepository<Workflow>, IWorkflowRepository
    {
        public WorkflowRepository(FloxxContext dbContext) : base(dbContext)
        {
        }
    }
}
