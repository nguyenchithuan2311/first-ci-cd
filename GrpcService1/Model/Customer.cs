namespace GrpcService1.Model;

public class Customer()
{
    public Customer(string name, string order) : this()
    {
        Id = Guid.NewGuid();
        Name = name;
        Order = order;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Order { get; set; }
}