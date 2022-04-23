using MySql.Data.MySqlClient;
using Org.BouncyCastle.Crypto.Tls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp6
{
    class DataBase
    {
        public bool ExecuteCommand(MySqlCommand command)
        {
            bool bl = true;
            try
            {
                Variables.Connection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception x) { MessageBox.Show(x.ToString()); bl = false; }
            if (Variables.Connection.State == ConnectionState.Open)
                Variables.Connection.Close();
            return bl;
        }
        public void FillConnection(string server, string port, string database, string username, string password)
        {
            Variables.Connection = new MySqlConnection(@"Server = " + server + "; Port = " + port + "; DataBase = '" + database + "'; user ID = '" + username + "'; Password = '" + password + "'; CharSet=utf8 ");
        }
        public bool TestConnection()
        {
            bool success = true;
            try
            {
                Variables.Connection.Open();
            }
            catch
            {
                MessageBox.Show("Подключение не удалось.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                success = false;
            }
            if (Variables.Connection.State == ConnectionState.Open)
                Variables.Connection.Close();
            return success;
        }
        public void SaveConnection(string server, string port, string database, string username, string password)
        {
            if(!(Directory.Exists(Path.GetDirectoryName(Variables.filepath))))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(Variables.filepath));
                using (FileStream fs = File.Create(Variables.filepath)) { }
            }
            else if(!(File.Exists(Variables.filepath)))
                using (FileStream fs = File.Create(Variables.filepath)) { }
            try
            {
                using(FileStream fs= File.Open(Variables.filepath, FileMode.Open)) { fs.SetLength(0); }
                using (StreamWriter sw = new StreamWriter(Variables.filepath))
                {
                    sw.WriteLine(server);
                    sw.WriteLine(port);
                    sw.WriteLine(database);
                    sw.WriteLine(username);
                    sw.WriteLine(password);
                }
            }
            catch
            {
                MessageBox.Show("Невозможно получить доступ к файлу", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void LoadConnection(TextBox server, TextBox port, TextBox database, TextBox username, TextBox password)
        {
            if (File.Exists(Variables.filepath))
            {
                try
                {
                    using (StreamReader sr = new StreamReader(Variables.filepath))
                    {
                        server.Text = sr.ReadLine();
                        port.Text = sr.ReadLine();
                        database.Text = sr.ReadLine();
                        username.Text = sr.ReadLine();
                        password.Text = sr.ReadLine();
                    }
                }
                catch
                {
                    MessageBox.Show("Невозможно получить думтуп к файлу", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                MessageBox.Show("Файл сохранения не найден.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public void OpenDirectory()
        {
            if (Directory.Exists(Path.GetDirectoryName(Variables.filepath)))
            {
                Process.Start(Path.GetDirectoryName(Variables.filepath));
            }
            else
                MessageBox.Show("Файл сохранения не найден.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}