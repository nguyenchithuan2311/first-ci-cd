using AutoMapper;
using Grpc.Core;
using GrpcService1.Infrastructure.Persistence;
using UOW.UoW;

namespace GrpcService1.Services;

public class CustomerService(IMapper mapper, IUnitOfWork unitOfWork) : Customer.CustomerBase
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public override async Task<CustomerReply> GetCustomer(CustomerRequest request, ServerCallContext context)
    {
        var customer = await _unitOfWork.GetRepository<Model.Customer>().GetByIdAsync(Guid.Parse(request.Id));
        return _mapper.Map<CustomerReply>(customer);
    }
    public override async Task<CustomerReply> CreateCustomer(CustomerRequest request, ServerCallContext context)
    { 
        var customer = new Model.Customer(request.Name, request.Order);
        await _unitOfWork.GetRepository<Model.Customer>().AddAsync(customer, context.CancellationToken);
        return _mapper.Map<CustomerReply>(customer);
    }
}