using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class BookRepository : IBookRepository
    {
        public void Add(Book book) => BookDAO.Add(book);

        public void Update(Book book) => BookDAO.Update(book);
        void IBookRepository.Delete(Book book) => BookDAO.Delete(book);

        IEnumerable<Book> IBookRepository.GetAll() => BookDAO.GetAll();
    }
}
