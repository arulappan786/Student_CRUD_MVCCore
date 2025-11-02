using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using StudentMVCCoreWb.Controllers;
using StudentMVCCoreWb.Entities;
using StudentMVCCoreWb.Models;
using StudentMVCCoreWb.Repositories;

namespace StudentMVCCore.MSTest;

[TestClass]
public class StudentControllerTest
{
    StudentController? _controller;
    Mock<ILogger<StudentController>>? _mockLogger;
    Mock<IStudentRepository>? _mockRepo;
    Mock<IMapper>? _mockMapper;

    [TestInitialize]
    public void Setup()
    {
        _mockLogger = new Mock<ILogger<StudentController>>();
        _mockRepo = new Mock<IStudentRepository>();
        _mockMapper = new Mock<IMapper>();
        _controller = new StudentController(_mockLogger.Object, _mockRepo.Object, _mockMapper.Object);
    }

    [TestMethod]
    public async Task Index_Returns_View_With_Students()
    {
        // Arrange: Mock repository to return sample students
        var students = new List<Student>
            {
                new Student { Id = 1, Name = "Arul", Address="test", Email="email@gmail.com", MobileNumber="8925719963" },
                new Student { Id = 2, Name = "Kumar", Address = "test", Email="email@gmail.com", MobileNumber="8925719963" }
            };

        _mockRepo?.Setup(repo => repo.GetAllStudents()).ReturnsAsync(students);

        // Optional: Mock mapper if controller uses view models
        var studentViewModels = new List<StudentViewModel>
            {
                new StudentViewModel { Id = 1, Name = "Arul", Address="test", Email="email@gmail.com", MobileNumber="8925719963" },
                new StudentViewModel { Id = 2, Name = "Kumar", Address = "test", Email="email@gmail.com", MobileNumber="8925719963" }
            };

        _mockMapper?.Setup(m => m.Map<IEnumerable<StudentViewModel>>(students)).Returns(studentViewModels);

        // Act
        var result = await (_controller!.Index()) as ViewResult;

        // Assert
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result.Model, typeof(IEnumerable<StudentViewModel>));
        var model = result.Model as IEnumerable<StudentViewModel>;
        Assert.IsNotNull(model);
        Assert.HasCount(2, model);
        Assert.AreEqual("Arul", model.First().Name);
    }

    [TestMethod]
    public async Task Details_ValidId_ReturnsViewWithViewModel()
    {
        // Arrange
        int studentId = 1;
        var student = new Student { Id = 1, Name = "Arul", Address = "test", Email = "email@gmail.com", MobileNumber = "8925719963" };
        var viewModel = new StudentViewModel { Id = 1, Name = "Arul", Address = "test", Email = "email@gmail.com", MobileNumber = "8925719963" };

        _mockRepo?.Setup(r => r.GetStudentById(studentId)).ReturnsAsync(student);
        _mockMapper?.Setup(m => m.Map<StudentViewModel>(student)).Returns(viewModel);

        // Act
        var result = await _controller!.Details(studentId);

        // Assert
        Assert.IsInstanceOfType(result, typeof(ViewResult));
        var viewResult = result as ViewResult;
        Assert.IsNotNull(viewResult?.Model);
        Assert.IsInstanceOfType(viewResult.Model, typeof(StudentViewModel));
        Assert.AreEqual("Arul", ((StudentViewModel)viewResult.Model).Name);
    }

    [TestMethod]
    public async Task Details_InvalidId_ReturnsNotFound()
    {
        // Arrange
        int studentId = 999;
        _mockRepo?.Setup(r => r.GetStudentById(studentId)).ReturnsAsync((Student?)null!);

        // Act
        var result = await _controller!.Details(studentId);

        // Assert
        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        _mockLogger?.Verify(
            l => l.Log(
                LogLevel.Warning,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString() == $"Student with ID {studentId} not found."),
                null,
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once
        );
    }

}
