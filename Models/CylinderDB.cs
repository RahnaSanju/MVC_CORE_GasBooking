using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;

namespace MVC_CORE_Project.Models
{
    public class CylinderDB
    {
        SqlConnection con = new SqlConnection(@"server=DESKTOP-8PBBHUD\SQLEXPRESS;database=Gas_Booking_Project;integrated security=true;");

        public string InsertCylinderDB(CylinderCls objCls)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_Insert_Cylinder", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@cyl_Type", objCls.CylinderType);
                cmd.Parameters.AddWithValue("@totStck", objCls.TotalStock);
                cmd.Parameters.AddWithValue("@fild", objCls.Filled);
                cmd.Parameters.AddWithValue("@emp", objCls.Empty);
                cmd.Parameters.AddWithValue("@amt", objCls.Amount);
                cmd.ExecuteNonQuery();
                con.Close();

                return ("Inserted Successfully");
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return ex.Message.ToString();
            }
        }

        public string UpdateCylinderDB(CylinderCls objCls)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_Update_Cylinder", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@cyl_Type", objCls.CylinderType);
                cmd.Parameters.AddWithValue("@totStck", objCls.TotalStock);
                cmd.Parameters.AddWithValue("@fild", objCls.Filled);
                cmd.Parameters.AddWithValue("@emp", objCls.Empty);
                cmd.Parameters.AddWithValue("@amt", objCls.Amount);
                cmd.ExecuteNonQuery();
                con.Close();

                return ("Updated Successfully");
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return ex.Message.ToString();
            }
        }

        public double GetCylDetails(int CylId,out int totstck,out int fild, out int empt)
        {
            double amt = 0;
            totstck = 0; fild = 0; empt = 0;
            SqlCommand cmd = new SqlCommand("sp_getCylinderDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@cyl_Id", CylId);
            SqlDataReader dr = cmd.ExecuteReader();
            if(dr.Read()==true)
            {
                totstck = Convert.ToInt32(dr["Total_Stock"]);
                amt = Convert.ToInt32(dr["Amount"]);
                fild = Convert.ToInt32(dr["filled"]);
                empt = Convert.ToInt32(dr["Empty"]);
                //price = Convert.ToDouble(dr["Amount"]);
            }
            con.Close();
            return amt;
        }

    }
}
