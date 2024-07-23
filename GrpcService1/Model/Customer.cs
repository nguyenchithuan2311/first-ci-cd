namespace GrpcService1.Model;

public class Customer()
{
    public Customer(string name, string order) : this()
    {
        Id = Guid.NewGuid();
        Name = name;
        Order = order;
    }

    public Guid Id { get; init; }
    public string Name { get; init; }
    public string Order { get; init; }
}