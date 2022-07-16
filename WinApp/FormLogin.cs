using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessObject.Models;
using DataAccess.Repository;

namespace WinApp
{
    public partial class FormLogin : Form
    {
        IAccountRepository _accountRepository;
        public FormLogin()
        {
            _accountRepository = new AccountRepository();
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string username = txtUsername.Text.Trim();
                string password = txtPassword.Text.Trim();  

                if(username.Length > 0 && password.Length > 0)
                {
                   AccountUser user =  _accountRepository.CheckLogin(username, password);
                    if(user != null)
                    {
                        FormBook frm  = new FormBook()
                        {
                            AccountLogin = user,
                        };
                        frm.ShowDialog();
                    } else
                    {
                        MessageBox.Show("You are not allowed to access this function.");
                    }
                }
            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Login");
            }
        }
    }
}
