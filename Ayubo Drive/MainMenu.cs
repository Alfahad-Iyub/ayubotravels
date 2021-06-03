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
    public partial class MainMenu_User_ : Form
    {
        public MainMenu_User_()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            RentCalculation obj = new RentCalculation(); //navigates to relevant page
            obj.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            DayTourCalculation obj = new DayTourCalculation(); //navigates to relevant page
            obj.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            LongTourHireCalculation obj = new LongTourHireCalculation(); //navigates to relevant page
            obj.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login obj = new Login(); //navigates to relevant page
            obj.Show();
        }
    }
}
