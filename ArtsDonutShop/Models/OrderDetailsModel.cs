using System;
using System.Data.SqlClient;

namespace ArtsDonutShop.Models
{
    public class OrderDetailsModel
    {
        #region Order Details Properties
        public int ordNo { get; set; }
        public int itemNo { get; set; }
        public int qtyOrder { get; set; }

        SqlConnection con = new SqlConnection("server=DESKTOP-6T8MT4L\\MYSQL_ART;database=ArtsDonutShop;integrated security=true");
        #endregion

        #region Get Orders Details
        public List<OrderDetailsModel> GetOrdersDetails()
        {
            SqlCommand cmd_allordersD = new SqlCommand("select * from OrderDetails", con);
            List<OrderDetailsModel> ordDlist = new List<OrderDetailsModel>();
            SqlDataReader readAllOrdersD = null;

            try
            {
                con.Open();
                readAllOrdersD = cmd_allordersD.ExecuteReader();

                while (readAllOrdersD.Read())
                {
                    ordDlist.Add(new OrderDetailsModel()
                    {
                        ordNo = Convert.ToInt32(readAllOrdersD[0]),
                        itemNo = Convert.ToInt32(readAllOrdersD[1]),
                        qtyOrder = Convert.ToInt32(readAllOrdersD[2])

                    });
                }

            }
            catch (SqlException ex)
            {

                throw new Exception(ex.Message);
            }
            finally
            {
                readAllOrdersD.Close();
                con.Close();
            }
            return ordDlist;
        }
        #endregion

        #region Get Order Detail by OrderNo
        public List<OrderDetailsModel> GetOrderDetailsItem(int ordNo)
        {
            SqlCommand cmd_searchByordNo = new SqlCommand("select * from OrderDetails where ordNo=@ordNo", con);
            cmd_searchByordNo.Parameters.AddWithValue("@ordNo", ordNo);
            List<OrderDetailsModel> ordrecD = new List<OrderDetailsModel>();
            SqlDataReader read_orderD = null;
            try
            {
            con.Open();
            read_orderD = cmd_searchByordNo.ExecuteReader();
            while (read_orderD.Read())

            {
                ordrecD.Add(new OrderDetailsModel()
                {
                    ordNo = Convert.ToInt32(read_orderD[0]),
                    itemNo = Convert.ToInt32(read_orderD[1]),
                    qtyOrder = Convert.ToInt32(read_orderD[2])
                });
            }

            }
            catch (SqlException es)
            {

                throw new Exception(es.Message);
            }
            finally
            {
                read_orderD.Close();
                con.Close();

            }
            return ordrecD;
        }
        #endregion

        #region POST Add New Order Detail
        public string AddOrder(OrderDetailsModel newOrderD)
        {
            SqlCommand cmd_addOrderD = new SqlCommand("insert into OrderDetails values(@ordNo,@itemNo,@qtyOrder)", con);
            cmd_addOrderD.Parameters.AddWithValue("@ordNo", newOrderD.ordNo);
            cmd_addOrderD.Parameters.AddWithValue("@itemNo", newOrderD.itemNo);
            cmd_addOrderD.Parameters.AddWithValue("@qtyOrder", newOrderD.qtyOrder);


            try
            {
                con.Open();
                cmd_addOrderD.ExecuteNonQuery();
            }
            catch (Exception es)
            {

                throw new Exception(es.Message);
            }
            finally
            {
                con.Close();
            }
            return "New Order Added Successfully";
        }
        #endregion

        #region DELETE Order
        public string DeleteOrderDetail(int ordNo)
        {
            SqlCommand cmd_deleteOrderD = new SqlCommand("delete from OrderDetails where ordNo=@ordNo", con);
            cmd_deleteOrderD.Parameters.AddWithValue("@ordNo", ordNo);
            try
            {
                con.Open();
                cmd_deleteOrderD.ExecuteNonQuery();
            }
            catch (Exception es)
            {

                throw new Exception(es.Message);
            }
            finally
            {
                con.Close();
            }
            return "Order Detail Was Deleted Successfully!";
        }
        #endregion

        #region UPDATE Order Detail
        public string UpdateOrder(OrderDetailsModel updOrderD)
        {
            SqlCommand cmd_updOrderD = new SqlCommand("update OrderDetails set itemNo=@itemNo, qtyOrder=@qtyOrder", con);
            cmd_updOrderD.Parameters.AddWithValue("@itemNo", updOrderD.itemNo);
            cmd_updOrderD.Parameters.AddWithValue("@qtyOrder", updOrderD.qtyOrder);

            try
            {
                con.Open();
                cmd_updOrderD.ExecuteNonQuery();

            }
            catch (Exception es)
            {

                throw new Exception(es.Message);
            }
            finally
            {
                con.Close();
            }
            return "Order Details was Updated Successfully.";
        }
        #endregion        
    }
}