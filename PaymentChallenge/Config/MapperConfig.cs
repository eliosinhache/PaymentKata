using AutoMapper;
using PaymentChallenge.Data;
using PaymentChallenge.Model.Request;
using PaymentChallenge.Model.Response;

namespace PaymentChallenge.Config
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Payment, NewPayment>().ReverseMap();
            CreateMap<Payment, ConfirmPayment>().ReverseMap();
            CreateMap<Payment, PaymentResponse>().ReverseMap();
        }
    }
}
