using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Models;

namespace DataAccess.Repository
{
    public interface IBookRepository
    {
        IEnumerable<Book> GetAll();

        void Delete(Book book);
        void Add(Book book);
        void Update(Book book);
    }
}
