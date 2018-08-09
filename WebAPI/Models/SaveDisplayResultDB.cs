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

        public IEnumerable<Displaydata> GetDisplayResult(int id)
        {

            IList<Displaydata> lstreceipe = new List<Displaydata>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetDisplayResults"))
                {                    
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", id);

                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        lstreceipe = dt.DataTableToList<Displaydata>();
                    }                   
                }
            }

            return lstreceipe.ToArray();
        }

        public int SaveQuery(string query, int Id)
        {
            // First Delete records then save
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("Delete from dbo.Recipe_Query_Mohanish" +
                        " where ID =  @ID", con))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.AddWithValue("@ID", Id);
                        con.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO dbo.Recipe_Query_Mohanish(ID,Query)" +
                        "VALUES (@id, @query)", con))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.AddWithValue("@id", Id);
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