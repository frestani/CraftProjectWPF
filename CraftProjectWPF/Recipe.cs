using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftProjectWPF
{
    internal class Recipe
    {
        //public string Name { get; set; }
        public double yieldAmount { get; set; }
        public double Price { get; set; }
        //public string Description { get; set; }

        //public List<Item> Ingredients = new List<Item>();

        //from PROG 201 class code
        public string Name;
        public string Description;
        public float YieldAmount = 1;
        public string YieldType = "cup(s)";
        public float Value = 0;

        public List<Item> Ingredients = new List<Item>();
    }
}
