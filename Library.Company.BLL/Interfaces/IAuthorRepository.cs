using Library.Company.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Company.BLL.Interfaces
{
    public interface IAuthorRepository:IGenericRepository<Author>
    {
         Task<IEnumerable<Author>>? GetByNameAsync(string name);
    }
}
