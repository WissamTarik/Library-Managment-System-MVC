using AutoMapper;
using Library.Company.DAL.Models;
using Library.Company.PL.Dtos;

namespace Library.Company.PL.Mapping
{
    public class AuthorProfile:Profile
    {
        public AuthorProfile()
        {

            CreateMap<Author, AuthorDto>().ReverseMap();
        }
    }
}
