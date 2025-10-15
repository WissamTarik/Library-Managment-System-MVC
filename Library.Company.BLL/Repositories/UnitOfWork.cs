using Library.Company.BLL.Interfaces;
using Library.Company.DAL.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Company.BLL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LibraryDbContext _context;

        public IAuthorRepository AuthorRepository { get; }
        public ICategoryRepository CategoryRepository { get ; }
        public IBookRepository BookRepository { get ; }

        public UnitOfWork(LibraryDbContext context)
        {
            _context = context;
            AuthorRepository = new AuthorRepository(_context);
            CategoryRepository = new CategoryRepository(_context);
            BookRepository = new BookRepository(_context);
        }
        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

     

        public async ValueTask DisposeAsync()
        {
           await _context.DisposeAsync();
        }
    }
}
