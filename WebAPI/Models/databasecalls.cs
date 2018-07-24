using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class databasecalls
    {
        string connectionString =
          "Data Source=apsgs-ctl01,17001;Initial Catalog=DEV_SIP_STG1;"
          + "Integrated Security=true";

        public IEnumerable<receipemaster> GetReceipeMaster()
        {
            IList<receipemaster> lstreceipe = new List<receipemaster>();

            // Provide the query string with a parameter placeholder.
            string queryString =
                "SELECT ID as RecipeId, Specialty,Recipe_Parent, Recipe from dbo.Recipe_Master_Mohanish order by RecipeId";

            // Create and open the connection in a using block. This
            // ensures that all resources will be closed and disposed
            // when the code exits.
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                // Create the Command and Parameter objects.
                SqlCommand command = new SqlCommand(queryString, connection);

                // Open the connection in a try/catch block. 
                // Create and execute the DataReader, writing the result
                // set to the console window.
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        receipemaster rp = new receipemaster();
                        rp.RecipeId = Convert.ToInt32(reader[0]);
                        rp.Specialty = reader[1].ToString();
                        rp.Recipe_Parent = reader[2].ToString();
                        rp.Recipe = reader[3].ToString();
                        lstreceipe.Add(rp);
                    }

                    reader.Close();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return lstreceipe;
        }

        public IEnumerable<receipemaster> SearchReceipe(string searchvalues)
        {
            string[] str = searchvalues.Split('|');

            string specialty = str[0];
            string recipeparent = str[1];
            string recipe = str[2];


            IList<receipemaster> lstreceipe = new List<receipemaster>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("dbo.Search_Recipes", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@Specialty", specialty == string.Empty ? null : specialty));
                command.Parameters.Add(new SqlParameter("@RecipeParent", recipeparent == string.Empty ? null : recipeparent));
                command.Parameters.Add(new SqlParameter("@Recipe", recipe == string.Empty ? null : recipe));                
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        receipemaster rp = new receipemaster();
                        rp.RecipeId = Convert.ToInt32(reader[0]);
                        rp.Specialty = reader[1].ToString();
                        rp.Recipe_Parent = reader[2].ToString();
                        rp.Recipe = reader[3].ToString();
                        lstreceipe.Add(rp);
                    }

                    reader.Close();
                }
                catch (Exception)
                {
                    
                    throw;
                }
            }
            return lstreceipe;
        }

        public int SaveReceipeMaster(receipemaster rp)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO dbo.Recipe_Master_Mohanish (ID,Specialty,Recipe_Parent,Recipe,Priority,PreLogicalOperator,Attribute,Condition,CodeGroup_Name,PostLogicalOperator)" +
                        "VALUES (@ID,@Specialty, @RecipeParent, @Recipe, @Priority,@PreLogicalOperator, @Attribute, @Condition, @Codegroup, @PostLogicalOperator)", con))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.AddWithValue("@ID", getIdFromTable());
                        cmd.Parameters.AddWithValue("@Specialty", rp.Specialty);
                        cmd.Parameters.AddWithValue("@RecipeParent", rp.Recipe_Parent);
                        cmd.Parameters.AddWithValue("@Recipe", rp.Recipe);
                        cmd.Parameters.AddWithValue("@Priority", 0);
                        cmd.Parameters.AddWithValue("@PreLogicalOperator", null);
                        cmd.Parameters.AddWithValue("@Attribute", null);
                        cmd.Parameters.AddWithValue("@Condition", null);
                        cmd.Parameters.AddWithValue("@Codegroup", null);
                        cmd.Parameters.AddWithValue("@PostLogicalOperator", null);
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

        private int getIdFromTable()
        {
            int ID_Col=0;

            // Provide the query string with a parameter placeholder.
            string queryString =
                "SELECT max(ID)+1 as colid from dbo.Recipe_Master_Mohanish";

            // Create and open the connection in a using block. This
            // ensures that all resources will be closed and disposed
            // when the code exits.
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                // Create the Command and Parameter objects.
                SqlCommand command = new SqlCommand(queryString, connection);

                // Open the connection in a try/catch block. 
                // Create and execute the DataReader, writing the result
                // set to the console window.
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        ID_Col = Convert.ToInt32(reader[0]);
                    }

                    reader.Close();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return ID_Col;
        }
        public int UpdateReceipeMaster(receipemaster rp)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("UPDATE dbo.Recipe_Master_Mohanish SET Specialty= @Specialty, Recipe_Parent = @RecipeParent, Recipe = @Recipe WHERE ID = @RecipeId", con))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.AddWithValue("@RecipeId", rp.RecipeId);
                        cmd.Parameters.AddWithValue("@Specialty", rp.Specialty);
                        cmd.Parameters.AddWithValue("@RecipeParent", rp.Recipe_Parent);
                        cmd.Parameters.AddWithValue("@Recipe", rp.Recipe);
                        
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

        public int deleteReceipeMaster(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("DELETE FROM dbo.Recipe_Master_Mohanish WHERE ID = @RecipeId", con))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.AddWithValue("@RecipeId", id);
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