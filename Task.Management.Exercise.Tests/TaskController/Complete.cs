using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TaskManagement.Exercise.Dtos;
using TaskManagement.Exercise.Enums;
using TaskManagement.Exercise.Database.Repositories;
using TestArtifacts = TaskManagement.Exercise.Controllers;

namespace TaskManagement.Exercise.Tests.TaskController
{
    [TestFixture]
    internal class CompleteTests
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
        public async Task Complete_Returns_Updated_Task_When_Found()
        {
            // Arrange
            var id = 5;
            var existing = new TaskDto { Id = id, Title = "T", CreatedAt = DateTime.UtcNow, Status = Status.Open };
            var updated = new TaskDto { Id = id, Title = "T", CreatedAt = existing.CreatedAt, Status = Status.Complete };

            _mockRepo.Setup(r => r.GetById(id)).ReturnsAsync(existing);
            _mockRepo.Setup(r => r.Update(It.Is<TaskDto>(t => t.Id == id && t.Status == Status.Complete)))
                     .ReturnsAsync(updated);

            // Act
            var result = await _controller.Complete(id);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var ok = (OkObjectResult)result;
            ok.Value.Should().BeEquivalentTo(updated);
        }

        [Test]
        public void Complete_Throws_KeyNotFoundException_When_Not_Found()
        {
            // Arrange
            var id = 999;
            _mockRepo.Setup(r => r.GetById(id)).ReturnsAsync((TaskDto?)null);

            // Act / Assert
            Assert.ThrowsAsync<KeyNotFoundException>(async () => await _controller.Complete(id));
        }
    }
}
