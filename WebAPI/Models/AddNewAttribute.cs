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

    }
}