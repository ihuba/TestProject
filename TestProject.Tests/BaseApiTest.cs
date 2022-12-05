using TestProject.Client;

namespace TestProject.Tests;

public abstract class BaseApiTest
{
    protected TestProjectClient TestProjectClient => new ();
}