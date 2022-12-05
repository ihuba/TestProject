using System.Net.Http.Headers;

namespace ApiCore;

public abstract class BaseApiClient
{
    public HttpClient HttpClient;
    public string Token { get; private set; }
    
    protected BaseApiClient(Uri baseUrl)
    {
        HttpClient httpClient = new System.Net.Http.HttpClient();
        httpClient.BaseAddress = baseUrl;
        httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        this.HttpClient = httpClient;
    }
    
    public virtual void ApplyAuthorization(string token) => this.ApplyAuthorization("Bearer", token);

    public virtual void ApplyAuthorization(string scheme, string token)
    {
        this.Token = token;
        this.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme, token);
    }
}