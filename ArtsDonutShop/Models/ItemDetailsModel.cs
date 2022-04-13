using System;
using System.Data.SqlClient;
namespace ArtsDonutShop.Models
{
    public class ItemDetailsModel
    {

        #region Item Details Properties
        public int itemNo { get; set; }
        public double itemPrice { get; set; }

        public string itemDescription { get; set; }

        #endregion

        SqlConnection con = new SqlConnection("server=DESKTOP-6T8MT4L\\MYSQL_ART;database=ArtsDonutShop;integrated security=true");

        #region Get Items
        public List<ItemDetailsModel> GetItems()
        {
            SqlCommand cmd_allitems = new SqlCommand("select * from ItemDetails", con);
            List<ItemDetailsModel> itemlist = new List<ItemDetailsModel>();
            SqlDataReader readAllItems = null;

            try
            {
                con.Open();
                readAllItems = cmd_allitems.ExecuteReader();

                while (readAllItems.Read())
                {
                    itemlist.Add(new ItemDetailsModel()
                    {
                        itemNo = Convert.ToInt32(readAllItems[0]),
                        itemPrice = Convert.ToDouble(readAllItems[1]),
                        itemDescription = readAllItems[2].ToString(),

                    });
                }

            }
            catch (SqlException ex)
            {

                throw new Exception(ex.Message);
            }
            finally
            {
                readAllItems.Close();
                con.Close();
            }
            return itemlist;
        }
        #endregion

        #region Get Item Detail by Item No
        public ItemDetailsModel GetItemDetail(int itemNo)
        {
            SqlCommand cmd_searchByitemNo = new SqlCommand("select * from ItemDetails where itemNo=@itemNo", con);
            cmd_searchByitemNo.Parameters.AddWithValue("@itemNo", itemNo);
            SqlDataReader read_item = null;
            ItemDetailsModel itemrec = new ItemDetailsModel();
            try
            {
                con.Open();
                read_item = cmd_searchByitemNo.ExecuteReader();
                if (read_item.Read())
                {
                    itemrec.itemNo = Convert.ToInt32(read_item[0]);
                    itemrec.itemPrice = Convert.ToDouble(read_item[1]);
                    itemrec.itemDescription = read_item[2].ToString();

                }
                else
                {
                    throw new Exception("Item Not Found");
                }
            }
            catch (Exception es)
            {

                throw new Exception(es.Message);
            }
            finally
            {
                read_item.Close();
                con.Close();

            }
            return itemrec;
        }
        #endregion

        #region POST Add New Item
        public string AddItem(ItemDetailsModel newItems)
        {
            SqlCommand cmd_addItem = new SqlCommand("insert into ItemDetails values(@itemPrice,@itemDescription)", con);
            //cmd_addItem.Parameters.AddWithValue("@itemNo", newItems.itemNo);
            cmd_addItem.Parameters.AddWithValue("@itemPrice", newItems.itemPrice);
            cmd_addItem.Parameters.AddWithValue("@itemDescription", newItems.itemDescription);

            try
            {
                con.Open();
                cmd_addItem.ExecuteNonQuery();
            }
            catch (Exception es)
            {

                throw new Exception(es.Message);
            }
            finally
            {
                con.Close();
            }
            return "New Item Added Successfully";
        }
        #endregion

        #region DELETE Item
        public string DeleteItem(int itemNo)
        {
            SqlCommand cmd_deleteItem = new SqlCommand("delete from ItemDetails where itemNo=@itemNo", con);
            cmd_deleteItem.Parameters.AddWithValue("@itemNo", itemNo);
            try
            {
                con.Open();
                cmd_deleteItem.ExecuteNonQuery();
            }
            catch (Exception es)
            {

                throw new Exception(es.Message);
            }
            finally
            {
                con.Close();
            }
            return "Item Was Deleted Successfully!";
        }
        #endregion

        #region UPDATE Item
        public string UpdateItem(ItemDetailsModel updItem)
        {
            SqlCommand cmd_updItem = new SqlCommand("update ItemDetails set itemNo=@itemNo, itemPrice=@itemPrice,itemDescription=@itemDescription", con);
            cmd_updItem.Parameters.AddWithValue("@itemNo", updItem.itemNo);
            cmd_updItem.Parameters.AddWithValue("@itemPrice", updItem.itemPrice);
            cmd_updItem.Parameters.AddWithValue("@itemDescription", updItem.itemDescription);


            try
            {
                con.Open();
                cmd_updItem.ExecuteNonQuery();

            }
            catch (Exception es)
            {

                throw new Exception(es.Message);
            }
            finally
            {
                con.Close();
            }
            return "Item Information was Updated Successfully.";
        }
        #endregion

    }
}
