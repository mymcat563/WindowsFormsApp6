using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp6
{
    public partial class TittleForm : Form
    {
        ResizeHandler Sizer = new ResizeHandler();
        public TittleForm()
        {
            InitializeComponent();
            Sizer.Form = this;
            Sizer.GroupBox = gbServer;
        }
        private void btSettings_Click(object sender, EventArgs e)
        {
            Sizer.TittleResize();
        }
        private void btExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void btStart_Click(object sender, EventArgs e)
        {
            DataBase DB = new DataBase();
            DB.FillConnection(tbServer.Text, tbPort.Text, tbDB.Text, tbLogin.Text, tbPassword.Text);
            if (DB.TestConnection())
            {
                AuthForm frm = new AuthForm();
                Hide();
                frm.ShowDialog();
            }
        }

        private void btClear_Click(object sender, EventArgs e)
        {
            tbServer.Text = "88.205.135.82";
            tbPort.Text = "7077";
            tvDB.Text = "children_entertaiment";
            tbLogin.Text = "entertain";
            tbPassword.Text = "1234";
        }

        private void brload_Click(object sender, EventArgs e)
        {
            DataBase DB = new DataBase();
            DB.LoadConnection(tbServer, tbPort, tbDB, tbLogin, tbPassword);
        }

        private void TitleForm_Load(object sender, EventArgs e)
        {
            if (File.Exists(Variables.filepath))
                btLoad_Click(sender, e);
        }

        private void btOpenDir_Click(object sender, EventArgs e)
        {
            DataBase DB = new DataBase();
            DB.OpenDirectory();
        }

        public bool AuthCheck(string login, string password)
        {
            DataTable result = Select("users", "*", "login='" + login + "'AND password'" + password + "'AND enabled = 1");
            if (result.Rows.Count == 1)
            {
                Variables.User.Fill(Convert.ToInt32(result.Rows[0][0]), result.Rows[0][2].ToString() + " " + result.Rows[0][3].ToString() + " " + result.Rows[0][4].ToString(), login, result.Rows[0][1].ToString());
                return true;
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль", "Ошибка");
                return false;
            }
        }

        private void TittleForm_Load(object sender, EventArgs e)
        {

        }
    }
}
