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
        public string Role { get; set; }
    }
    public partial class AutentificationF : Form
    {
        User user;
        
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
            int exist = 0;
            using (OleDbConnection connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + @"D:\АИС.accdb;" + "Persist Security Info=false;"))
            {
                connection.Open();
                byte[] sha_pass;
                using (SHA256 coder = SHA256.Create()) 
                { 
                    sha_pass = coder.ComputeHash(Encoding.UTF8.GetBytes(password)); 
                }
                string search = "";
                foreach (byte symbol in sha_pass)
                {
                    search += symbol.ToString("x2");
                }
                List<string> result = new List<string>();
                using (OleDbCommand command = new OleDbCommand($"Select * From [Учётные записи] WHERE [Логин]='{login}' AND [Пароль]='{search}'", connection))
                {
                    OleDbDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                        result.Add(reader[3].ToString());
                    exist = result.Count;
                    user.Name = login;
                    user.Role = result[0];
                }
            }
            signed = (exist == 1) ;
            if (signed)
            {
                this.Dispose();
            }
        }
        public User Signed()
        {
            return user;
        }
    }
}
