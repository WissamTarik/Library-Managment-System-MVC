using Library.Company.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Company.BLL.Interfaces
{
    public interface ICategoryRepository:IGenericRepository<Category>
    {
        Task<IEnumerable<Category>>? GetByNameAsync(string name);
    }
}
