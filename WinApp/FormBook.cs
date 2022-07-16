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
    public partial class FormBook : Form
    {
        IBookRepository bookRepository;
        BindingSource source;
        public AccountUser AccountLogin { get; set; }

        private static Book book;
        public FormBook()
        {
            bookRepository = new BookRepository();
            InitializeComponent();
        }



        private void FormBook_Load(object sender, EventArgs e)
        {
            try
            {
                IEnumerable<Book> books = bookRepository.GetAll();
                LoadData(books.ToList());

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Load data");
            }
        }

        private void LoadData(List<Book> list)
        {
            try
            {
                source = new BindingSource();
                source.DataSource = list;
                dgvBookList.DataSource = source;
                if (list.Count() > 0)
                {
                    btnDelete.Enabled = true;
                    btnUpdate.Enabled = true;
                    book = (Book)dgvBookList.Rows[0].DataBoundItem;
                }
                else
                {
                    btnDelete.Enabled = false;
                    btnUpdate.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvBookList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                book = (Book)dgvBookList.Rows[e.RowIndex].DataBoundItem;
            }
            catch (Exception ex)
            {

            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            bookRepository.Delete(book);
            LoadData(bookRepository.GetAll().ToList());
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FormBookDetail formBookDetail = new FormBookDetail()
            {
                BookRepository = bookRepository,
                IsInsert = true,    
            };
            if(formBookDetail.ShowDialog() == DialogResult.OK)
            {
                LoadData(bookRepository.GetAll().ToList());
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            FormBookDetail formBookDetail = new FormBookDetail()
            {
                BookRepository = bookRepository,
                IsInsert = false,
                BookInfo = book,
            };
            if (formBookDetail.ShowDialog() == DialogResult.OK)
            {
                LoadData(bookRepository.GetAll().ToList());
            }
        }
    }
}
