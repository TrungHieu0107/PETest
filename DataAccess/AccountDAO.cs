using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Models;

namespace DataAccess
{
    public class AccountDAO
    {
        private static AccountDAO instance;
        private static readonly object InstanceLock = new object();
        

        private AccountDAO(){}

        public static AccountDAO Instance
        {
            get
            {
                lock (InstanceLock)
                {
                    if(instance == null)
                    {
                        instance = new AccountDAO();
                    }
                    return instance;
                }
            }
        }
        public AccountUser CheckLogin(string username, string password)
        {
            try
            {
                using (var db = new BookPublisherContext())
                {
                    AccountUser user = db.AccountUsers.SingleOrDefault(x => x.UserPassword == password && x.UserId == username);

                    return user;

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
