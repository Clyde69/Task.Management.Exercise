using AutoFixture;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Exercise.Dtos;
using TaskManagement.Exercise.Enums;

namespace TaskManagement.Exercise.Tests.TaskRepository
{
    internal class UpdateTests : BaseClass
    {
        [Test]
        public async Task Changed_Item_Updated()
        {
            //  Arrange
            var index = new Random().Next(0, 11);
            var itemToUpdate = _tasks[index];
            itemToUpdate.Title = "Updated";
            itemToUpdate.Description = "Also Updated";
            if (itemToUpdate.Status.Equals(Status.Open))
            {
                itemToUpdate.Status = Status.Complete;
            }
            else
            {
                itemToUpdate.Status = Status.Open;
            }

            //  Act
            var result = await _objectUnderTest.Update(itemToUpdate);

            //  Assert
            result.Id.Should().NotBe(null);
            result.Id.Should().NotBe(0);
            result.Title.Should().Be(itemToUpdate.Title);
            result.Description.Should().Be(itemToUpdate.Description);
            result.CreatedAt.Should().Be(itemToUpdate.CreatedAt);
            _tasks.Count().Should().Be(12);
            _tasks.First(w => w.Id.Equals(itemToUpdate.Id)).Should().BeEquivalentTo(result);
        }

        [Test]
        public async Task Item_Not_Exists_Exeception_Thrown()
        {
            //  Arrange
            var newTask = _fixture.Build<TaskDto>()
                .With(w => w.Id, 0)
                .Create();

            //  Act
            var action = () => _objectUnderTest.Update(newTask);

            //  Assert
            await action.Should().ThrowAsync<KeyNotFoundException>()
                .WithMessage("No item in the database could be found to update.");
        }
    }
}
