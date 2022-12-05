using System.Net;
using ApiCore;
using ApiCore.Models;
using FluentAssertions;
using TestProject.Client.Endpoints.Order;
using TestProject.Client.Models.Order;

namespace TestProject.Client.Actions;

public class OrderActions: BaseActions
{
    private readonly HttpClient _client;

    public OrderActions(HttpClient client)
    {
        this._client = client;
    }

    public ResponseCalculateOrderTotalModel? PostRequestOrderCalculation(RequestCalculateOrderTotalModel postRequestCalculateOrderTotalModel)
    {
        StatusCodeResponse<ResponseCalculateOrderTotalModel?> statusCodeResponse = this._client.PostRequestResults<RequestCalculateOrderTotalModel, ResponseCalculateOrderTotalModel>(OrderEndpoints.PostCalculateOrderTotal, postRequestCalculateOrderTotalModel);
        statusCodeResponse.StatusCode.Should().Be(HttpStatusCode.OK); 
        return statusCodeResponse.Data;
    }
}