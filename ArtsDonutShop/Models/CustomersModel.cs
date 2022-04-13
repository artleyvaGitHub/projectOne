using System;
using System.Data.SqlClient;
namespace ArtsDonutShop.Models
{
    public class CustomersModel
    {
        #region Customers Properties
        public int accNo { get; set; }
        public string accCustomerName { get; set; }
        public string accAddress { get; set; }
        public string accCity { get; set; }
        public string accState { get; set; }
        public string accPhone { get; set; }
        public string accUserid { get; set; }
        public string accPassword { get; set; }
        public string accStatus { get; set; }
        #endregion

        SqlConnection con = new SqlConnection("server=DESKTOP-6T8MT4L\\MYSQL_ART;database=ArtsDonutShop;integrated security=true");

        #region Get Customers
        public List<CustomersModel> GetCustomers()
        {
            SqlCommand cmd_allcustomers = new SqlCommand("select * from Customers", con);
            List<CustomersModel> custlist = new List<CustomersModel>();
            SqlDataReader readAllCustomers = null;

            try
            {
                con.Open();
                readAllCustomers = cmd_allcustomers.ExecuteReader();

                while (readAllCustomers.Read())
                {
                    custlist.Add(new CustomersModel()
                    {
                        accNo = Convert.ToInt32(readAllCustomers[0]),
                        accCustomerName = readAllCustomers[1].ToString(),
                        accAddress = readAllCustomers[2].ToString(),
                        accCity = readAllCustomers[3].ToString(),
                        accState = readAllCustomers[4].ToString(),
                        accPhone = readAllCustomers[5].ToString(),
                        accUserid = readAllCustomers[6].ToString(),
                        accPassword = readAllCustomers[7].ToString(),
                    });
                }

            }
            catch (SqlException ex)
            {

                throw new Exception(ex.Message);
            }
            finally
            {
                readAllCustomers.Close();
                con.Close();    
            }
            return custlist;
        }
        #endregion

        #region Get Customer Detail by ID
        public CustomersModel GetCustomerDetail(int accNo)
        {
            SqlCommand cmd_searchByaccNo = new SqlCommand("select * from Customers where accNo=@accNo", con);
            cmd_searchByaccNo.Parameters.AddWithValue("@accNo", accNo);
            SqlDataReader read_customer = null;
            CustomersModel custrec = new CustomersModel();
            try
            {
                con.Open();
                read_customer = cmd_searchByaccNo.ExecuteReader();
                if (read_customer.Read())
                {
                    custrec.accNo = Convert.ToInt32(read_customer[0]);
                    custrec.accCustomerName = read_customer[1].ToString();
                    custrec.accAddress = read_customer[2].ToString();
                    custrec.accCity = read_customer[3].ToString();
                    custrec.accState = read_customer[4].ToString();
                    custrec.accPhone = read_customer[5].ToString();
                    custrec.accUserid = read_customer[6].ToString();
                    custrec.accPassword = read_customer[7].ToString();
                }
                else
                {
                    throw new Exception("Customer Number Not Found.");
                }
            }
            catch (Exception es)
            {

                throw new Exception(es.Message);
            }
            finally 
            {
                read_customer.Close();
                con.Close();

            }
            return custrec;
        }
        #endregion

        #region POST Add New Customer
        public string AddCustomer(CustomersModel newCustomer)
        {
            SqlCommand cmd_addCustomer = new SqlCommand("insert into Customers values(@accCustomerName,@accAddress,@accCity,@accState,@accPhone,@accUserid,@accPassword,@accStatus)",con);
            cmd_addCustomer.Parameters.AddWithValue("@accCustomerName",newCustomer.accCustomerName);
            cmd_addCustomer.Parameters.AddWithValue("@accAddress", newCustomer.accAddress);
            cmd_addCustomer.Parameters.AddWithValue("@accCity", newCustomer.accCity);
            cmd_addCustomer.Parameters.AddWithValue("@accState", newCustomer.accState);
            cmd_addCustomer.Parameters.AddWithValue("@accPhone", newCustomer.accPhone);
            cmd_addCustomer.Parameters.AddWithValue("@accUserid", newCustomer.accUserid);
            cmd_addCustomer.Parameters.AddWithValue("@accPassword", newCustomer.accPassword);
            cmd_addCustomer.Parameters.AddWithValue("@accStatus", newCustomer.accStatus);
            try
            {
                con.Open();
                cmd_addCustomer.ExecuteNonQuery();
            }
            catch (Exception es)
            {

                throw new Exception(es.Message);
            }
            finally
            {
                con.Close();
            }
            return "New Customer Information Added Successfully";
        }
        #endregion

