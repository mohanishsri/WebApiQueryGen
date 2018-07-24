using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class receipemaster
    {
        public int RecipeId { get; set; }
        public string Specialty { get; set; }
        public string Recipe_Parent { get; set; }
        public string Recipe { get; set; }
        public string Priority { get; set; }
        public string PreLogicalOperator { get; set; }
        public string Attribute { get; set; }
        public string Condition { get; set; }
        public string Codegroup { get; set; }
        public string PostLogicalOperator { get; set; }
    }

    public class recipecolumn
    {
        public string colname { get; set; }
        public string colvalue { get; set; }
    }
}