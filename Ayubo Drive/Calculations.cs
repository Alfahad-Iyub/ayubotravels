using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient; //this is used to help us connect to the database to read/write in them.

namespace Ayubo_Drive
{
    public class Calculations
    {
        public int getRent(int vehicleid,int noofdays,bool driver)
        {
            int Rent = 0; //declaring variable

            SqlConnection con = new SqlConnection(); //connection to the database
            con.ConnectionString = "Data Source=FAHAD\\FAHADSQL;Initial Catalog=AyuboDrive;Integrated Security=True";

            con.Open(); //copening the connection

            SqlCommand cmd = new SqlCommand(); //to execute sql commands
            cmd.Connection = con;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "select * from RentCalculation where Vehicle_ID='" + vehicleid + "'"; //sql query

            SqlDataReader sdr = cmd.ExecuteReader(); //it reads the table in database from first to last

            string dailyrent = ""; //declaring variables
            string weeklyrent = "";
            string monthlyrent = "";
            string driverrate = "";

            while (sdr.Read())
            {
                dailyrent = sdr["Daily_Rent"].ToString(); //retreive data from database as int and assign to new variables as string
                weeklyrent = sdr["Weekly_Rent"].ToString();
                monthlyrent = sdr["Monthly_Rent"].ToString();
                driverrate = sdr["Driver_Rate"].ToString();
            }

            int Val_dailyrent = Convert.ToInt32(dailyrent); //assigning above retrived data into new variables and converting into INT32.
            int Val_weeklyrent = Convert.ToInt32(weeklyrent);
            int Val_monthlyrent = Convert.ToInt32(monthlyrent);
            int Val_driverrate = Convert.ToInt32(driverrate);

            int no_of_days = (noofdays % 31) % 7; //calculation to find number of days
            int no_of_weeks = (noofdays % 31) / 7; //calculation to find number of weeks
            int no_of_months = noofdays / 31; //calculation to find number of years

            if (driver==false) //to check if the driver checkbox is ticked or no
            {
                Val_driverrate = 0;
            }

            //calculates the total rent
            Rent = (no_of_days * Val_dailyrent) + (no_of_weeks * Val_weeklyrent) + (no_of_months * Val_monthlyrent) + (Val_driverrate * no_of_days);

            con.Close(); //closes connection

            return Rent; //returns the calculated value to be retrived by relevant button 
        }

        public double getDayRent(string vehicleid,string packagetype,DateTime starttime,DateTime endtime,int startkm,int endkm,out double BaseHireCharge,out double WaitingCharge,out double ExtraKmCharge)
        {
            double DayRent; //declaring variables

            double maximumkm = 0; //declaring variables
            double priceperkm = 0;
            double extrakmcharge = 0;
            double waitingcharge = 0;

            SqlConnection con = new SqlConnection(); //connection to the database
            con.ConnectionString = "Data Source=FAHAD\\FAHADSQL;Initial Catalog=AyuboDrive;Integrated Security=True";

            con.Open(); //opens connection

            SqlCommand cmd = new SqlCommand(); //to execute sql command
            cmd.Connection = con;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "select * from DayTourHireCalculation where Vehicle_ID='" + vehicleid + "' and Package_Type='" + packagetype + "'";

            SqlDataReader sdr = cmd.ExecuteReader(); //reads the table in the database from first to last
            while (sdr.Read())
            {
                maximumkm = Convert.ToDouble(sdr["Max_KM"].ToString()); //retreive data from database as int and convert it into string and then aain convert them to double
                priceperkm = Convert.ToDouble(sdr["Price_Per_KM"].ToString());
                extrakmcharge = Convert.ToDouble(sdr["Extra_KM_Charge"].ToString());
                waitingcharge = Convert.ToDouble(sdr["Waiting_Charge"].ToString());
            }

            double ExtraKm = 0; //assigning new variable
            double totalkm = endkm - startkm;
            if  (totalkm  > maximumkm)
            {
                ExtraKm = totalkm - maximumkm; //finds the extra km travelled
            }

            double ExtraHour = 0; //assigning new variable
            double totalhour = endtime.Subtract(starttime).TotalHours;
            if  (totalhour  > 24)
            {
                ExtraHour = totalhour - 24; //find the extra hour 
            }

            //calculates the relevent rates
            BaseHireCharge = maximumkm * priceperkm;
            ExtraKmCharge = ExtraKm * extrakmcharge;
            WaitingCharge = ExtraHour * waitingcharge;

            DayRent = BaseHireCharge + ExtraKmCharge + WaitingCharge;

            con.Close(); //closes connection

            return DayRent; //returns the calulated values to be retrived by the relevant page

        }

        public double getlongrent(string vehicleid, string packagetype, DateTime starttime, DateTime endtime, int startkm, int endkm, out double BaseHireCharge, out double ExtraKmCharge, out double OvernightStayCharge)
        {
            double LongRent; //declaring variables

            double maximumkm = 0;
            double priceperkm = 0;
            double extrakmcharge = 0;
            double driverovernightcharge = 0;
            double vehicleparkingrate = 0;

            SqlConnection con = new SqlConnection(); //connection to the database
            con.ConnectionString = "Data Source=FAHAD\\FAHADSQL;Initial Catalog=AyuboDrive;Integrated Security=True";

            con.Open(); //opens connection

            SqlCommand cmd = new SqlCommand(); //to execute sql command
            cmd.Connection = con;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "select * from LongTourHireCalculation where Vehicle_ID = '" + vehicleid + "' and Package_Type = '" + packagetype + "'";

            SqlDataReader sdr = cmd.ExecuteReader(); //reads the table in the database from first to last

            while (sdr.Read()) // reads the data in database as strings - assigns it to variables - convert them into double
            {
                maximumkm = Convert.ToDouble(sdr["Max_KM"].ToString());
                priceperkm = Convert.ToDouble(sdr["Price_Per_KM"].ToString());
                extrakmcharge = Convert.ToDouble(sdr["Extra_KM_Charge"].ToString());
                driverovernightcharge = Convert.ToDouble(sdr["Driver_Overnight_Charge"].ToString());
                vehicleparkingrate = Convert.ToDouble(sdr["Vehicle_Night_Parking_Rate"].ToString());
            }

            double ExtraKm = 0; //assigning new variables
            double totalkm = endkm - startkm;
            if (totalkm > maximumkm)
            {
                ExtraKm = totalkm - maximumkm; //find the extra km travelled
            }

            double OverNightCharge = 0; //assigning new variables
            double noofdays = endtime.Subtract(starttime).TotalDays;
            if (noofdays > 2)
            {
                OverNightCharge = noofdays - 2; //find the overnight stay 
            }

            //caluculates the relevant charges
            BaseHireCharge = maximumkm * priceperkm;
            OvernightStayCharge = (driverovernightcharge + vehicleparkingrate) * OverNightCharge;
            ExtraKmCharge = ExtraKm * extrakmcharge;

            LongRent = BaseHireCharge + OvernightStayCharge + ExtraKmCharge;

            con.Close(); //closes the connection

            return LongRent; //returns the calculated value to be retrived by the relevant page
        }
    }
}
