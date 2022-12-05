using ApiCore.Models;
using NUnit.Framework;

namespace ApiCore;

public static class ApiExtensions
{
    public static StatusCodeResponse<TResponse?> PostRequestResults<TRequest, TResponse>(
        this HttpClient client,
        Uri url,
        TRequest model)
    {
        var result = client.PostAsJsonAsync<TRequest>(url, model).GetAwaiter().GetResult();
        var response = result.DeserializeTo<TResponse>();
        return new StatusCodeResponse<TResponse?>()
        {
            Response = result,
            StatusCode = result.StatusCode,
            Data = response
        };
    }
    
    private static T? DeserializeTo<T>(this HttpResponseMessage message)
    {
        var obj = default (T);
        try
        {
            obj = message.Content.ReadAsAsync<T>().GetAwaiter().GetResult();
        }
        catch
        {
            TestContext.WriteLine("Response content cannot be deserialized to T model. Incorrect model is applied or there is no content in response");
        }
        return obj;
    }
    
}



