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
                "SELECT ID as RecipeId, Specialty,Recipe_Parent, Recipe, Priority from dbo.Recipe_Master_Mohanish order by RecipeId";

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
                        rp.Priority = reader[4].ToString();
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

            string recipe = str[0];
            string recipeparent = str[1];
            string specialty = str[2];
            string priority = str[3];


            IList<receipemaster> lstreceipe = new List<receipemaster>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("dbo.Search_Recipes", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@Specialty", specialty == string.Empty ? null : specialty));
                command.Parameters.Add(new SqlParameter("@RecipeParent", recipeparent == string.Empty ? null : recipeparent));
                command.Parameters.Add(new SqlParameter("@Recipe", recipe == string.Empty ? null : recipe));
                command.Parameters.Add(new SqlParameter("@Priority", priority == string.Empty ? null : priority));
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
                        rp.Priority = reader[4].ToString();
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
                        cmd.Parameters.AddWithValue("@Priority", rp.Priority);
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

        public int SaveReceipeDetails(receipemaster[] rp)
        {
            // get the ID
            int ID = 0;
            for (int i = 0; i < rp.Length ; i++ )
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
                foreach (receipemaster s in rp)
                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("INSERT INTO dbo.Recipe_Master_Main_Mohanish(ID,Specialty,Recipe_Parent,Recipe,Priority,PreLogicalOperator,Attribute,Condition,CodeGroup_Name,PostLogicalOperator)" +
                            "VALUES (@ID,@Specialty, @RecipeParent, @Recipe, @Priority,@PreLogicalOperator, @Attribute, @Condition, @Codegroup, @PostLogicalOperator)", con))
                        {
                            cmd.CommandType = System.Data.CommandType.Text;
                            cmd.Parameters.AddWithValue("@ID", s.RecipeId);
                            cmd.Parameters.AddWithValue("@Specialty", s.Specialty);
                            cmd.Parameters.AddWithValue("@RecipeParent", s.Recipe_Parent);
                            cmd.Parameters.AddWithValue("@Recipe", s.Recipe);
                            cmd.Parameters.AddWithValue("@Priority", Convert.ToInt32(s.Priority));
                            cmd.Parameters.AddWithValue("@PreLogicalOperator", s.PreLogicalOperator);
                            cmd.Parameters.AddWithValue("@Attribute", s.Attribute);
                            cmd.Parameters.AddWithValue("@Condition", s.Condition);
                            cmd.Parameters.AddWithValue("@Codegroup", s.Codegroup);
                            cmd.Parameters.AddWithValue("@PostLogicalOperator", s.PostLogicalOperator);
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
        public int UpdateReceipeMaster(receipemaster rp)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("UPDATE dbo.Recipe_Master_Mohanish SET Specialty= @Specialty, Recipe_Parent = @RecipeParent, Recipe = @Recipe, Priority= @Priority WHERE ID = @RecipeId", con))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.AddWithValue("@RecipeId", rp.RecipeId);
                        cmd.Parameters.AddWithValue("@Specialty", rp.Specialty);
                        cmd.Parameters.AddWithValue("@RecipeParent", rp.Recipe_Parent);
                        cmd.Parameters.AddWithValue("@Recipe", rp.Recipe);
                        cmd.Parameters.AddWithValue("@Priority", rp.Priority);

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

    public class attributedbcalls
    {
        string connectionString =
         "Data Source=apsgs-ctl01,17001;Initial Catalog=DEV_SIP_STG1;"
         + "Integrated Security=true";

        public IEnumerable<string> GetTablesName()
        {

            IList<string> lsttblnames = new List<string>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("dbo.Get_TableNames", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        lsttblnames.Add(reader[0].ToString());
                    }

                    reader.Close();
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return lsttblnames;
        }

        public IEnumerable<ColName> GetColNames(string tableName, int ID)
        {
            IList<ColName> lstcolnames = new List<ColName>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command;

                if (ID == -1)
                {
                    command = new SqlCommand("dbo.Get_ColumnNameAsTable", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@tablename", tableName == string.Empty ? "GIRFT_NCIP_RnD_BaseComponent" : tableName));
                }
                else
                {
                    command = new SqlCommand("dbo.Get_ColumnValue", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@tablename", tableName == string.Empty ? "GIRFT_NCIP_RnD_BaseComponent" : tableName));
                    command.Parameters.Add(new SqlParameter("@id", ID));
                }
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (ID == -1)
                    {
                        while (reader.Read())
                        {
                            ColName cn = new ColName();
                            cn.ID = Convert.ToInt32(reader[0]);
                            cn.Name = reader[1].ToString();
                            lstcolnames.Add(cn);
                        }

                        reader.Close();
                    }
                    else
                    {
                        int i = 1;
                        while (reader.Read())
                        {
                            ColName cn = new ColName();
                            cn.ID = i;
                            cn.Name = reader[0].ToString();
                            lstcolnames.Add(cn);
                            i++;
                        }

                        reader.Close();
                    }
                    
                }
                catch (Exception)
                {

                    throw;
                }
            }

            return lstcolnames;
        }
    }
}