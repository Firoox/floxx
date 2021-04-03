using Floxx.Shared.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Floxx.Core.Entities;
using Floxx.Infrastructure.Contexts;
using Floxx.Infrastructure.Repositories;
using Floxx.Tests.Builders;
using Xunit;

namespace Floxx.Tests.Data
{
    public class WorkflowRepositoryTests : BaseRepoTestFixture
    {
        [Fact]
        public async void AddsWorkflowAndSetsId()
        {
            WorkflowRepository repository = GetRepository();
            Workflow workflow = new WorkflowBuilder().Build();

            await repository.AddAsync(workflow);

            var newItem = (await repository.ListAsync())
                .FirstOrDefault();

            Assert.Equal(workflow, newItem);
            Assert.True(newItem?.Id > 0);
        }

        [Fact]
        public async Task UpdatesWorklflowAfterAddingIt()
        {

            WorkflowRepository repository = GetRepository();

            string displayNameEnglish = "English";
            string displayNameGerman = "Deutsch";

            Workflow workflow = new WorkflowBuilder().DisplayNames(displayNameEnglish, displayNameGerman).Build();

            await repository.AddAsync(workflow);

            // detach the item so we get a different instance
            dbContext.Entry(workflow).State = EntityState.Detached;

            // fetch the item and update its title
            var newWorkflow = (await repository.ListAsync()).FirstOrDefault(w => w.DisplayNameEnglish == displayNameEnglish && w.DisplayNameGerman == displayNameGerman);
            
            Assert.NotNull(newWorkflow);
            Assert.NotSame(workflow, newWorkflow);


            string newEnglishName = "One";
            string newGermanName = "Zwei";

            newWorkflow.DisplayNameEnglish = newEnglishName;
            newWorkflow.DisplayNameGerman = newGermanName;

            // Update the item
            await repository.UpdateAsync(newWorkflow);
            Workflow updatedWorkflow = (await repository.ListAsync()).FirstOrDefault(w => w.DisplayNameEnglish == newEnglishName && w.DisplayNameGerman == newGermanName);

            Assert.NotNull(updatedWorkflow);
            Assert.NotEqual(workflow.DisplayNameEnglish, updatedWorkflow.DisplayNameEnglish);
            Assert.NotEqual(workflow.DisplayNameGerman, updatedWorkflow.DisplayNameGerman);
            Assert.Equal(newWorkflow.Id, updatedWorkflow.Id);
        }

        private WorkflowRepository GetRepository()
        {
            DbContextOptions<FloxxContext> options = CreateNewContextOptions();
            Mock<IMediator> mockMediator = new Mock<IMediator>();
            Mock<ICurrentUserService> mockCurrentUserService = new Mock<ICurrentUserService>();

            dbContext = new FloxxContext(options, mockCurrentUserService.Object, mockMediator.Object);

            return new WorkflowRepository(dbContext);
        }
    }
}
