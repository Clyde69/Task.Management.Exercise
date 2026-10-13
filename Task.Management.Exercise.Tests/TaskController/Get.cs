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
    internal class GetTests
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
        public async Task GetAll_Returns_All_Tasks()
        {
            // Arrange
            var tasks = new List<TaskDto>
            {
                new TaskDto { Id = 1, Title = "A", CreatedAt = DateTime.UtcNow, Status = Status.Open },
                new TaskDto { Id = 2, Title = "B", CreatedAt = DateTime.UtcNow, Status = Status.Complete }
            }.AsQueryable();

            _mockRepo.Setup(r => r.Get(Status.All)).ReturnsAsync(tasks);

            // Act
            var result = await _controller.GetAll();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var ok = (OkObjectResult)result;
            ok.Value.Should().BeAssignableTo<IQueryable<TaskDto>>();
            ((IQueryable<TaskDto>)ok.Value).Count().Should().Be(2);
        }

        [Test]
        public async Task GetAll_With_Status_Filter_Returns_Filtered_Tasks()
        {
            // Arrange
            var openTasks = new List<TaskDto>
            {
                new TaskDto { Id = 1, Title = "A", CreatedAt = DateTime.UtcNow, Status = Status.Open }
            }.AsQueryable();

            _mockRepo.Setup(r => r.Get(Status.Open)).ReturnsAsync(openTasks);

            // Act
            var result = await _controller.GetAll(Status.Open);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var ok = (OkObjectResult)result;
            var returned = (IQueryable<TaskDto>)ok.Value;
            returned.Should().HaveCount(1);
            returned.First().Status.Should().Be(Status.Open);
        }
    }
}
