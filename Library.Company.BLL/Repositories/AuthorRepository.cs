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
    public class AuthorRepository : GenericRepository<Author>,IAuthorRepository
    {
        private readonly LibraryDbContext _context;
        public AuthorRepository(LibraryDbContext libraryDbContext):base(libraryDbContext)
        {
            _context = libraryDbContext;
            
        }

        public async Task<IEnumerable<Author>>? GetByNameAsync(string name)
        {
            return await _context.Authors.Where(c => c.Name.ToLower().Contains(name.ToLower())).ToListAsync();
        }
    }
}
