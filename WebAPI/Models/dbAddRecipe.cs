using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class dbAddRecipe
    {
        string connectionString =
          "Data Source=apsgs-ctl01,17001;Initial Catalog=DEV_SIP_STG1;"
          + "Integrated Security=true";

        public IEnumerable<AddRecipe> GetReceipeDetails()
        {
            IList<AddRecipe> lstreceipe = new List<AddRecipe>();

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
                        AddRecipe rp = new AddRecipe();
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

        public int SaveReceipeDetails(AddRecipe[] rp)
        {
            // get the ID
            int ID = 0;
            for (int i = 0; i < rp.Length; i++)
            {
                ID = rp[i].RecipeId;
                break;
            }

            // First Delete records then save
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("Delete from dbo.Recipe_Master_Main_Mohanish" +
                        " where ID =  @ID", con))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.AddWithValue("@ID", ID);
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
            // Save 
            try
            {
                foreach (AddRecipe s in rp)
                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("INSERT INTO dbo.Recipe_Master_Main_Mohanish(ID,Specialty,Recipe_Parent,Recipe,Priority,PreLogicalOperator,Attribute,Condition,CodeGroup_Name,PostLogicalOperator, LogicalOperator)" +
                            "VALUES (@ID,@Specialty, @RecipeParent, @Recipe, @Priority,@PreLogicalOperator, @Attribute, @Condition, @Codegroup, @PostLogicalOperator,@OrAnd)", con))
                        {
                            cmd.CommandType = System.Data.CommandType.Text;
                            cmd.Parameters.AddWithValue("@ID", s.RecipeId);
                            cmd.Parameters.AddWithValue("@Specialty", s.Specialty);
                            cmd.Parameters.AddWithValue("@RecipeParent", s.Recipe_Parent);
                            cmd.Parameters.AddWithValue("@Recipe", s.Recipe);
                            cmd.Parameters.AddWithValue("@Priority", Convert.ToInt32(s.Priority));
                            cmd.Parameters.AddWithValue("@PreLogicalOperator", s.PreLogicalOperator);
                            cmd.Parameters.AddWithValue("@Attribute", s.FunctionAttribute.Replace("_", s.Attribute));
                            cmd.Parameters.AddWithValue("@Condition", s.Condition);
                            cmd.Parameters.AddWithValue("@Codegroup", s.Codegroup);
                            cmd.Parameters.AddWithValue("@PostLogicalOperator", s.PostLogicalOperator);
                            cmd.Parameters.AddWithValue("@OrAnd", s.AndOr);
                            con.Open();
                            int rowsAffected = cmd.ExecuteNonQuery();
                            con.Close();
                        }
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
            int ID_Col = 0;

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

    }
}