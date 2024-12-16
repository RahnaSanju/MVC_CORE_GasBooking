using System.Data;
using System.Data.SqlClient;

namespace MVC_CORE_Project.Models
{
    public class BookingDB
    {
        SqlConnection con = new SqlConnection(@"server=DESKTOP-8PBBHUD\SQLEXPRESS;database=Gas_Booking_Project;integrated security=true;");

        public string InsertBookingDB(BookingCls objCls)
        {
            try
            {
                if(con.State==ConnectionState.Open)
                {
                    con.Close();
                }
                SqlCommand cmd = new SqlCommand("sp_Insert_Booking", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@cyl_Id", objCls.CylinderId);
                cmd.Parameters.AddWithValue("@cust_Id", objCls.CustId);
                cmd.Parameters.AddWithValue("@staff_Id", objCls.StaffId);
                cmd.Parameters.AddWithValue("@bk_Date", objCls.BookingDate);
                cmd.Parameters.AddWithValue("@bk_Status", objCls.BookingStatus);
                cmd.Parameters.AddWithValue("@bk_Mode", objCls.BookingMode);
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

        public string CancelBookingDB(int CustID)
        {
            try
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                SqlCommand cmd = new SqlCommand("sp_cancelBooking", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@custid", CustID);
                cmd.ExecuteNonQuery();
                con.Close();

                return ("Cancelled Successfully");
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
        public int GetCustomerID(int ConsNo)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            int custId = 0;
            SqlCommand cmd = new SqlCommand("sp_getCustomerId", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@consNo", ConsNo);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if(dr.Read()==true)
            {
                custId = Convert.ToInt32(dr["Cust_Id"]);
            }
            return custId;
        }

        public int GetCustomerBookingStatus(int CustId)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            //int custId = 0;
            SqlCommand cmd = new SqlCommand("sp_getBookingStatus", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@cid", CustId);
            SqlParameter sp = new SqlParameter();
            sp.ParameterName = "@status";
            sp.DbType = DbType.Int32;
            sp.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(sp);
            con.Open();
            cmd.ExecuteReader();
            return (Convert.ToInt32(sp.Value));
        }

        public CustomerCls getCustomerDetails(int ConsNo)
        {

            GetCustomerID(ConsNo);

            CustomerCls obj=new CustomerCls();
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            SqlCommand cmd = new SqlCommand("sp_getCustomerDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@cnsno", ConsNo);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if(dr.Read()==true)
            {
                obj.custName = dr["Cust_Name"].ToString();
                obj.custAddr = dr["Cust_Address"].ToString();
                obj.custPhone = dr["Cust_Phone"].ToString();
            }
            return obj;

        }

        public List<BookingCls> getCustomerDetailsSearch(string qry)
        {
            var list = new List<BookingCls>();
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            SqlCommand cmd = new SqlCommand("sp_CustomerSearch", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@qry", qry);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                var getdata = new BookingCls
                {
                    ConsumerNo = Convert.ToInt32(dr["Consumer_No"]),
                    custName = dr["Cust_Name"].ToString(),
                    custAddr = dr["Cust_Address"].ToString(),
                    custPhone = dr["Cust_Phone"].ToString(),
                    custEmail = dr["Cust_Email"].ToString(),
                    custPhoto = dr["Cust_Photo"].ToString(),
                    CylinderType = dr["Cyl_Type"].ToString(),
                    CylinderId = Convert.ToInt32(dr["Cyl_Id"]),
                    BookingDate = Convert.ToDateTime(dr["Bk_Date"]).ToShortDateString(),
                    BookingStatus = dr["Bk_Status"].ToString(),
                    BookingMode = dr["Bk_Mode"].ToString(),
                };
                list.Add(getdata);
            }
            con.Close();
            return list;


        }
    }
}
