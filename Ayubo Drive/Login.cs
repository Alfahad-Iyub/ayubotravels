using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ayubo_Drive
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text; //Declaring Variables
            string password = textBox2.Text;

            if (username == "Admin" && password == "admin123") //using IF statements to make sure the user enters correct credentials
            {
                MessageBox.Show("Login Success !");
                this.Hide();
                MainMenu_User_ obj = new MainMenu_User_();
                obj.Show();
            }
            else
            {
                MessageBox.Show("Login Not Success !");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit(); //closes application
        }
    }
}
