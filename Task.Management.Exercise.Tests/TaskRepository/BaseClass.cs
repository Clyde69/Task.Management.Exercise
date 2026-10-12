using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using Moq;
using TaskManagement.Exercise.Database.Contexts;
using TaskManagement.Exercise.Dtos;
using TaskManagement.Exercise.Enums;
using TestArtifacts = TaskManagement.Exercise.Database.Repositories;

namespace TaskManagement.Exercise.Tests.TaskRepository
{
    internal abstract class BaseClass
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

            _mockContext.Setup(c => c.Tasks.Add(It.IsAny<TaskDto>()))
                .Callback((TaskDto task) =>
                {
                    var nextId = _tasks.Max(c => c.Id) + 1;
                    task.Id = nextId;
                    _tasks.Add(task);
                }); 
            _mockContext.Setup(c => c.Tasks.Update(It.IsAny<TaskDto>()))
                .Callback((TaskDto task) =>
                {
                    var oldTask = _tasks.First(w => w.Id.Equals(task.Id));
                    var index = _tasks.IndexOf(oldTask);
                    _tasks[index] = task;
                });

            _objectUnderTest = new TestArtifacts.TaskRepository(_mockContext.Object);
        }

    }
}
