using AutoFixture;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using TaskManagement.Exercise.Database.Contexts;
using TaskManagement.Exercise.Dtos;
using TaskManagement.Exercise.Enums;
using TestArtifacts = TaskManagement.Exercise.Database.Repositories;

namespace TaskManagement.Exercise.Tests.TaskRepository
{
    internal class GetTests
    {
        internal TestArtifacts.TaskRepository _objectUnderTest;
        internal Mock<TaskManagementDbContext> _mockContext;

        internal Fixture _fixture;

        internal List<TaskDto> _tasks;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();

            _mockContext = new Mock<TaskManagementDbContext>();

            var openTasks = _fixture.Build<TaskDto>()
                .With(w => w.Status, Status.Open)
                .CreateMany(5);
            var completeTasks = _fixture.Build<TaskDto>()
                .With(w => w.Status, Status.Complete)
                .CreateMany(7);

            _tasks = new List<TaskDto>();
            _tasks.AddRange(openTasks);
            _tasks.AddRange(completeTasks);

            var mockSet = new Mock<DbSet<TaskDto>>();
            mockSet.As<IQueryable<TaskDto>>().Setup(m => m.Provider).Returns(_tasks.AsQueryable().Provider);
            mockSet.As<IQueryable<TaskDto>>().Setup(m => m.Expression).Returns(_tasks.AsQueryable().Expression);
            mockSet.As<IQueryable<TaskDto>>().Setup(m => m.ElementType).Returns(_tasks.AsQueryable().ElementType);
            mockSet.As<IQueryable<TaskDto>>().Setup(m => m.GetEnumerator()).Returns(() => _tasks.AsQueryable().GetEnumerator());

            _mockContext.Setup(c => c.Tasks).Returns(mockSet.Object);

            _objectUnderTest = new TestArtifacts.TaskRepository(_mockContext.Object);
        }

        [Test]
        public async Task Filter_Is_All_Returns_All()
        {
            //  Arrange
            var filter = Status.All;

            //  Act
            var result = await _objectUnderTest.Get(filter);

            //  Assert
            result.Count().Should().Be(12);
        }

        [Test]
        public async Task Filter_Is_Open_Only_Returns_Open()
        {
            //  Arrange
            var filter = Status.Open;

            //  Act
            var result = await _objectUnderTest.Get(filter);

            //  Assert
            result.Count().Should().Be(5);
        }

        [Test]
        public async Task Filter_Is_Complete_Only_Returns_Complete()
        {
            //  Arrange
            var filter = Status.Complete;

            //  Act
            var result = await _objectUnderTest.Get(filter);

            //  Assert
            result.Count().Should().Be(7);
        }
    }
}
