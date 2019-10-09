using CluedIn.Core.Data;
using CluedIn.Core.Data.Parts;
using CluedIn.Core.Messages.Processing;
using CluedIn.ExternalSearch.Providers.EventBrite;
using CluedIn.Testing.Base.ExternalSearch;
using Moq;
using Xunit;

namespace ExternalSearch.EventBrite.Integration.Tests
{
    public class EventBriteTests : BaseExternalSearchTest<EventBriteExternalSearchProvider>
    {
        // TODO
        //[Fact]
        //public void Test()
        //{
        //    // Arrange
        //    this.testContext = new TestContext();

        //    IEntityMetadata entityMetadata = new EntityMetadataPart()
        //        {
        //            Name        = "Sitecore",
        //            EntityType  = EntityType.Organization,
        //        };

        //    var externalSearchProvider  = new Mock<EventBriteExternalSearchProvider>(MockBehavior.Loose);
        //    var clues                   = new List<CompressedClue>();

        //    externalSearchProvider.CallBase = true;

        //    this.testContext.ProcessingHub.Setup(h => h.SendCommand(It.IsAny<ProcessClueCommand>())).Callback<IProcessingCommand>(c => clues.Add(((ProcessClueCommand)c).Clue));
        //    this.testContext.Container.Register(Component.For<IExternalSearchProvider>().UsingFactoryMethod(() => externalSearchProvider.Object));

        //    var context         = this.testContext.Context.ToProcessingContext();
        //    var command         = new ExternalSearchCommand();
        //    var actor           = new ExternalSearchProcessing(context.ApplicationContext);
        //    var workflow        = new Mock<Workflow>(MockBehavior.Loose, context, new EmptyWorkflowTemplate<ExternalSearchCommand>());

        //    workflow.CallBase = true;

        //    command.With(context);
        //    command.OrganizationId  = context.Organization.Id;
        //    command.EntityMetaData  = entityMetadata;
        //    command.Workflow        = workflow.Object;
        //    context.Workflow        = command.Workflow;

        //    // Act
        //    var result = actor.ProcessWorkflowStep(context, command);
        //    Assert.Equal(WorkflowStepResult.Repeat.SaveResult, result.SaveResult);

        //    result = actor.ProcessWorkflowStep(context, command);
        //    Assert.Equal(WorkflowStepResult.Success.SaveResult, result.SaveResult);
        //    context.Workflow.AddStepResult(result);

        //    context.Workflow.ProcessStepResult(context, command);

        //    // Assert
        //    this.testContext.ProcessingHub.Verify(h => h.SendCommand(It.IsAny<ProcessClueCommand>()), Times.AtLeastOnce);

        //    Assert.True(clues.Count > 0);
        //}

        //[InlineData("Sitecore")]
        //[InlineData("CluedIn")]
        //public void TestClueProduction(string name)
        //{
        //    IEntityMetadata entityMetadata = new EntityMetadataPart()
        //    {
        //        Name = name,
        //        EntityType = EntityType.Organization
        //    };

        //    Setup(null, entityMetadata);

        //    // Assert
        //    this.testContext.ProcessingHub.Verify(h => h.SendCommand(It.IsAny<ProcessClueCommand>()), Times.AtLeastOnce);
        //    Assert.True(clues.Count > 0);
        //}

        [Theory]
        [InlineData("asdndsafakjfnwekjnrkjnqe213wrsadfasdf")]
        [InlineData("")]
        [InlineData(null)]
        [Trait("Category", "slow")]
        public void TestNoClueProduction(string name)
        {
            IEntityMetadata entityMetadata = new EntityMetadataPart()
            {
                Name = name,
                EntityType = EntityType.Organization
            };

            Setup(null, entityMetadata);

            // Assert
            this.testContext.ProcessingHub.Verify(h => h.SendCommand(It.IsAny<ProcessClueCommand>()), Times.Never);
            Assert.True(clues.Count == 0);
        }

        [Theory]
        [InlineData("null")]
        [InlineData("empty")]
        [InlineData("nonWorking")]
        [Trait("Category", "slow")]
        public void TestInvalidApiToken(string provider)
        {
            var tokenProvider = GetProviderByName(provider);
            object[] parameters = { tokenProvider };

            IEntityMetadata entityMetadata = new EntityMetadataPart()
            {
                Name = "Sitecore",
                EntityType = EntityType.Organization
            };

            Setup(parameters, entityMetadata);

            // Assert
            this.testContext.ProcessingHub.Verify(h => h.SendCommand(It.IsAny<ProcessClueCommand>()), Times.Never);
            Assert.True(clues.Count == 0);
        }
    }
}
