using Library.Company.BLL.Interfaces;
using Library.Company.DAL.Data.Contexts;
using Library.Company.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Company.BLL.Repositories
{
    public class BookRepository : GenericRepository<Book>,IBookRepository
    {
        private readonly LibraryDbContext _context;
        public BookRepository(LibraryDbContext libraryDbContext):base(libraryDbContext)
        {
            _context = libraryDbContext;
        }

        public async Task<IEnumerable<Book>>? GetByNameAsync(string name)
        {
            return await _context.Books.Where(b => b.Title.ToLower().Contains(name.ToLower())).ToListAsync();
        }

    }
}
