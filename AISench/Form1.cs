using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading.Tasks;
using System.IO.Compression;

namespace AIS
{
    public partial class Form1 : Form
    {
        OleDbConnection connection;
        OleDbCommand command;
        OleDbDataAdapter reader;
        DataTable table;
        User signed;
        StreamWriter log;
        
        public Form1()
        {
            log = new StreamWriter("log.txt", true);
            InitializeComponent();
            dataGridView1.Size = new Size(
                this.ClientSize.Width - dataGridView1.Left * 2,
                this.ClientSize.Height - dataGridView1.Top * 2);
            
        }

        private void LOG(User user, string question)
        {
            var task = new Task(() => { log.WriteLine($"({DateTime.UtcNow}) {user.Role} '{user.Name}' выполнил команду [{question}]"); });
            task.Start();
        }
        private void sELECTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string question = "SELECT * FROM Книги";
            reader = new OleDbDataAdapter(question, connection);
            table = new DataTable();
            reader.Fill(table);
            dataGridView1.DataSource = table;
            LOG(signed, "Select All.");
        }

        private void подключитьсяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (AutentificationF signIn = new AutentificationF())
            {
                signIn.ShowDialog();
                signed = signIn.Signed();
            }
            if (signed.Name == null) return;
            if (signed.Role == "Администратор") 
            { 
                правкаToolStripMenuItem.Visible = true;
                запросToolStripMenuItem.Visible = true;
                сделатьОнтрольнуюТочкуToolStripMenuItem.Visible = true;
                загрузитьКонтрольнуюТочкуToolStripMenuItem.Visible = true;
            }
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
            if (signed.Role == "Администратор")
            {
                правкаToolStripMenuItem.Visible = false;
                запросToolStripMenuItem.Visible = false;
                сделатьОнтрольнуюТочкуToolStripMenuItem.Visible = false;
                загрузитьКонтрольнуюТочкуToolStripMenuItem.Visible = false;
                signed = new User();
            }
            toolStripStatusLabel1.Text = "Connection closed.";
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            dataGridView1.Size = new Size(
                this.ClientSize.Width - dataGridView1.Left * 2,
                this.ClientSize.Height - dataGridView1.Top * 2);
            this.Refresh();
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
                string question = "INSERT INTO [Книги]" +
                    "([Артикул],[ФИО],[Название],[Год издания],[Цена],[Жанр])" +
                    $"VALUES ({result.Articul},{result.FIO},{result.AuthorName},{result.Year},{result.Price},{result.Genre});";
                command = new OleDbCommand(question, connection);
                command.ExecuteNonQuery();
                toolStripStatusLabel1.Text = "Complete.";
                LOG(signed, $"Add: [{result.Articul},{result.FIO},{result.AuthorName},{result.Year},{result.Price},{result.Genre}]");
                question = "SELECT * FROM Книги";
                reader = new OleDbDataAdapter(question, connection);
                table = new DataTable();
                reader.Fill(table);
                dataGridView1.DataSource = table;
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
                string question = "UPDATE [Книги]" +
                    $"SET [Артикул] = {result.Articul},[ФИО] = '{result.FIO}',[Название] = '{result.AuthorName}',[Год издания] ={result.Year},[Цена] = {result.Price},[Жанр] = '{result.Genre}'" +
                    $"WHERE [Код]={dataGridView1.CurrentRow.Cells[0].Value}";
                command = new OleDbCommand(question, connection);
                command.ExecuteNonQuery();
                toolStripStatusLabel1.Text = "Complete.";
                LOG(signed, $"Update: [{RowToString(dataGridView1.CurrentRow)}]");
                question = "SELECT * FROM Книги";
                reader = new OleDbDataAdapter(question, connection);
                table = new DataTable();
                reader.Fill(table);
                dataGridView1.DataSource = table;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(connection != null)
                connection.Dispose();
            log.Close();
        }
        string RowToString(DataGridViewRow row)
        {
            string result = "";
            for(int i = 0;i<row.Cells.Count;i++)
            {
                result += $"{row.Cells[i].Value}, ";
            }
            return result.Substring(0,result.Length-2);
        }
        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Удалить эту запись?","Предупреждение",MessageBoxButtons.OKCancel);
            if(dialogResult == DialogResult.OK)
            {
                DataGridViewRow row = dataGridView1.CurrentRow;
                if (command != null)
                    command.Dispose();
                string question = "DELETE FROM [Книги]" +
                    $"WHERE [Код]={row.Cells[0].Value}";
                command = new OleDbCommand(question, connection);
                command.ExecuteNonQuery();
                toolStripStatusLabel1.Text = "Complete.";
                LOG(signed, $"Delete: [{RowToString(row)}]");
                question = "SELECT * FROM Книги";
                reader = new OleDbDataAdapter(question, connection);
                table = new DataTable();
                reader.Fill(table);
                dataGridView1.DataSource = table;
            }
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Информационная система книжного магазина. \nВерсия: 1.0.0.0. \nРелиз: 21.11.2023.", "О програме");
        }

        private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (signed.Role == null)
            {
                MessageBox.Show("Вы не авторизованы для получения справки", "Внимание");
                return;
            }
            string message = "'Покдлючение' - авторизация пользователя и получения доступа к информационной системе." +
                "\n'Запрос' - получение таблицы из базы данных или добавление нового элемента.";
            if (signed.Role == "Администратор")
            {
                message += "\n'Правка' - изменение(или удаление) конкретного элемента базы данных";
            }
            MessageBox.Show(message,"Справка по использованию продукта.");
        }

        private void сделатьОнтрольнуюТочкуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            connection.Close();
            FileStream input = new FileStream("D:\\АИС.accdb", FileMode.Open);
            FileStream output = new FileStream($"АИС({DateTime.UtcNow.ToString().Replace(':', '-').Replace('.', '-')}).gz", FileMode.Create);
            GZipStream stream = new GZipStream(output, CompressionMode.Compress);
            input.CopyTo(stream);
            MessageBox.Show("Контрольная точка создана");
            input.Close();
            input.Dispose();
            stream.Close();
            stream.Dispose();
            output.Close();
            output.Dispose();
            connection.Open();
            LOG(signed, "SnapShot");
        }

        private void загрузитьКонтрольнуюТочкуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog OFD = new OpenFileDialog();
            connection.Close();
            OFD.Filter = "BackUpDB|*.gz|All files|*.*";
            string back_up_file = "";
            DialogResult warning = MessageBox.Show("Сделать копию базы данных?", "Предупреждение", MessageBoxButtons.OKCancel);
            if (warning == DialogResult.OK)
            {
                FileStream input = new FileStream("D:\\АИС.accdb", FileMode.Open);
                FileStream output = new FileStream($"АИС({DateTime.UtcNow.ToString().Replace(':', '-').Replace('.', '-')}).gz", FileMode.Create);
                GZipStream stream = new GZipStream(output, CompressionMode.Compress);
                input.CopyTo(stream);
                MessageBox.Show("Контрольная точка создана");
                input.Close();
                input.Dispose();
                stream.Close();
                stream.Dispose();
                output.Close();
                output.Dispose();
                LOG(signed, "SnapShot");
            }
            if (OFD.ShowDialog() == DialogResult.OK)
            {
                back_up_file = OFD.FileName;
                FileStream input = new FileStream(back_up_file, FileMode.Open);
                FileStream output = new FileStream($"АИС.accdb", FileMode.Create);
                GZipStream stream = new GZipStream(output, CompressionMode.Decompress);
                input.CopyTo(output);
                MessageBox.Show("Контрольная точка загружена");
                input.Close();
                input.Dispose();
                stream.Close();
                stream.Dispose();
                output.Close();
                output.Dispose();
                LOG(signed, "DeSnapShot");
            }
            OFD.Dispose();
            connection.Open();
        }
    }
}
