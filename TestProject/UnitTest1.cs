using Domain.Entity;
using Domain.Ports;
using Moq;

namespace TestProject
{
    public class UnitTest1
    {
        [Fact]
        public async Task Test1Async()
        {
            // Arrange
            var mockRepo = new Mock<IEmployeeRepository>();
            var employId = 1;
            var employee = new Employee { Id = employId, Nombre = "John Doe" };
            mockRepo.Setup(repo => repo.GetEmployeeByIdAsync(employId)).ReturnsAsync(employee);

            // Act
            var result = await mockRepo.Object.GetEmployeeByIdAsync(employId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(employId, result.Id);
            Assert.Equal("John Doe", result.Nombre);
        }
    }
}