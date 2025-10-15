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
    public class GenericRepository<T>:IGenericRepository<T> where T:BaseEntity
    {

        private readonly LibraryDbContext _context;
        public GenericRepository(LibraryDbContext libraryDbContext)
        {
            _context = libraryDbContext;
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if (typeof(T) == typeof(Book))
            {
                return (IEnumerable<T>) await _context.Books.Include(B=>B.Author).Include(B=>B.Category).ToListAsync();
            }
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T>? GetByIdAsync(int id)
        {
            if (typeof(T) == typeof(Author))
            {
                return await _context.Authors.Include(a => a.Books).FirstOrDefaultAsync(a => a.Id == id) as T;
            }
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task AddAsync(T model)
        {
           await _context.Set<T>().AddAsync(model);
            
        }
        public void Update(T model)
        {
            _context.Set<T>().Update(model);
        }

        public void Delete(T model)
        {
            _context.Set<T>().Remove(model);
        }

       
    }
}
