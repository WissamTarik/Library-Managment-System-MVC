using AutoMapper;
using Library.Company.DAL.Models;
using Library.Company.PL.Dtos;

namespace Library.Company.PL.Mapping
{
    public class UserToReturnProfile:Profile
    {
        public UserToReturnProfile()
        {
            CreateMap<UserToReturnDto, AppUser>().ReverseMap();
        }
    }
}
