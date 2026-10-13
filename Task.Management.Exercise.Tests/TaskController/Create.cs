using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TaskManagement.Exercise.Dtos;
using TaskManagement.Exercise.Database.Repositories;
using TestArtifacts = TaskManagement.Exercise.Controllers;

namespace TaskManagement.Exercise.Tests.TaskController
{
    [TestFixture]
    internal class CreateTests
    {
        private Mock<ITaskRepository> _mockRepo = null!;
        private TestArtifacts.TaskController _controller = null!;

        [SetUp]
        public void Setup()
        {
            _mockRepo = new Mock<ITaskRepository>();
            _controller = new TestArtifacts.TaskController(_mockRepo.Object);
        }

        [Test]
        public async Task Create_Returns_Ok_With_Created_Task()
        {
            // Arrange
            var incoming = new TaskDto { Id = null, Title = "New", CreatedAt = DateTime.UtcNow };
            var created = new TaskDto { Id = 123, Title = incoming.Title, CreatedAt = incoming.CreatedAt };

            _mockRepo.Setup(r => r.Create(incoming)).ReturnsAsync(created);

            // Act
            var result = await _controller.Create(incoming);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var ok = (OkObjectResult)result;
            ok.Value.Should().BeEquivalentTo(created);
        }
    }
}
