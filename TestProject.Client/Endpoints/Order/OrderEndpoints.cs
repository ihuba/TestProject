namespace TestProject.Client.Endpoints.Order;

public static class OrderEndpoints
{
    public static Uri PostCalculateOrderTotal => new ("/post", UriKind.Relative);
}