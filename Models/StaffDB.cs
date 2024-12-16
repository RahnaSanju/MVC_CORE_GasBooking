using System.Data.SqlClient;
using System.Data;

namespace MVC_CORE_Project.Models
{
    public class StaffDB
    {
        SqlConnection con = new SqlConnection(@"server=DESKTOP-8PBBHUD\SQLEXPRESS;database=Gas_Booking_Project;integrated security=true;");
        int regid = 1;

        public string InsertStaffDB(StaffCls objCls)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_getMaxRegID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read() == true)
                {

                    int maxId = Convert.ToInt32(dr["MaxId"]);
                    if (maxId > 0)
                    {
                        regid = maxId + 1;
                    }
                }
                con.Close();

                SqlCommand cmd1 = new SqlCommand("sp_Insert_Staff", con);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@stfId", regid);
                cmd1.Parameters.AddWithValue("@stfName", objCls.sName);
                cmd1.Parameters.AddWithValue("@stfPhone", objCls.sPhone);
                cmd1.Parameters.AddWithValue("@stfEmail", objCls.sEmail);
                cmd1.Parameters.AddWithValue("@stfStatus", "active");
                con.Open();
                cmd1.ExecuteNonQuery();
                con.Close();

                SqlCommand cmd2 = new SqlCommand("sp_Insert_Login", con);
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.Parameters.AddWithValue("@regId", regid);
                cmd2.Parameters.AddWithValue("@usrnm", objCls.sUsrnm);
                cmd2.Parameters.AddWithValue("@pwd", objCls.sPwd);
                cmd2.Parameters.AddWithValue("@lgType", "Staff");
                cmd2.Parameters.AddWithValue("@lgStatus", "active");
                con.Open();
                cmd2.ExecuteNonQuery();
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

        public int CheckUsernameExistsDB(StaffCls objCls)
        {
            SqlCommand cmd = new SqlCommand("[sp_getCountId]", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("usrnm", objCls.sUsrnm);
            SqlParameter sp = new SqlParameter();
            sp.ParameterName = "@status";
            sp.DbType = DbType.Int32;
            sp.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(sp);
            con.Open();
            cmd.ExecuteReader();
            con.Close();
            return (Convert.ToInt32(sp.Value));
        }

    }
}
