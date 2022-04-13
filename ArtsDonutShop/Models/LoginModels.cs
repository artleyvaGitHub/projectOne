using System;
using System.Data.SqlClient;
namespace ArtsDonutShop.Models
{
    public class LoginModels
    {
        public int userId { get; set; }
        public string userName { get; set; }
        public string userPassword { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string jobTitle { get; set; }
        public string userEmail { get; set; }
        public string userStatus { get; set; }

        SqlConnection con = new SqlConnection("server=DESKTOP-6T8MT4L\\MYSQL_ART;database=ArtsDonutShop;integrated security=true");

        #region Get Orders
        public List<LoginModels> GetLoginList()
        {
            SqlCommand cmd_alluser = new SqlCommand("select * from Logintbl", con);
            List<LoginModels> userlist = new List<LoginModels>();
            SqlDataReader readAlluser = null;

            try
            {
                con.Open();
                readAlluser = cmd_alluser.ExecuteReader();

                while (readAlluser.Read())
                {
                    userlist.Add(new LoginModels()
                    {
                        userName = readAlluser[1].ToString(),
                        userPassword = readAlluser[2].ToString(),
                        firstName = readAlluser[3].ToString(),
                        lastName = readAlluser[4].ToString(),
                        jobTitle = readAlluser[5].ToString(),
                        userEmail = readAlluser[6].ToString(),
                        userStatus = readAlluser[7].ToString()
                        
                        
                    }); 
                }

            }
            catch (SqlException ex)
            {

                throw new Exception(ex.Message);
            }
            finally
            {
                readAlluser.Close();
                con.Close();
            }
            return userlist;
        }
        #endregion

        #region POST Add New User
        public string AddNewUser(LoginModels newUser)
        {
            SqlCommand cmd_addLogin = new SqlCommand("insert into Logintbl values(@userName,@userPassword,@firstName,@lastName,@jobTitle,@userEmail,@userStatus)", con);
            cmd_addLogin.Parameters.AddWithValue("@userName", newUser.userName);
            cmd_addLogin.Parameters.AddWithValue("@userPassword", newUser.userPassword);
            cmd_addLogin.Parameters.AddWithValue("@firstName", newUser.firstName);
            cmd_addLogin.Parameters.AddWithValue("@lastName", newUser.lastName);
            cmd_addLogin.Parameters.AddWithValue("@jobTitle", newUser.jobTitle);
            cmd_addLogin.Parameters.AddWithValue("@userEmail", newUser.userEmail);
            cmd_addLogin.Parameters.AddWithValue("@userStatus", newUser.userStatus);

            try
            {
                con.Open();
                cmd_addLogin.ExecuteNonQuery();
            }
            catch (Exception es)
            {

                throw new Exception(es.Message);
            }
            finally
            {
                con.Close();
            }
            return "New User Added Successfully";
        }
        #endregion

    }

}
