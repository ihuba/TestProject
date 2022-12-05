using TestProject.Client.Builders.Order;

namespace TestProject.Client.Builders;

public static class Builders
{
    public static OrderBuilders OrderBuilders { get; } = new ();
}