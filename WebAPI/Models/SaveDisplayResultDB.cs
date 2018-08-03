using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;

namespace WebAPI.Models
{
    public class SaveDisplayResultDB
    {
        string connectionString =
          "Data Source=apsgs-ctl01,17001;Initial Catalog=DEV_SIP_STG1;"
          + "Integrated Security=true";

        public IEnumerable<Displaydata> GetDisplayResult()
        {

            IList<Displaydata> lstreceipe = new List<Displaydata>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetDisplayResults"))
                {                    
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;                   

                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        lstreceipe = dt.DataTableToList<Displaydata>();
                    }                   
                }
            }

            return lstreceipe;
        }

        public int SaveQuery(string query)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO dbo.Recipe_Query_Mohanish(ID,Query)" +
                        "VALUES (@id, @query)", con))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.AddWithValue("@id", 1);
                        cmd.Parameters.AddWithValue("@query", query);
                        con.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }

                return 1;
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}