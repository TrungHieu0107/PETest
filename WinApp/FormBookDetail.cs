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
    public partial class FormBookDetail : Form
    {

        IPublisherRepository _publisherRepository;
        public bool IsInsert { get; set; }

        public Book BookInfo { get; set; }  

        public IBookRepository BookRepository { get; set; }

        public FormBookDetail()
        {
            _publisherRepository = new PublisherRepository();
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                (bool flag, string error, Book book) validate = ValidateAll();
                if (validate.flag)
                {
                    if (IsInsert)
                    {
                        BookRepository.Add(validate.book);
                    } else
                    {
                        BookRepository.Update(validate.book);
                    }
                } else
                {
                    MessageBox.Show(validate.error, "Input error");
                    DialogResult = DialogResult.None;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Add new");
                DialogResult = DialogResult.None;
            }
        }

        private void FormBookDetail_Load(object sender, EventArgs e)
        {
            cboPublisher.Items.Clear();
            cboPublisher.DataSource = _publisherRepository.GetAll();
            cboPublisher.DisplayMember = "PublisherName";
            cboPublisher.ValueMember = "PublisherId";

            if (IsInsert)
            {
                btnAdd.Text = "Add";
            }
            else
            {
                btnAdd.Text = "Update";
                txtBookId.Enabled = false;
                cboPublisher.SelectedValue = BookInfo.PublisherId;
                txtBookId.Text = BookInfo.BookId;
                txtBookName.Text = BookInfo.BookName;   
                numQuantity.Value = (decimal)BookInfo.Quantity;
                txtAuthName.Text = BookInfo.AuthorName;
            }
        }

        private (bool flag, string error, Book book) ValidateAll()
        {
            var flag = true;
            var error = "";
            Book book = new Book();
            if (txtBookId.Text.Length > 0)
            {
                if (txtBookId.Text.Trim().Length < 6 || txtBookId.Text.Trim().Length > 12)
                {
                    flag = false;
                    error += "Id must be from 6 to 12 characters \n";
                }
                else if (txtBookId.Text.Contains(' '))
                {
                    flag = false;
                    error += "Id cannot contains white space \n";
                }
                else
                {
                    book.BookId = txtBookId.Text;
                }
            }
            else
            {
                flag = false;
                error += "Book Id is required \n";
            }

            //name 
            if (txtBookName.Text.Length > 0)
            {
                if (txtBookName.Text.Trim().Length > 0)
                {
                    book.BookName = txtBookName.Text;
                }
                else
                {
                    flag = false;
                    error += "Book name is required \n";
                    txtBookName.Text = String.Empty;
                }

            }
            else
            {
                flag = false;
                error += "Book name is required \n";
            }

            //Author 
            if (txtAuthName.Text.Length > 0)
            {
                if (txtBookName.Text.Trim().Length >= 10)
                {
                    book.AuthorName = txtAuthName.Text;
                }
                else
                {
                    flag = false;
                    error += "Author name must greater than 10 characters \n";
                    txtBookName.Text = String.Empty;
                }

            }
            else
            {
                flag = false;
                error += "Author name is required \n";
            }
            //Quatity 
            try
            {
                int quantity = int.Parse(numQuantity.Text.Trim());
                if(quantity > 0)
                {
                    book.Quantity = quantity;
                } else
                {
                    flag = false;
                    error += "Quantity must greater than 0 \n";
                }
            }
            catch (Exception ex)
            {
                flag = false;
                error += "Quantity is required \n";
            }

            if(cboPublisher.SelectedValue == null)
            {
                flag = false;
                error += "Publisher is required \n";
            } else
            {
                book.PublisherId = (string)cboPublisher.SelectedValue;
            }

            return (flag, error, book );
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure to cancel?", "Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(result == DialogResult.Yes)
            {
                Close();
            }
        }
    }
}
