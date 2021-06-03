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
    public partial class RentCalculation : Form
    {
        public RentCalculation()
        {
            InitializeComponent();
        }

        private void RentCalculation_Load(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //to get vehicle type when we select the vehicle id

            SqlConnection con = new SqlConnection(); //this is the connection to the database
            con.ConnectionString = "Data Source=FAHAD\\FAHADSQL;Initial Catalog=AyuboDrive;Integrated Security=True";

            con.Open(); //opening the connection
            SqlCommand cmd = new SqlCommand(); //to execute sql commands
            cmd.Connection = con;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "select * from RentCalculation where Vehicle_ID='" + comboBox1.Text + "'";

            cmd.ExecuteNonQuery(); //executes the command and returns the number of rows affected
            SqlDataReader dr; //reads data retrived from database
            dr = cmd.ExecuteReader(); //it reads the table in database from first to last
            while (dr.Read())
            {
                string vehicletype = (string)dr["Vehicle_Type"].ToString();
                textBox1.Text = vehicletype;
            }
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //to get the rent calculation with connection to the calculation class

            string vehicleid = comboBox1.Text; //declaring variables
            DateTime date1 = dateTimePicker1.Value;
            DateTime date2 = dateTimePicker2.Value;
            int noofdays = (int)date2.Subtract(date1).TotalDays + 1;

            bool driver = false;
            if (checkBox1.Checked == true) //using IF statement to confirm if the box is checked
            {
                driver = true;
            }

            Calculations c = new Calculations(); //connection to the calculation class
            int Rent = c.getRent(Convert.ToInt32(vehicleid), Convert.ToInt32(noofdays), driver);
            textBox9.Text = Convert.ToString(Rent); // assigning the total value to the relevent textbox
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide(); //going back to the previous page
            MainMenu_User_ obj = new MainMenu_User_();
            obj.Show();
        }
    }
}
