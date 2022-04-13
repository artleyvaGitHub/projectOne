using System;
using System.Data;
using System.Data.SqlClient;
using ArtsDonutShop.Models;

namespace ArtsDonutShop.Models
{
    public class PlaceAnOrderModel
    {
        public int ordNo { get; set; } // self generated
        public int itemNo { get; set; } // from item Details table
        public int qtyOrder { get; set; } // entered by customer
        public decimal ordTotalCost { get; set; } // from Order table
        

        


        SqlConnection con = new SqlConnection("server=DESKTOP-6T8MT4L\\MYSQL_ART;database=ArtsDonutShop;integrated security=true");
        
        #region Place an Order
        public string PlaceAnOrder(int accNo, List<PlaceAnOrderModel> placeAnOrders)
        {

            // Get the order No === self generated
            #region Self generated Order No
            PlaceAnOrderModel newOrdNo = new PlaceAnOrderModel();
            SqlCommand cmd_neworderNo = new SqlCommand("select isnull(Max(ordNo),0)+1 as newOrderNo from Orders", con);
            SqlDataReader _read;
            
            con.Open();
            _read = cmd_neworderNo.ExecuteReader();
            _read.Read();
            //Generated Account - basically increment of 1 to primary key
            newOrdNo.ordNo = Convert.ToInt32(_read[0]);
            con.Close();
            #endregion

            // Add New Orders to the Orders table with the Total Cost of the order
            #region 
            SqlCommand cmd_addOrder = new SqlCommand("insert into Orders VALUES(@accNo,@ordTotalCost,getdate())", con);
            cmd_addOrder.Parameters.AddWithValue("@accNo", accNo);
            cmd_addOrder.Parameters.AddWithValue("@ordTotalCost", ordTotalCost);
            con.Open();
            cmd_addOrder.ExecuteNonQuery();
            con.Close();
            #endregion

            //Add Order No, Customer No and Date Ordered in OrderDtails Table
            #region Add records to OrderDetails Table
            // Add the records to OrderDetails Table 
            foreach (var item in placeAnOrders)
            {
                SqlCommand cmd_PlacetheOrderDetail = new SqlCommand("INSERT INTO OrderDetails VALUES(@ordNo,@itemNo, @qtyOrder)", con);
                con.Open();
                cmd_PlacetheOrderDetail.Parameters.AddWithValue("@ordNo", newOrdNo.ordNo);
                cmd_PlacetheOrderDetail.Parameters.AddWithValue("@itemNo", item.itemNo);
                cmd_PlacetheOrderDetail.Parameters.AddWithValue("@qtyOrder", item.qtyOrder);
                cmd_PlacetheOrderDetail.ExecuteNonQuery();
                con.Close();
            }
            #endregion

            //Calculate the Total Orders to be saved in the Orders table
            #region Get the Total Cost of the Order and ready to save in the Order Table.
            SqlCommand cmd_ordTotalCost = new SqlCommand("select OrderDetails.ordNo, CAST(SUM(OrderDetails.qtyOrder*ItemDetails.itemPrice) as decimal(10,2)) as TotalOrder from OrderDetails JOIN ItemDetails ON OrderDetails.itemNo=ItemDetails.itemNo Where ordNo=@ordNo GROUP BY OrderDetails.ordNo", con);
            con.Open();
            cmd_ordTotalCost.Parameters.AddWithValue("@ordNo", newOrdNo.ordNo);
            _read = cmd_ordTotalCost.ExecuteReader();
            _read.Read();
            ordTotalCost = Convert.ToDecimal(_read[1]);
            con.Close();
            #endregion

            // Update Orders table with the Total Cost of the order
            #region Update Orders table with the Total Cost of the order
            SqlCommand cdm_updOrder = new SqlCommand("update Orders SET ordTotalCost = @ordTotalCost where ordNo=@ordNo", con);
            cdm_updOrder.Parameters.AddWithValue("@ordNo", newOrdNo.ordNo);
            cdm_updOrder.Parameters.AddWithValue("@ordTotalCost", ordTotalCost);
            con.Open();
            cdm_updOrder.ExecuteNonQuery();
            con.Close();
            #endregion

            return "Order #" + newOrdNo.ordNo + " Added New Order";
         #endregion
        
        
        }


    }
}