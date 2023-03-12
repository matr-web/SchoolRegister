using Newtonsoft.Json;
using System.Text;

namespace SchoolRegister.IntegrationTests.Helpers;

public static class HttpContentHelper
{
    /// <summary>
    /// A extension method for object type, that is responsible for serializing values from object type into .json for StringContent type.
    /// </summary>
    public static HttpContent ToJsonHttpContent(this object obj)
    {
        var json = JsonConvert.SerializeObject(obj);

        var httpContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

        return httpContent;
    }
}