        #region DELETE Customer
        public string DeleteCustomer(int accNo)
        {
            SqlCommand cmd_deleteCustomer = new SqlCommand("delete from Customers where accNo=@accNo", con);
            cmd_deleteCustomer.Parameters.AddWithValue("@accNo", accNo);
            try
            {
                con.Open();
                cmd_deleteCustomer.ExecuteNonQuery();
            }
            catch (Exception es)
            {

                throw new Exception(es.Message);
            }
            finally
            {
                con.Close();
            }
            return "Customer #" + accNo + " Was Deleted Successfully!";
        }
        #endregion

        #region UPDATE Customer
        public string UpdateCustomer(CustomersModel updCustomer)
        {
            SqlCommand cmd_updCustomer = new SqlCommand("update Customers set accName=@accName, accAddress=@accAddress, accCity=@accCity, accState=@accState, accPhone=@accPhone, accUserid=@accUserid, accPassword=@accPassword, accStatus=@accStatus", con);
            cmd_updCustomer.Parameters.AddWithValue("@accName", updCustomer.accCustomerName);
            cmd_updCustomer.Parameters.AddWithValue("@accAddress", updCustomer.accAddress);
            cmd_updCustomer.Parameters.AddWithValue("@accCity", updCustomer.accCity);
            cmd_updCustomer.Parameters.AddWithValue("@accState", updCustomer.accState);
            cmd_updCustomer.Parameters.AddWithValue("@accPhone", updCustomer.accPhone);
            cmd_updCustomer.Parameters.AddWithValue("@accUserid", updCustomer.accUserid);
            cmd_updCustomer.Parameters.AddWithValue("@accPassword", updCustomer.accPassword);
            cmd_updCustomer.Parameters.AddWithValue("@accStatus", updCustomer.accStatus);

            try
            {
                con.Open();
                cmd_updCustomer.ExecuteNonQuery();

            }
            catch (Exception es)
            {

                throw new Exception(es.Message);
            }
            finally
            {
                con.Close();
            }
            return "Customer #" + accNo + " Information was Updated Successfully.";
        }
        #endregion

        //#region GET Customer List Hard Data ===for my reference only
        //public List<CustomersModel> GetCustomersList()
        //{
        //    List<CustomersModel> customers = new List<CustomersModel>();
        //    customers.Add(new CustomersModel() { accNo = 1002, accCustomerName = "Josephine Leyva", accAddress = "33 Mateo Ave", accCity = "Millbrae", accState = "CA", accPhone = "6501234567", accUserid = "cherryjo", accPassword = "password" });
        //    customers.Add(new CustomersModel() { accNo = 1003, accCustomerName = "Yuan Leyva", accAddress = "2535 Bantry Lane", accCity = "San Francisco", accState = "CA", accPhone = "5101234567", accUserid = "yuan", accPassword = "password" });
        //    customers.Add(new CustomersModel() { accNo = 1004, accCustomerName = "George Smith", accAddress = "123 Main Ave", accCity = "Daly City", accState = "CA", accPhone = "6509876543", accUserid = "george", accPassword = "password" });
        //    customers.Add(new CustomersModel() { accNo = 1005, accCustomerName = "Jane Doe", accAddress = "456 Penny Lane", accCity = "Sunnyvale", accState = "CA", accPhone = "6501234568", accUserid = "jane", accPassword = "password" });
        //    customers.Add(new CustomersModel() { accNo = 1006, accCustomerName = "Virginia Leyva", accAddress = "77 R. De Jesus", accCity = "Palo Alto", accState = "CA", accPhone = "6502345678", accUserid = "jean", accPassword = "password" });
        //    return customers;
        //}
        //#endregion


    }
}
