using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AISench
{
    public partial class Form1 : Form
    {
        OleDbConnection connection;
        OleDbCommand command;
        OleDbDataAdapter reader;
        DataTable table;
        int gbVis = 0;
        bool signed;
        public Form1()
        {
            
            InitializeComponent();
            groupBox1.Visible = false;
            dataGridView1.Size = new Size(
                this.ClientSize.Width - dataGridView1.Left * 2 - groupBox1.Width * gbVis,
                this.ClientSize.Height - dataGridView1.Top * 2);
            groupBox1.Left = dataGridView1.Width + dataGridView1.Left + 5;
        }

        private void sELECTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            command = new OleDbCommand("SELECT * FROM 'Книги'", connection);
            reader = new OleDbDataAdapter("SELECT * FROM Книги", connection);
            table = new DataTable();
            reader.Fill(table);
            dataGridView1.DataSource = table;
        }

        private void подключитьсяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (AutentificationF signIn = new AutentificationF())
            {
                signIn.ShowDialog();
                signed = signIn.Signed();
            }
            if (!signed) return;
            try
            {
                //MessageBox.Show("Connectiong...");
                connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + @"D:\АИС.accdb;" + "Persist Security Info=false;");
                connection.Open();
                //MessageBox.Show("Complete");
                toolStripStatusLabel1.Text = "Connected.";
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void отсоединитьсяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            connection.Close();
            toolStripStatusLabel1.Text = "Connection closed.";
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            dataGridView1.Size = new Size(
                this.ClientSize.Width - dataGridView1.Left * 2 - groupBox1.Width*gbVis,
                this.ClientSize.Height - dataGridView1.Top * 2);
            groupBox1.Left = dataGridView1.Width + dataGridView1.Left+5;
        }

        private void показатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetVisible(true);
            this.Refresh();
        }

        private void скрытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetVisible(false);
            this.Refresh();
        }
        void SetVisible(bool visible)
        {
            скрытьToolStripMenuItem.Enabled = visible;
            показатьToolStripMenuItem.Enabled = !visible;
            groupBox1.Visible = visible;
            gbVis = Convert.ToInt32(visible);
            dataGridView1.Size = new Size(
                this.ClientSize.Width - dataGridView1.Left * 2 - groupBox1.Width * gbVis,
                this.ClientSize.Height - dataGridView1.Top * 2);
            groupBox1.Left = dataGridView1.Width + dataGridView1.Left + 5;
        }

        private void добавитьНовыйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            book result;
            using (InsertForm insert = new InsertForm())
            {
                insert.ShowDialog();
                result = insert.show();
            }
            if (result.Exist)
            {
                if (command!= null)
                    command.Dispose();
                command = new OleDbCommand("INSERT INTO [Книги]" +
                    "([Артикул],[ФИО],[Название],[Год издания],[Цена],[Жанр])" +
                    $"VALUES ({result.Articul},{result.FIO},{result.AuthorName},{result.Year},{result.Price},{result.Genre});", connection);
                command.ExecuteNonQuery();
                toolStripStatusLabel1.Text = "Complete.";
            }
        }

        private void изменитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            book result;
            using (InsertForm update = new InsertForm(dataGridView1.CurrentRow))
            {
                update.ShowDialog();
                result=update.show();
            }
            if (result.Exist)
            {
                if (command != null)
                    command.Dispose();
                command = new OleDbCommand("UPDATE [Книги]" +
                    $"SET [Артикул] = {result.Articul},[ФИО] = '{result.FIO}',[Название] = '{result.AuthorName}',[Год издания] ={result.Year},[Цена] = {result.Price},[Жанр] = '{result.Genre}'" +
                    $"WHERE [Код]={dataGridView1.CurrentRow.Cells[0].Value}", connection);
                command.ExecuteNonQuery();
                toolStripStatusLabel1.Text = "Complete.";
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(connection.State == ConnectionState.Open)
                connection.Close();
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Удалить эту запись?","Предупреждение",MessageBoxButtons.OKCancel);
            if(dialogResult == DialogResult.OK)
            {
                DataGridViewRow row = dataGridView1.CurrentRow;
                if (command != null)
                    command.Dispose();
                command = new OleDbCommand("DELETE FROM [Книги]" +
                    $"WHERE [Код]={row.Cells[0].Value}", connection);
                command.ExecuteNonQuery();
                toolStripStatusLabel1.Text = "Complete.";
            }
        }
    }
}
