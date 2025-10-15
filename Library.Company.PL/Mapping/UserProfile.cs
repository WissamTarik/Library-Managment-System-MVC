using AutoMapper;
using Library.Company.DAL.Models;
using Library.Company.PL.Dtos;

namespace Library.Company.PL.Mapping
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<SignUpDto,AppUser>().ReverseMap();
            CreateMap<SignInDto,AppUser>().ReverseMap();
        }
    }
}
