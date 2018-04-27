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
    public partial class NewBook : Form
    {
        public NewBook()
        {
            InitializeComponent();
        }

        private void bookBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.bookBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.libraryDataSet);
        }

        private void NewBook_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "libraryDataSet.Publisher". При необходимости она может быть перемещена или удалена.
            this.publisherTableAdapter.Fill(this.libraryDataSet.Publisher);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "libraryDataSet.Author1". При необходимости она может быть перемещена или удалена.
            this.author1TableAdapter.Fill(this.libraryDataSet.Author1);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "libraryDataSet.Book". При необходимости она может быть перемещена или удалена.
            this.bookTableAdapter.Fill(this.libraryDataSet.Book);
            if (Owner is Form1 main)
            {
                bookBindingSource.Position = main.bookSelRowNum;
            }
        }
    }
}