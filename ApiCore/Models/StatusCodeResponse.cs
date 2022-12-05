using System.Net;

namespace ApiCore.Models;

public class StatusCodeResponse<TResponse>
{
  public HttpResponseMessage Response { get; set; }

  public TResponse Data { get; set; }

  public HttpStatusCode StatusCode { get; set; }

  public void Deconstruct(out HttpResponseMessage response, out TResponse data)
  {
    response = this.Response;
    data = this.Data;
  }
}