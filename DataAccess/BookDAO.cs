using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Models;

namespace DataAccess
{
    public class BookDAO
    {
        private static BookPublisherContext context = new BookPublisherContext();
        public static IEnumerable<Book> GetAll()
        {
            try
            {
                var list = from book in context.Books select book;
                return list.ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void Delete(Book book)
        {
            try
            {
                Book b = context.Books.SingleOrDefault(x => x.BookId == book.BookId);
                if(b == null)
                {
                    throw new Exception("Not found");
                } else
                {
                    context.Books.Remove(book);
                    context.SaveChanges();
                }
               
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        } 
        public static void Add(Book book)
        {
            try
            {
                Book b = context.Books.SingleOrDefault(x => x.BookId == book.BookId);
                if(b != null)
                {
                    throw new Exception("The book Id is already in use");
                } else
                {
                    context.Books.Add(book);
                    context.SaveChanges();
                }
               
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void Update(Book book)
        {
            try
            {
                Book b = context.Books.SingleOrDefault(x => x.BookId == book.BookId);
                if(b == null)
                {
                    throw new Exception("Not found");
                } else
                {
                    context.Entry(b).CurrentValues.SetValues(book);
                    context.SaveChanges();
                }
               
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
