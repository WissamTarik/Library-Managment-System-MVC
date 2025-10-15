using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Company.BLL.Interfaces
{
    public interface IUnitOfWork:IAsyncDisposable
    {
        IAuthorRepository AuthorRepository { get;  }
        ICategoryRepository CategoryRepository { get;  }
       IBookRepository BookRepository { get; }

        Task<int> CompleteAsync();
    }
}
