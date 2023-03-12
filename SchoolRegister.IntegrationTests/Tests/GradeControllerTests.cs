using FluentAssertions;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Moq;
using SchoolRegister.BusinessAccess.Interfaces;
using SchoolRegister.DataAccess;
using SchoolRegister.Entities;
using SchoolRegister.Models.Dto_s.GradeDto_s;
using System.Linq.Expressions;

namespace SchoolRegister.IntegrationTests.Tests;

#nullable disable
public class GradeControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private WebApplicationFactory<Program> _factory;
    private HttpClient _client;
    private Mock<IGradeService> _gradeServiceMock = new Mock<IGradeService>();

    public GradeControllerTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory
           .WithWebHostBuilder(builder =>

           builder.ConfigureServices(services =>
           {
               var dbContextOptions = services
                   .SingleOrDefault(service => service.ServiceType == typeof(DbContextOptions<SchoolRegisterContext>));

               // Register IGradeService Mock.
               services.AddSingleton(_gradeServiceMock.Object);

               // Remove current Database Registration.
               services.Remove(dbContextOptions);

               // Register our Fake Policy Evaluator.
               services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();

               // Register our Fake User.
               services.AddMvc(option => option.Filters.Add(new FakeUserFilter(StaticData.role_administrator)));

               // Implement InMemoryDb.
               services.AddDbContext<SchoolRegisterContext>(options => options.UseInMemoryDatabase("SchoolRegisterDb"));

           }));

        _client = _factory.CreateClient();
    }

    [Theory]
    [InlineData(1)]
    public async Task Get_WithValidId_ReturnsOk(int id)
    {
        // Arrange:
        // Mock GetGradeByAsync method from GradeService, so it returns a GradeDto with our Fake Grade values.
        var subjectDto = _gradeServiceMock
            .Setup(e => e.GetGradeByAsync(It.IsNotNull<Expression<Func<GradeEntity, bool>>>(), It.IsNotNull<string>()))
            .Returns(Task.FromResult(new GradeDto() { Id = id }));

        // Act:
        // Get Response from Get method from SchoolRegister.WebAPI/Controllers/GradeController.
        var response = await _client.GetAsync("api/Grade/Get?gradeId=" + id);

        // Assert:
        // Check if Response is: 200 OK. 
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
    }

    [Theory]
    [InlineData(-1)]
    public async Task Get_WithInvalidId_ReturnsNotFound(int id)
    {
        // Arrange:
        // Mock GetGradeByAsync method from GradeService, so it doesn't find a Grade with given id.
        var subjectDto = _gradeServiceMock
            .Setup(e => e.GetGradeByAsync(It.IsNotNull<Expression<Func<GradeEntity, bool>>>(), It.IsNotNull<string>()))
            .Returns(Task.FromResult(new GradeDto() { Id = 0 }));

        // Act:
        // Get Response from Get method from SchoolRegister.WebAPI/Controllers/GradeController.
        var response = await _client.GetAsync("api/Grade/Get?gradeId=" + id);

        // Assert:
        // Check if Response is: 404 NotFound. 
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task PostSubject_WithValidModel_ReturnsCreated()
    {
        // Arrange:
        // Create Fake Grade witch proper data.
        var createGradeDto = new CreateGradeDto()
        {
            GradeValue = GradeValue.B_VeryGood,
            StudentId = 1,
            SubjectId = 2
        };

        // Serialize createGradeDto object to .json.
        var httpContent = createGradeDto.ToJsonHttpContent();

        // Mock InsertGradeAsync method from GradeService, so it generates a GradeDto with our Fake Grade values.
        var gradeDto = _gradeServiceMock.Setup(e => e.InsertGradeAsync(It.IsAny<CreateGradeDto>())).Returns(Task.FromResult(new GradeDto()
        {
            Id = 1,
            GradeValue = createGradeDto.GradeValue,
            StudentId = createGradeDto.StudentId,
            SubjectId = createGradeDto.SubjectId
        }));

        // Act:
        // Get Response from Post method from SchoolRegister.WebAPI/Controllers/GradeController.
        var response = await _client.PostAsync("api/Grade/Post", httpContent);

        // Assert:
        // Check if Response is: 201 Created. 
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
    }

    [Fact]
    public async Task PostGrade_WithInvalidModel_ReturnsBadRequest()
    {
        // Arrange:
        // Create Fake Grade witch wrong data.
        var createGradeDto = new CreateGradeDto() {};

        // Serialize createGradeDto object to .json.
        var httpContent = createGradeDto.ToJsonHttpContent();

        // Act:
        // Get Response from Post method from SchoolRegister.WebAPI/Controllers/GradeController.
        var response = await _client.PostAsync("api/Grade/Post", httpContent);

        // Assert:
        // Check if Response is: 400 BadRequest. 
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
    }

    [Theory]
    [InlineData(1)]
    public async Task PutSubject_WithValidModel_ReturnsOk(int id)
    {
        // Arrange:
        // Create Fake Grade witch proper data.
        var updateGradeDto = new UpdateGradeDto()
        {
            Id = id,
            GradeValue = GradeValue.B_VeryGood
        };

        // Serialize updateGradeDto object to .json.
        var httpContent = updateGradeDto.ToJsonHttpContent();

        // Mock UpdateGradeDto method from GradeService, so it generates a GradeDto with our Fake Grade values.
        var gradeDto = _gradeServiceMock.Setup(e => e.UpdateGradeAsync(It.IsAny<UpdateGradeDto>())).Returns(Task.FromResult(new GradeDto()
        {
            Id = 1,
            GradeValue = updateGradeDto.GradeValue
        }));

        // Act:
        // Get Response from Put method from SchoolRegister.WebAPI/Controllers/GradeController.
        var response = await _client.PutAsync("api/Grade/Put?gradeId=" + id, httpContent);

        // Assert:
        // Check if Response is: 200 OK. 
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
    }

    [Theory]
    [InlineData(1)]
    public async Task PutSubject_WithInvalidRequest_ReturnsBadRequest(int id)
    {
        // Arrange:
        // Create Fake Grade witch proper data.
        var updateGradeDto = new UpdateGradeDto()
        {
            Id = id,
            GradeValue = GradeValue.B_VeryGood
        };

        // Serialize updateGradeDto object to .json.
        var httpContent = updateGradeDto.ToJsonHttpContent();

        // Act:
        // Get Response from Put method from SchoolRegister.WebAPI/Controllers/GradeController.
        var response = await _client.PutAsync("api/Grade/Put?gradeId=" + id + 1, httpContent);

        // Assert:
        // Check if Response is: 400 BadRequest. 
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
    }


    [Theory]
    [InlineData(1)]
    public async Task Delete_WithValidId_ReturnsNoContent(int id)
    {
        // Arrange:
        // Mock GetGradeByAsync method from GradeService, so it generates a GradeDto with Fake Grade values.
        var subjectDto = _gradeServiceMock
            .Setup(e => e.GetGradeByAsync(It.IsNotNull<Expression<Func<GradeEntity, bool>>>(), null))
            .Returns(Task.FromResult(new GradeDto() { Id = 1 }));

        // Act:
        // Get Response from Delete method from SchoolRegister.WebAPI/Controllers/GradeController.
        var response = await _client.DeleteAsync("api/Grade/Delete?subjectId=" + id);

        // Assert:
        // Check if Response is: 204 NoContent. 
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);
    }

    [Theory]
    [InlineData(-1)]
    public async Task Delete_WithInvalidId_ReturnsNotFound(int id)
    {
        // Arrange:
        // Mock GetGradeByAsync method from GradeService, so it generates a GradeDto with Fake Grade values.
        var subjectDto = _gradeServiceMock
            .Setup(e => e.GetGradeByAsync(It.IsNotNull<Expression<Func<GradeEntity, bool>>>(), null))
            .Returns(Task.FromResult<GradeDto>(null));

        // Act:
        // Get Response from Delete method from SchoolRegister.WebAPI/Controllers/GradeController.
        var response = await _client.GetAsync("api/Grade/Get?gradeId=" + id);

        // Assert:
        // Check if Response is: 404 NotFound. 
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
    }
}
