using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using Testes.src.app.services;

namespace Testes.test.integration.helper
{
  public abstract class TestHelper
  {
    public string ConvertObjectToJson(object? obj)
    {
      return JsonConvert.SerializeObject(obj);
    }

    public dynamic ConvertJsonToObject(string obj)
    {
      return JsonConvert.DeserializeObject(obj)!;
    }

    public HttpContent GetDefaultHttpContent(string resquestBody, string mediaType = "application/json")
    {
      return new StringContent(resquestBody, Encoding.UTF8, mediaType);
    }

    public void setAccessTokenInHttpClient(HttpClient httpClient, string accessToken)
    {
      httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
    }

    public string GetJwtToken(int userId)
    {
        var configurationMock = new ConfigurationMock();
        configurationMock.calls.Add("JwtSettings:SecretKey", 0);
        configurationMock.data.Add("JwtSettings:SecretKey", "Settings.Secretsdfasdfasdfasfasdfasdfasdfasdfasdf");
        var accessToken = new JwtService(new JwtSecurityTokenHandler(), configurationMock).GenerateToken(userId);
        return accessToken;
    }
  }
}