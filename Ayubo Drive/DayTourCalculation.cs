using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient; // this is used to help us connect to the database to read/write in them.

namespace Ayubo_Drive
{
    public partial class DayTourCalculation : Form
    {
        public DayTourCalculation()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide(); //travels back to the previous page
            MainMenu_User_ obj = new MainMenu_User_();
            obj.Show();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //to get the vehicle type when the vehicle id is selected

            SqlConnection con = new SqlConnection(); //connection to the database
            con.ConnectionString = "Data Source=FAHAD\\FAHADSQL;Initial Catalog=AyuboDrive;Integrated Security=True";

            con.Open(); //opens the connection
            SqlCommand cmd = new SqlCommand(); //to execute sql command
            cmd.Connection = con;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "select * from DayTourHireCalculation where Vehicle_ID='" + comboBox1.Text + "'";

            cmd.ExecuteNonQuery(); //executes the command and returns the number of rows affected
            SqlDataReader dr; // reads data retrived from the database
            dr = cmd.ExecuteReader(); //it reads the table in database from first to last
            while (dr.Read())
            {
                string vehicletype = (string)dr["Vehicle_Type"].ToString();
                textBox4.Text = vehicletype;
            }
            con.Close(); //closes the connection
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string vehicleid = comboBox1.Text; // declaring variables
            string packagetype = comboBox2.Text;
            DateTime starttime = dateTimePicker1.Value;
            DateTime endtime = dateTimePicker2.Value;
            int startkm = Convert.ToInt32(textBox2.Text);
            int endkm = Convert.ToInt32(textBox3.Text);

            Calculations c = new Calculations(); //connection to the calculation class
            double DayRent = c.getDayRent(vehicleid, packagetype, starttime, endtime, startkm, endkm, out double BaseHireCharge, out double WaitingCharge, out double ExtraKmCharge);

            textBox6.Text = Convert.ToString(WaitingCharge); //assigning the calculated values to respected boxes
            textBox7.Text = Convert.ToString(ExtraKmCharge);
            textBox8.Text = Convert.ToString(BaseHireCharge);
            textBox9.Text = Convert.ToString(DayRent);
        }
    }
}
