using FluentAssertions;
using TaskManagement.Exercise.Enums;

namespace TaskManagement.Exercise.Tests.TaskRepository
{
    internal class GetTests : BaseClass
    {
        [Test]
        [TestCase(Status.All, 12)]
        [TestCase(Status.Open, 5)]
        [TestCase(Status.Complete, 7)]
        public async Task Returns_Expected(Status filter, int expectedCount)
        {
            //  Arrange

            //  Act
            var result = await _objectUnderTest.Get(filter);

            //  Assert
            result.Count().Should().Be(expectedCount);
        }
    }
}
