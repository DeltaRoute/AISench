using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AISench
{
    public struct book
    {
        public int Articul;
        public string FIO;
        public string AuthorName;
        public int Year;
        public int Price;
        public string Genre;
        public bool Exist;
    }
    public partial class InsertForm : Form
    {
        public string Articul, FIO, AuthorName, Year, Price, Genre;
        public int articul, year, price;
        public bool confirm = true;
        book result;
        public InsertForm()
        {
            InitializeComponent();
            button1.Text = "Добавить";
        }
        public InsertForm(DataGridViewRow row)
        {
            InitializeComponent();
            textBox1.Text = row.Cells[1].Value.ToString();
            textBox2.Text = row.Cells[2].Value.ToString();
            textBox3.Text = row.Cells[3].Value.ToString();
            textBox4.Text = row.Cells[4].Value.ToString();
            textBox5.Text = row.Cells[5].Value.ToString();
            textBox6.Text = row.Cells[6].Value.ToString();
            button1.Text = "Изменить";
        }
        public book show()
        {
            result.Articul = articul;
            result.FIO = FIO;
            result.AuthorName = AuthorName;
            result.Year = year;
            result.Price = price;
            result.Genre = Genre;
            result.Exist = confirm;
            return result;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Articul = textBox1.Text;
            FIO = textBox2.Text;
            AuthorName = textBox3.Text;
            Year = textBox4.Text;
            Price = textBox5.Text;
            Genre = textBox6.Text;
            //articul,year, price;
            string status = "";
           
            try
            {
                articul = Convert.ToInt32(Articul);
            }
            catch
            {
                status += "Некорректные данные в поле 'Артикул'.";
                confirm = false;
            }
            if (FIO == "")
            {
                confirm = false;
                status += "Некорректные данные в поле 'Фамилия И.О.'.";
            }
            if(AuthorName == "")
            {
                confirm = false;
                status += "Некорректные данные в поле 'Название'.";
            }
            try
            {
                year = Convert.ToInt32(Year);
            }
            catch
            {
                status += "Некорректные данные в поле 'Год издания'.";
                confirm = false;
            }
            try
            {
                price = Convert.ToInt32(Price);
            }
            catch
            {
                status += "Некорректные данные в поле 'Цена'.";
                confirm = false;
            }
            if (Genre == "")
            {
                confirm = false;
                status += "Некорректные данные в поле 'Жанр'.";
            }
                lb_status.Text = status;
            if (confirm)
            {
                this.Dispose();
            }
        }
    }
}
