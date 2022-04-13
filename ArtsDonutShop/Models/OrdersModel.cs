using System;
using System.Data.SqlClient;
namespace ArtsDonutShop.Models
{
    public class OrdersModel
    {
        #region Order Properties
        public int ordNo { get; set; }
        public int ordaccNo { get; set; }
        public double ordTotalCost { get; set; }
        public DateTime ordDate { get; set; }
        #endregion

        SqlConnection con = new SqlConnection("server=DESKTOP-6T8MT4L\\MYSQL_ART;database=ArtsDonutShop;integrated security=true");

        #region Get Orders
        public List<OrdersModel> GetOrders()
        {
            SqlCommand cmd_allorders = new SqlCommand("select * from Orders", con);
            List<OrdersModel> ordlist = new List<OrdersModel>();
            SqlDataReader readAllOrders = null;

            try
            {
                con.Open();
                readAllOrders = cmd_allorders.ExecuteReader();

                while (readAllOrders.Read())
                {
                    ordlist.Add(new OrdersModel()
                    {
                        ordNo = Convert.ToInt32(readAllOrders[0]),
                        ordaccNo = Convert.ToInt32(readAllOrders[1]),
                        ordTotalCost = Convert.ToDouble(readAllOrders[2]),
                        ordDate = Convert.ToDateTime(readAllOrders[3])
                       
                    });
                }

            }
            catch (SqlException ex)
            {

                throw new Exception(ex.Message);
            }
            finally
            {
                readAllOrders.Close();
                con.Close();
            }
            return ordlist;
        }
        #endregion

        #region Get Order Detail by OrderNo
        public OrdersModel GetOrderDetail(int ordNo)
        {
            SqlCommand cmd_searchByordNo = new SqlCommand("select * from Orders where ordNo=@ordNo", con);
            cmd_searchByordNo.Parameters.AddWithValue("@ordNo", ordNo);
            SqlDataReader read_order = null;
            OrdersModel ordrec = new OrdersModel();
            try
            {
                con.Open();
                read_order = cmd_searchByordNo.ExecuteReader();
                if (read_order.Read())
                {
                    ordrec.ordNo = Convert.ToInt32(read_order[0]);
                    ordrec.ordaccNo = Convert.ToInt32(read_order[1]);
                    ordrec.ordTotalCost = Convert.ToDouble(read_order[2]);
                    ordrec.ordDate = Convert.ToDateTime(read_order[3]);
                    
                }
                else
                {
                    throw new Exception("Order Not Found");
                }
            }
            catch (Exception es)
            {

                throw new Exception(es.Message);
            }
            finally
            {
                read_order.Close();
                con.Close();

            }
            return ordrec;
        }
        #endregion

        #region POST Add New Customer
        public string AddOrder(OrdersModel newOrder)
        {
            SqlCommand cmd_addOrder = new SqlCommand("insert into Orders values(@ordaccNo,@ordTotalCost,@ordDate)", con);
            cmd_addOrder.Parameters.AddWithValue("@ordaccNo", newOrder.ordaccNo);
            cmd_addOrder.Parameters.AddWithValue("@ordTotalCost", newOrder.ordTotalCost);
            cmd_addOrder.Parameters.AddWithValue("@ordDate", newOrder.ordDate);

            try
            {
                con.Open();
                cmd_addOrder.ExecuteNonQuery();
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
        public string DeleteOrder(int ordNo)
        {
            #region Delete Order Table
            SqlCommand cmd_deleteOrder = new SqlCommand("delete from Orders where ordNo=@ordNo", con);
            cmd_deleteOrder.Parameters.AddWithValue("@ordNo", ordNo);
            try
            {
                con.Open();
                cmd_deleteOrder.ExecuteNonQuery();
            }
            catch (Exception es)
            {

                throw new Exception(es.Message);
            }
            finally
            {
                con.Close();
            }
            #endregion

            #region Delete Order Detail Table
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
            #endregion

            return "Order #" + ordNo+ " Was Deleted Successfully!";
        }
        #endregion

        #region UPDATE Order
        public string UpdateOrder(OrdersModel updOrder)
        {
            SqlCommand cmd_updOrder = new SqlCommand("update Orders set ordaccNo=@ordaccNo, ordTotalCost=@ordTotalCost, ordDate=@ordDate", con);
            cmd_updOrder.Parameters.AddWithValue("@ordaccNo", updOrder.ordaccNo);
            cmd_updOrder.Parameters.AddWithValue("@ordTotalCost", updOrder.ordTotalCost);
            cmd_updOrder.Parameters.AddWithValue("@ordDate", updOrder.ordDate);


            try
            {
                con.Open();
                cmd_updOrder.ExecuteNonQuery();

            }
            catch (Exception es)
            {

                throw new Exception(es.Message);
            }
            finally
            {
                con.Close();
            }
            return "Order Information was Updated Successfully.";
        }
        #endregion

    }
}
