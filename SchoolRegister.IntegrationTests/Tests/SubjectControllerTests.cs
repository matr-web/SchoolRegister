using FluentAssertions;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Moq;
using SchoolRegister.BusinessAccess.Interfaces;
using SchoolRegister.DataAccess;
using SchoolRegister.Entities;
using SchoolRegister.Models.Dto_s.SubjectDto_s;
using SchoolRegister.Models.Dto_s.SubjectDto_sl;
using System.Linq.Expressions;

namespace SchoolRegister.IntegrationTests.Tests;

#nullable disable
public class SubjectControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private WebApplicationFactory<Program> _factory;
    private HttpClient _client;
    private Mock<ISubjectService> _subjectServiceMock = new Mock<ISubjectService>();

    public SubjectControllerTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory
           .WithWebHostBuilder(builder =>

           builder.ConfigureServices(services =>
           {
               var dbContextOptions = services
                   .SingleOrDefault(service => service.ServiceType == typeof(DbContextOptions<SchoolRegisterContext>));

               // Register ISubjectService Mock.
               services.AddSingleton(_subjectServiceMock.Object);

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
        // Mock GetSubjectByAsync method from SubjectService, so it returns a SubjectDto with our Fake Subject values.
        var subjectDto = _subjectServiceMock
            .Setup(e => e.GetSubjectByAsync(It.IsNotNull<Expression<Func<SubjectEntity, bool>>>(), It.IsNotNull<string>()))
            .Returns(Task.FromResult(new SubjectDto() { Id = 1 }));

        // Act:
        // Get Response from Get method from SchoolRegister.WebAPI/Controllers/SubjectController.
        var response = await _client.GetAsync("api/Subject/Get?subjectId=" + id);

        // Assert:
        // Check if Response is: 200 OK. 
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
    }

    [Theory]
    [InlineData(-1)]
    public async Task Get_WithInvalidId_ReturnsNotFound(int id)
    {
        // Arrange:
        // Mock GetSubjectByAsync method from SubjectService, so it doesn't find a Subject with given id.
        var subjectDto = _subjectServiceMock
            .Setup(e => e.GetSubjectByAsync(It.IsNotNull<Expression<Func<SubjectEntity, bool>>>(), It.IsNotNull<string>()))
            .Returns(Task.FromResult(new SubjectDto() { Id = 0 }));

        // Act:
        // Get Response from Get method from SchoolRegister.WebAPI/Controllers/SubjectController.
        var response = await _client.GetAsync("api/Subject/Get?subjectId=" + id);

        // Assert:
        // Check if Response is: 404 NotFound. 
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task PostSubject_WithValidModel_ReturnsCreated()
    {
        // Arrange:
        // Create Fake Subject witch proper data.
        var createSubjectDto = new CreateSubjectDto()
        {
            Name = "Fake Subject",
            Description = "Fake Subject Description"
        };

        // Serialize createSubjectDto object to .json.
        var httpContent = createSubjectDto.ToJsonHttpContent();

        // Mock InsertSubjectAsync method from SubjectService, so it generates a SubjectDto with our Fake Subject values.
        var subjectDto = _subjectServiceMock.Setup(e => e.InsertSubjectAsync(It.IsAny<CreateSubjectDto>())).Returns(Task.FromResult(new SubjectDto() 
        { Id = 1,
          Name = createSubjectDto.Name, 
          Description = createSubjectDto.Description 
        }));

        // Act:
        // Get Response from Post method from SchoolRegister.WebAPI/Controllers/SubjectController.
        var response = await _client.PostAsync("api/Subject/Post", httpContent);

        // Assert:
        // Check if Response is: 201 Created. 
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
    }

    [Fact]
    public async Task PostSubject_WithInvalidModel_ReturnsBadRequest()
    {
        // Arrange:
        // Create Fake Subject witch wrong data.
        var createSubjectDto = new CreateSubjectDto()
        {
            Name = null,
            Description = "Fake Subject Description"
        };

        // Serialize createSubjectDto object to .json.
        var httpContent = createSubjectDto.ToJsonHttpContent();

        // Act:
        // Get Response from Post method from SchoolRegister.WebAPI/Controllers/SubjectController.
        var response = await _client.PostAsync("api/Subject/Post", httpContent);

        // Assert:
        // Check if Response is: 400 BadRequest. 
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
    }

    [Theory]
    [InlineData(1)]
    public async Task PutSubject_WithValidModel_ReturnsOk(int id)
    {
        // Arrange:
        // Create Fake Subject witch proper data.
        var updateSubjectDto = new UpdateSubjectDto()
        {
            Id = id,
            Name = "Fake Subject",
            Description = "Fake Subject Description"
        };

        // Serialize updateSubjectDto object to .json.
        var httpContent = updateSubjectDto.ToJsonHttpContent();

        // Mock UpdateSubjectDto method from SubjectService, so it generates a SubjectDto with our Fake Subject values.
        var subjectDto = _subjectServiceMock.Setup(e => e.UpdateSubjectAsync(It.IsAny<UpdateSubjectDto>())).Returns(Task.FromResult(new SubjectDto()
        {
            Id = id,
            Name = updateSubjectDto.Name,
            Description = updateSubjectDto.Description
        }));

        // Act:
        // Get Response from Put method from SchoolRegister.WebAPI/Controllers/SubjectController.
        var response = await _client.PutAsync("api/Subject/Put?subjectId=" + id, httpContent);

        // Assert:
        // Check if Response is: 200 OK. 
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
    }

    [Theory]
    [InlineData(1)]
    public async Task PutSubject_WithInvalidRequest_ReturnsBadRequest(int id)
    {
        // Arrange:
        // Create Fake Subject witch proper data.
        var updateSubjectDto = new UpdateSubjectDto()
        {
            Id = id,
            Name = "Fake Subject",
            Description = "Fake Subject Description"
        };

        // Serialize updateSubjectDto object to .json.
        var httpContent = updateSubjectDto.ToJsonHttpContent();

        // Act:
        // Get Response from Put method from SchoolRegister.WebAPI/Controllers/SubjectController.
        var response = await _client.PutAsync("api/Subject/Put?subjectId=" + id + 1, httpContent);

        // Assert:
        // Check if Response is: 400 BadRequest. 
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
    }


    [Theory]
    [InlineData(1)]
    public async Task Delete_WithValidId_ReturnsNoContent(int id)
    {
        // Arrange:
        // Mock GetSubjectByAsync method from SubjectService, so it generates a SubjectDto with our Fake Subject values.
        var subjectDto = _subjectServiceMock
            .Setup(e => e.GetSubjectByAsync(It.IsNotNull<Expression<Func<SubjectEntity, bool>>>(), null))
            .Returns(Task.FromResult(new SubjectDto() { Id = 1 }));

        // Act:
        // Get Response from Delete method from SchoolRegister.WebAPI/Controllers/SubjectController.
        var response = await _client.DeleteAsync("api/Subject/Delete?subjectId=" + id);

        // Assert:
        // Check if Response is: 204 NoContent. 
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);
    }

    [Theory]
    [InlineData(-1)]
    public async Task Delete_WithInvalidId_ReturnsNotFound(int id)
    {
        // Arrange:
        // Mock GetSubjectByAsync method from SubjectService, so it generates a SubjectDto with Fake Subject values.
        var subjectDto = _subjectServiceMock
            .Setup(e => e.GetSubjectByAsync(It.IsNotNull<Expression<Func<SubjectEntity, bool>>>(), null))
            .Returns(Task.FromResult<SubjectDto>(null));

        // Act:
        // Get Response from Delete method from SchoolRegister.WebAPI/Controllers/SubjectController.
        var response = await _client.GetAsync("api/Subject/Get?subjectId=" + id);

        // Assert:
        // Check if Response is: 404 NotFound. 
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
    }
}
