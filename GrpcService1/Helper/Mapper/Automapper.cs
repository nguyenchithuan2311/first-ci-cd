using AutoMapper;

namespace GrpcService1.Helper.Mapper;

public class Automapper : Profile
{
    public Automapper()
    {
        CreateMap<Model.Customer, CustomerReply>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString())) // Guid to string
            .ReverseMap()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.Parse(src.Id))); // string to Guid

        CreateMap<CustomerRequest, Model.Customer>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.Parse(src.Id))); // string to Guid

        CreateMap<Model.Customer, CustomerRequest>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }
}