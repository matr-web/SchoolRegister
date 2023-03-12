using FluentAssertions;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Moq;
using SchoolRegister.BusinessAccess.Interfaces;
using SchoolRegister.DataAccess;
using SchoolRegister.Entities;
using SchoolRegister.Models.Dto_s.GroupDto_s;
using System.Linq.Expressions;

namespace SchoolRegister.IntegrationTests.Tests;

#nullable disable
public class GroupControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private WebApplicationFactory<Program> _factory;
    private HttpClient _client;
    private Mock<IGroupService> _groupServiceMock = new Mock<IGroupService>();

    public GroupControllerTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory
           .WithWebHostBuilder(builder =>

           builder.ConfigureServices(services =>
           {
               var dbContextOptions = services
                   .SingleOrDefault(service => service.ServiceType == typeof(DbContextOptions<SchoolRegisterContext>));

               // Register IGroupService Mock.
               services.AddSingleton(_groupServiceMock.Object);

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
        // Mock GetGroupByAsync method from GroupService, so it returns a GroupDto with our Fake Group values.
        var groupDto = _groupServiceMock
            .Setup(e => e.GetGroupByAsync(It.IsNotNull<Expression<Func<GroupEntity, bool>>>(), It.IsNotNull<string>()))
            .Returns(Task.FromResult(new GroupDto() { Id = 1 }));

        // Act:
        // Get Response from Get method from SchoolRegister.WebAPI/Controllers/GroupController.
        var response = await _client.GetAsync("api/Group/Get?groupId=" + id);

        // Assert:
        // Check if Response is: 200 OK. 
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
    }

    [Theory]
    [InlineData(-1)]
    public async Task Get_WithInvalidId_ReturnsNotFound(int id)
    {
        // Arrange:
        // Mock GetGroupByAsync method from GroupService, so it doesn't find a Group with given id.
        var groupDto = _groupServiceMock
            .Setup(e => e.GetGroupByAsync(It.IsNotNull<Expression<Func<GroupEntity, bool>>>(), It.IsNotNull<string>()))
            .Returns(Task.FromResult(new GroupDto() { Id = 0 }));

        // Act:
        // Get Response from Get method from SchoolRegister.WebAPI/Controllers/GroupController.
        var response = await _client.GetAsync("api/Group/Get?subjectId=" + id);

        // Assert:
        // Check if Response is: 404 NotFound. 
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task PostGroup_WithValidModel_ReturnsCreated()
    {
        // Arrange:
        // Create Fake Group witch proper data.
        var createGroupDto = new CreateGroupDto()
        {
            Name = "Fake Group"          
        };

        // Serialize createGroupDto object to .json.
        var httpContent = createGroupDto.ToJsonHttpContent();

        // Mock InsertGroupAsync method from GroupService, so it generates a GroupDto with our Fake Group values.
        var subjectDto = _groupServiceMock.Setup(e => e.InsertGroupAsync(It.IsAny<CreateGroupDto>())).Returns(Task.FromResult(new GroupDto()
        {
            Id = 1,
            Name = createGroupDto.Name
        }));

        // Act:
        // Get Group from Post method from SchoolRegister.WebAPI/Controllers/GroupController.
        var response = await _client.PostAsync("api/Group/Post", httpContent);

        // Assert:
        // Check if Response is: 201 Created. 
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
    }

    [Fact]
    public async Task PostSubject_WithInvalidModel_ReturnsBadRequest()
    {
        // Arrange:
        // Create Fake Group witch wrong data.
        var createGroupDto = new CreateGroupDto()
        {
            Name = null
        };

        // Serialize createGroupDto object to .json.
        var httpContent = createGroupDto.ToJsonHttpContent();

        // Act:
        // Get Response from Post method from SchoolRegister.WebAPI/Controllers/GroupController.
        var response = await _client.PostAsync("api/Group/Post", httpContent);

        // Assert:
        // Check if Response is: 400 BadRequest. 
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
    }

    [Theory]
    [InlineData(1)]
    public async Task PutSubject_WithValidModel_ReturnsOk(int id)
    {
        // Arrange:
        // Create Fake Group witch proper data.
        var updateGroupDto = new UpdateGroupDto()
        {
            Id = id,
            Name = "Fake Group"
        };

        // Serialize updateGroupDto object to .json.
        var httpContent = updateGroupDto.ToJsonHttpContent();

        // Mock UpdateGroupDto method from GroupService, so it generates a GroupDto with our Fake Group values.
        var subjectDto = _groupServiceMock.Setup(e => e.UpdateGroupAsync(It.IsAny<UpdateGroupDto>())).Returns(Task.FromResult(new GroupDto()
        {
            Id = id,
            Name = updateGroupDto.Name
        }));

        // Act:
        // Get Response from Post method from SchoolRegister.WebAPI/Controllers/GroupController.
        var response = await _client.PutAsync("api/Group/Put?groupId=" + id, httpContent);

        // Assert:
        // Check if Response is: 200 OK. 
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
    }

    [Theory]
    [InlineData(1)]
    public async Task PutSubject_WithInvalidRequest_ReturnsBadRequest(int id)
    {
        // Arrange:
        // Create Fake Group witch proper data.
        var updateGroupDto = new UpdateGroupDto()
        {
            Id = id,
            Name = "Fake Group"
        };

        // Serialize updateGroupDto object to .json.
        var httpContent = updateGroupDto.ToJsonHttpContent();

        // Act:
        // Get Response from Put method from SchoolRegister.WebAPI/Controllers/GroupController.
        var response = await _client.PutAsync("api/Group/Put?subjectId=" + id + 1, httpContent);

        // Assert:
        // Check if Response is: 400 BadRequest. 
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
    }


    [Theory]
    [InlineData(1)]
    public async Task Delete_WithValidId_ReturnsNoContent(int id)
    {
        // Arrange:
        // Mock GetGroupByAsync method from GroupService, so it generates a GroupDto with our Fake Group values.
        var groupDto = _groupServiceMock
            .Setup(e => e.GetGroupByAsync(It.IsNotNull<Expression<Func<GroupEntity, bool>>>(), null))
            .Returns(Task.FromResult(new GroupDto() { Id = 1 }));

        // Act:
        // Get Response from Delete method from SchoolRegister.WebAPI/Controllers/GroupController.
        var response = await _client.DeleteAsync("api/Group/Delete?subjectId=" + id);

        // Assert:
        // Check if Response is: 204 NoContent. 
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);
    }

    [Theory]
    [InlineData(-1)]
    public async Task Delete_WithInvalidId_ReturnsNotFound(int id)
    {
        // Arrange:
        // Mock GetGroupByAsync method from GroupService, so it generates a GroupDto with our Fake Group values.
        var groupDto = _groupServiceMock
            .Setup(e => e.GetGroupByAsync(It.IsNotNull<Expression<Func<GroupEntity, bool>>>(), null))
            .Returns(Task.FromResult<GroupDto>(null));

        // Act:
        // Get Response from Delete method from SchoolRegister.WebAPI/Controllers/GroupController.
        var response = await _client.GetAsync("api/Group/Get?subjectId=" + id);

        // Assert:
        // Check if Response is: 404 NotFound. 
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
    }
}
