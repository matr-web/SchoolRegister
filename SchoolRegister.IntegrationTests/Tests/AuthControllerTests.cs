using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Moq;
using SchoolRegister.BusinessAccess.Interfaces;
using SchoolRegister.DataAccess;
using SchoolRegister.Models.Dto_s.UserDto_s;

namespace SchoolRegister.IntegrationTests.Tests;

#nullable disable
public class AuthControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private WebApplicationFactory<Program> _factory;
    private HttpClient _client;
    private Mock<IUserService> _userServiceMock = new Mock<IUserService>();

    public AuthControllerTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory
            .WithWebHostBuilder(builder =>

            builder.ConfigureServices(services =>
            {
                var dbContextOptions = services
                    .SingleOrDefault(service => service.ServiceType == typeof(DbContextOptions<SchoolRegisterContext>));

                // Register IUserService Mock.
                services.AddSingleton<IUserService>(_userServiceMock.Object);

                // Remove current Database Registration.
                services.Remove(dbContextOptions);

                // Implement InMemoryDb.
                services.AddDbContext<SchoolRegisterContext>(options => options.UseInMemoryDatabase("SchoolRegisterDb"));
            }));

        _client = _factory.CreateClient();
    }

    [Fact]
    public async Task RegisterUser_ForValidModel_ReturnsOk()
    {
        // Arrange:
        // Create Fake User witch proper data.
        var registerUser = new RegisterUserDto() 
        {
            FirstName = "FakeName_1",
            LastName = "FakeName_1",
            Email = "fakemail1@test.com",
            Password = "password123",
            ConfirmPassword = "password123",
            RoleId = 1, // Administrator Role in this case.
            //GroupId = 1,
            //Title = "mgr inż."
        };

        // Serialize RegisterUserDto object to .json.
        var httpContent = registerUser.ToJsonHttpContent();

        // Act:
        // Get Response from Register method from SchoolRegister.WebAPI/Controllers/AuthController.
        var response = await _client.PostAsync("api/Auth/Register", httpContent);

        // Assert:
        // Check if Response is: 200 OK. 
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
    }

    [Fact]
    public async Task RegisterUser_ForDifferentPasswords_ReturnsBadRequest()
    {
        // Arrange:
        // Create Fake User, which gave different passwords during registration.
        var registerUser = new RegisterUserDto()
        {
            FirstName = "FakeName_1",
            LastName = "FakeName_1",
            Email = "fakemail1@test.com",
            Password = "password123",
            ConfirmPassword = "password123_bad",
            RoleId = 1,
        };

        // Serialize RegisterUserDto object to .json.
        var httpContent = registerUser.ToJsonHttpContent();

        // Act:
        // Get Response from Register method from SchoolRegister.WebAPI/Controllers/AuthController.
        var response = await _client.PostAsync("api/Auth/Register", httpContent);

        // Assert:
        // Check if Response is: 400 BadRequest. 
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Login_ForRegisteredUser_ReturnsOk()
    {
        // Arrange:
        // Give proper User data to check the login action.
        var loginDto = new LoginUserDto()
        {
            Email = "fakemail1@test.com",
            Password = "password123"
        };

        // Serialize LoginUserDto object to .json.
        var httpContent = loginDto.ToJsonHttpContent();

        // Mock GenerateToken method from UserService, so it generates an fake JWT Token.
        var token = _userServiceMock.Setup(e => e.GenerateToken(It.IsAny<LoginUserDto>())).Returns(Task.FromResult("jwt"));

        // Act:
        // Get Response from Login method from SchoolRegister.WebAPI/Controllers/AuthController.
        var response = await _client.PostAsync("api/Auth/Login", httpContent);

        // Assert:
        // Check if Response is: 200 OK. 
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
    }
}
