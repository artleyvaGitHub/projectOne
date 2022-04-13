using System;
using System.Data.SqlClient;

namespace ArtsDonutShop.Models
{
    public class CustomerOrderedByOrderNo
    {
        public int accNo { get; set; }
        public string accCustomerName { get; set; }
        public int ordNo { get; set; }
        public string ordDate { get; set; }
        public int itemNo { get; set; }
        public int qtyOrder { get; set; }
        public string itemDescription { get; set; }
        public double itemPrice { get; set; }

        SqlConnection con = new SqlConnection("server=DESKTOP-6T8MT4L\\MYSQL_ART;database=ArtsDonutShop;integrated security=true");
        #region Get Customer Ordered by Order No
        public List<CustomerOrderedByOrderNo> GetCustomerOrderedDetails(int ordNo)
        {
            SqlCommand cmd_customerorderedbyOrderNo = new SqlCommand("select Customers.accNo,Customers.accCustomerName, Orders.ordNo, Orders.ordDate,OrderDetails.itemNo, OrderDetails.qtyOrder, ItemDetails.itemDescription ,ItemDetails.itemPrice,(OrderDetails.qtyOrder * ItemDetails.itemPrice) as TotalOrder from Customers JOIN Orders ON Orders.ordaccNo = Customers.accNo JOIN OrderDetails ON Orders.ordNo = OrderDetails.ordNo JOIN ItemDetails ON ItemDetails.itemNo = OrderDetails.itemNo WHERE OrderDetails.ordNo = @ordNo", con);
            cmd_customerorderedbyOrderNo.Parameters.AddWithValue("@ordNo", ordNo);
            List<CustomerOrderedByOrderNo> custordlist = new List<CustomerOrderedByOrderNo>();
            SqlDataReader readAllCustomerOrdersbyOrderNo = null;

            try
            {
                con.Open();
                readAllCustomerOrdersbyOrderNo = cmd_customerorderedbyOrderNo.ExecuteReader();

                while (readAllCustomerOrdersbyOrderNo.Read())
                {
                    custordlist.Add(new CustomerOrderedByOrderNo()
                    {
                        accNo = Convert.ToInt32(readAllCustomerOrdersbyOrderNo[0]),
                        accCustomerName = readAllCustomerOrdersbyOrderNo[1].ToString(),
                        ordNo = Convert.ToInt32(readAllCustomerOrdersbyOrderNo[2]),
                        ordDate = readAllCustomerOrdersbyOrderNo[3].ToString(),
                        itemNo = Convert.ToInt32(readAllCustomerOrdersbyOrderNo[4]),
                        qtyOrder = Convert.ToInt32(readAllCustomerOrdersbyOrderNo[5]),
                        itemDescription = readAllCustomerOrdersbyOrderNo[6].ToString(),
                        itemPrice = Convert.ToDouble(readAllCustomerOrdersbyOrderNo[7])

                    });
                }

            }
            catch (SqlException ex)
            {

                throw new Exception(ex.Message);
            }
            finally
            {
                readAllCustomerOrdersbyOrderNo.Close();
                con.Close();
                
            }
            return custordlist;
        }
        #endregion
    }
}
