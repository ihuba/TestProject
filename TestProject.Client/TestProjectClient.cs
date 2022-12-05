using ApiCore;
using TestProject.Client.Actions;

namespace TestProject.Client
{
    public class TestProjectClient: BaseApiClient
    {
        public TestProjectClient() : base(new Uri("https://httpbin.org/")) { }

        public OrderActions OrderActions => new(HttpClient);
    }
}

