using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security;
using System.Security.Cryptography;
using System.IO;
using System.Data.OleDb;

namespace AISench
{
    public struct User
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
    public partial class AutentificationF : Form
    {
        OleDbConnection connection;
        OleDbCommand command;
        public AutentificationF()
        {
            InitializeComponent();
            //'Админ','System','Администратор'
        }
        bool signed = false;
        private void bt_sign_up_Click(object sender, EventArgs e)
        {
            string login = tb_login.Text;
            string password = tb_password.Text;
            connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + @"D:\АИС.accdb;" + "Persist Security Info=false;");
            connection.Open();
            var sha_pass = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(password));
            string search = "";
            foreach(byte symbol in sha_pass)
            {
                search += symbol.ToString("x2");
            }
            command = new OleDbCommand($"Select Count(*) From [Учётные записи] WHERE [Логин]='{login}' AND [Пароль]='{search}'", connection);
            int exist = (int)command.ExecuteScalar();
            signed = (exist == 1) ;
            if (signed)
                this.Dispose();
        }
        public bool Signed()
        {
            return signed;
        }
    }
}
