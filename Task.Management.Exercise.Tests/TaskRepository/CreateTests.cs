using AutoFixture;
using FluentAssertions;
using TaskManagement.Exercise.Dtos;

namespace TaskManagement.Exercise.Tests.TaskRepository
{
    internal class CreateTests : BaseClass
    {
        [Test]
        public async Task Create_Id_Is_Null_Adds_Task_Returns_Task_With_DBId()
        {
            //  Arrange
            var newTask = _fixture.Build<TaskDto>()
                .With(w => w.Id, (int?)null)
                .Create();

            //  Act
            var result = await _objectUnderTest.Create(newTask);

            //  Assert
            result.Id.Should().NotBe(null);
            result.Id.Should().NotBe(0);
            result.Title.Should().Be(newTask.Title);
            result.Description.Should().Be(newTask.Description);
            result.CreatedAt.Should().Be(newTask.CreatedAt);
            _tasks.Count().Should().Be(13);
            _tasks.Last().Should().BeEquivalentTo(result);
        }

        [Test]
        public async Task Create_Id_Is_Zero_Adds_Task_Returns_Task_With_DBId()
        {
            //  Arrange
            var newTask = _fixture.Build<TaskDto>()
                .With(w => w.Id, 0)
                .Create();

            //  Act
            var result = await _objectUnderTest.Create(newTask);

            //  Assert
            result.Id.Should().NotBe(null);
            result.Id.Should().NotBe(0);
            result.Title.Should().Be(newTask.Title);
            result.Description.Should().Be(newTask.Description);
            result.CreatedAt.Should().Be(newTask.CreatedAt);
            _tasks.Count().Should().Be(13);
            _tasks.Last().Should().BeEquivalentTo(result);
        }
    }
}
