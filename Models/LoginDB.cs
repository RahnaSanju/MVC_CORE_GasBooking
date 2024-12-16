using System.Data;
using System.Data.SqlClient;

namespace MVC_CORE_Project.Models
{
    
    public class LoginDB
    {
        SqlConnection con = new SqlConnection(@"server=DESKTOP-8PBBHUD\SQLEXPRESS;database=Gas_Booking_Project;integrated security=true;");

        public int CheckUsrnmPwdinDB(LoginCls objCls)
        {
            SqlCommand cmd = new SqlCommand("[sp_login]", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("usrnm", objCls.lUsrnm);
            cmd.Parameters.AddWithValue("pwd", objCls.lPwd);
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

        public string CheckLogTypeinDB(LoginCls objCls)
        {
            string logtype = "";
            SqlCommand cmd = new SqlCommand("[sp_getLog_Type]", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("usrnm", objCls.lUsrnm);
            cmd.Parameters.AddWithValue("pwd", objCls.lPwd);
            con.Open() ;
            SqlDataReader dr=cmd.ExecuteReader();
            if(dr.Read()==true)
            {
                 logtype = dr["Log_Type"].ToString();
            }
            con.Close();
            return logtype;
        }

        public int GetRegIDfromDB(LoginCls objCls)
        {
            int regid = 0;
            SqlCommand cmd = new SqlCommand("[sp_getRegID]", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@usrnm", objCls.lUsrnm);
            cmd.Parameters.AddWithValue("@pwd", objCls.lPwd);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read() == true)
            {
                regid = Convert.ToInt32(dr["reg_id"]);
            }
            con.Close();
            return regid;
        }

        //public int GetCustomerBookingStatus(int CustId)
        //{
        //    if (con.State == ConnectionState.Open)
        //    {
        //        con.Close();
        //    }
        //    //int custId = 0;
        //    SqlCommand cmd = new SqlCommand("sp_getBookingStatus", con);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.AddWithValue("@cid", CustId);
        //    SqlParameter sp = new SqlParameter();
        //    sp.ParameterName = "@status";
        //    sp.DbType = DbType.Int32;
        //    sp.Direction = ParameterDirection.Output;
        //    cmd.Parameters.Add(sp);
        //    con.Open();
        //    cmd.ExecuteReader();
        //    return (Convert.ToInt32(sp.Value));
        //}


    }
}
