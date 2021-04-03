using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Floxx.Core.Entities;
using Floxx.Core.Interfaces;
using Floxx.Shared;
using Floxx.Shared.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Floxx.Infrastructure.Contexts
{
    public class FloxxContext : IdentityDbContext<User, IdentityRole, string>
    {
        private readonly ICurrentUserService currentUserService;
        private readonly IMediator mediator;

        public FloxxContext(DbContextOptions<FloxxContext> options, ICurrentUserService currentUserService, IMediator mediator)
            : base(options)
        {
            this.currentUserService = currentUserService;
            this.mediator = mediator;
        }

        public DbSet<Status> Statuses
        {
            get;
            set;
        }

        public DbSet<Step> Steps
        {
            get;
            set;
        }

        public DbSet<Workflow> Workflows
        {
            get;
            set;
        }

        public DbSet<WorkflowPath> WorkflowPaths
        {
            get; 
            set;

        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            UpdateAuditableEntities();

            int result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            if (mediator != null)
            {
                await DispatchEvents();
            }

            return result;
        }

        public override int SaveChanges()
        {
            return SaveChangesAsync().GetAwaiter().GetResult();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            MapStep(modelBuilder);
            MapStatus(modelBuilder);
            MapWorkflow(modelBuilder);
            MapWorkflowPath(modelBuilder);
        }

        private void MapStatus(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Status>(e =>
            {
                e.ToTable(nameof(Status));
                e.Property(s => s.Id).HasMaxLength(100);
            });
        }

        private void MapStep(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Step>(e =>
            {
                e.ToTable(nameof(Step));
                e.HasOne(s => s.Workflow).WithMany(w => w.Steps).HasForeignKey(s => s.WorkflowId);
            });
        }

        private void MapWorkflow(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Workflow>(e =>
            {
                e.ToTable(nameof(Workflow));
            });
        }

        private void MapWorkflowPath(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WorkflowPath>(e =>
            {
                e.ToTable(nameof(WorkflowPath));
                e.Property(wp => wp.Id).ValueGeneratedOnAdd();
                e.HasIndex(wp => new { wp.StepId, wp.SuccessorStepId }).IsUnique();
                e.HasOne(wp => wp.Step).WithMany(s => s.WorkflowPaths).HasForeignKey(wp => wp.StepId);
                e.HasOne(wp => wp.SuccessorStep).WithMany().HasForeignKey(wp => wp.SuccessorStepId);
            });
        }

        private void UpdateAuditableEntities()
        {
            foreach (var entry in ChangeTracker.Entries<IAuditableEntity>().ToList())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedOn = DateTime.UtcNow;
                        entry.Entity.CreatedBy = currentUserService.UserId;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedOn = DateTime.UtcNow;
                        entry.Entity.LastModifiedBy = currentUserService.UserId;
                        break;
                }
            }
        }

        private async Task DispatchEvents()
        {
            BaseEntity[] entitiesWithEvents = ChangeTracker.Entries<BaseEntity>()
                .Select(e => e.Entity)
                .Where(e => e.Events.Any())
                .ToArray();

            foreach (var entity in entitiesWithEvents)
            {
                BaseDomainEvent[] events = entity.Events.ToArray();

                entity.Events.Clear();

                foreach (BaseDomainEvent domainEvent in events)
                {
                    await mediator.Publish(domainEvent).ConfigureAwait(false);
                }
            }
        }
    }
}
