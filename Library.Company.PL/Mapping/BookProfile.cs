using AutoMapper;
using Library.Company.DAL.Models;
using Library.Company.PL.Dtos;

namespace Library.Company.PL.Mapping
{
    public class BookProfile:Profile
    {
        public BookProfile()
        {

            CreateMap<Book, BookDto>().ReverseMap();
        }
    }
}
