using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;

namespace WebAPI.Models
{
    public class AddNewAttribute
    {
        string connectionString =
         "Data Source=apsgs-ctl01,17001;Initial Catalog=DEV_SIP_STG1;"
         + "Integrated Security=true";

        public IEnumerable<attributecolval> GetColumnValues(string colname)
        {
            IList<attributecolval> lstreceipe = new List<attributecolval>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetColumnValues"))
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@colname", colname);

                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        lstreceipe = dt.DataTableToList<attributecolval>();
                    }
                }
            }

            return lstreceipe;
        }

        public IEnumerable<attributecolval> GetColumnValues()
        {
            string colname = "Diag_01_Derived";

            IList<attributecolval> lstreceipe = new List<attributecolval>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetColumnValues"))
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@colname", colname);

                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        lstreceipe = dt.DataTableToList<attributecolval>();
                    }
                }
            }

            return lstreceipe;
        }

        public int SaveAttributeValue(attributecolval[] value, string colname)
        {   

            string[] strcolnattr = colname.Split('|');

            string selectedvalue = string.Empty;

            foreach (attributecolval s in value)
            {
                if(selectedvalue == string.Empty)
                {
                    selectedvalue = s.itemName;
                }
                else
                {
                    selectedvalue = selectedvalue + ',' + s.itemName + ',';
                }
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("dbo.SaveAttrbute", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@colname", strcolnattr[0]));
                command.Parameters.Add(new SqlParameter("@attributealias", strcolnattr[1]));
                command.Parameters.Add(new SqlParameter("@selectedvalue", selectedvalue));
                
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close(); 
                }
                catch (Exception)
                {

                    throw;
                }
            } 
           
            return 1;
        }

    }
}