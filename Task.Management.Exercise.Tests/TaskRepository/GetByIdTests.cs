using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Exercise.Enums;

namespace TaskManagement.Exercise.Tests.TaskRepository
{
    internal class GetByIdTests : BaseClass
    {
        [Test]
        public async Task Returns_Expected_When_Valid_Id()
        {
            //  Arrange
            var id = _tasks.First().Id;

            //  Act
            var result = await _objectUnderTest.GetById(id.Value);

            //  Assert
            result.Should().BeEquivalentTo(_tasks.First());
        }

        [Test]
        public async Task Returns_Null_When_Invalid_Id()
        { 
            //  Arrange
            var id = _tasks.Max(w => w.Id) + 1;

            //  Act
            var result = await _objectUnderTest.GetById(id.Value);

            //  Assert
            result.Should().BeNull();
        }
    }
}
