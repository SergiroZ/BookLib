using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookLib
{
    public partial class Form1 : Form
    {
        public int bookSelRowNum = 0,
                   authorSelRowNum = 0,
                 publisherSelRowNum = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "libraryDataSet.BookRelavity". При необходимости она может быть перемещена или удалена.
            this.bookRelavityTableAdapter.Fill(this.libraryDataSet.BookRelavity);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "libraryDataSet.Book". При необходимости она может быть перемещена или удалена.
            this.bookTableAdapter.Fill(this.libraryDataSet.Book);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "libraryDataSet.Author". При необходимости она может быть перемещена или удалена.
            this.authorTableAdapter.Fill(this.libraryDataSet.Author);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "libraryDataSet.Publisher". При необходимости она может быть перемещена или удалена.
            this.publisherTableAdapter.Fill(this.libraryDataSet.Publisher);
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {
        }

        private void authorBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.Validate();
                this.authorBindingSource.EndEdit();

                this.tableAdapterManager.UpdateAll(this.libraryDataSet);
            }
            catch (System.Data.SqlClient.SqlException)
            {
                MessageBox.Show("\tУдаление связанных записей...\n");
                MessageBox.Show(authorSelRowNum.ToString());

                try
                {
                    var query = libraryDataSet.Book.AsEnumerable().
                                    Where(r => r.Field<int>("IdAuthor") == authorSelRowNum);
                    foreach (var row in query.ToList())
                    {
                        libraryDataSet.Book.Rows.Find((int)row.Field<int>("Id")).Delete();
                    }

                    //bookTableAdapter.Fill(libraryDataSet.Book);
                    //bookTableAdapter.Update(libraryDataSet.Book);
                    //bookRelavityTableAdapter.Fill(libraryDataSet.BookRelavity);

                    this.tableAdapterManager.UpdateAll(this.libraryDataSet);
                }
                catch (System.Data.SqlClient.SqlException)
                {
                    //throw;
                }
            }
        }

        private void publisherBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.publisherBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.libraryDataSet);
        }

        private void publisherBindingNavigator_RefreshItems(object sender, EventArgs e)
        {
        }

        private void authorBindingNavigator_RefreshItems(object sender, EventArgs e)
        {
        }

        private void tabControl1_MouseDown(object sender, MouseEventArgs e)
        {
            int caseSwitch = tabControl1.SelectedIndex;
            switch (caseSwitch)
            {
                case 0:
                    bookBindingNavigator.Visible = true;
                    authorBindingNavigator.Visible = false;
                    publisherBindingNavigator.Visible = false;
                    break;

                case 1:
                    bookBindingNavigator.Visible = false;
                    authorBindingNavigator.Visible = true;
                    publisherBindingNavigator.Visible = false;
                    break;

                case 2:
                    bookBindingNavigator.Visible = false;
                    authorBindingNavigator.Visible = false;
                    publisherBindingNavigator.Visible = true;
                    break;

                default:
                    break;
            }
        }

        private void bindingNavigatorAddNewItem2_Click(object sender, EventArgs e)
        {
            NewBook newBook = new NewBook
            {
                Owner = this
            };

            newBook.ShowDialog();

            this.bookRelavityTableAdapter.Fill(this.libraryDataSet.BookRelavity);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            bookSelRowNum = dataGridView1.CurrentCell.RowIndex;
        }

        private void authorDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            authorSelRowNum = (int)authorDataGridView.
                Rows[authorDataGridView.SelectedCells[0].RowIndex].Cells[0].Value;
        }
    }
}