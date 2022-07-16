using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Models;

namespace DataAccess
{
    public class PublisherDAO
    {
        private static BookPublisherContext context = new BookPublisherContext();


        public static List<Publisher> GetAll()
        {
            try
            {
                return context.Publishers.ToList();
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        
    }
}
