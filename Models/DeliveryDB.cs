using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MVC_CORE_Project.Models
{
    public class DeliveryDB
    {
        SqlConnection con = new SqlConnection(@"server=DESKTOP-8PBBHUD\SQLEXPRESS;database=Gas_Booking_Project;integrated security=true;");

        public List<DeliveryCls> GetDeliveryDetails()
        {
            try
            {
                var list=new List<DeliveryCls>();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                SqlCommand cmd = new SqlCommand("sp_Get_Delivery_Details", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader dr= cmd.ExecuteReader();
                while(dr.Read())
                {
                    var getdata = new DeliveryCls
                    {
                        bookId = Convert.ToInt32(dr["Bk_Id"]),
                        BkDate = dr["Bk_Date"].ToString(),
                        custId = Convert.ToInt32(dr["Cust_Id"]),
                        custName = dr["cust_Name"].ToString(),
                        custAddr = dr["cust_Address"].ToString(),
                        custConsNo = dr["Consumer_No"].ToString(),
                        custPhone = dr["cust_Phone"].ToString(),
                        cylId = Convert.ToInt32(dr["cyl_Id"]),
                        cylType = dr["cyl_Type"].ToString(),
                        cylAmt = Convert.ToDouble(dr["Amount"])
                    };
                    list.Add(getdata);
                    
                }
                con.Close();
                return list;

            }
            catch (Exception)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                throw;
            }
        }

        public DeliveryCls getBookingDetails(int id)
        {
            try
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                var getdata = new DeliveryCls();
                SqlCommand cmd = new SqlCommand("sp_Get_Booking_Details", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@bkId", id);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    getdata.bookId = Convert.ToInt32(dr["Bk_Id"]);
                    getdata.custId = Convert.ToInt32(dr["Cust_Id"]);
                    getdata.cylId = Convert.ToInt32(dr["cyl_Id"]);
                }
                con.Close();
                return getdata;
            }
            catch (Exception)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                throw;
            }
        }

        public string InsertDeliveryDB(int id,DateTime date,int stfId,int cylId)
        {
            try
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                SqlCommand cmd = new SqlCommand("sp_Insert_Delivery", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@bkId", id);
                cmd.Parameters.AddWithValue("@stfId", stfId);
                cmd.Parameters.AddWithValue("@date", date);
                cmd.Parameters.AddWithValue("@cylId", cylId);
                cmd.ExecuteNonQuery();
                con.Close();

                return ("Delivered Successfully");
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
    }
}
