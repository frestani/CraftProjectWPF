using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftProjectWPF
{
    internal class Recipe
    {
        public string Name { get; set; }
        public List<Item> Ingredients = new List<Item>();
        public double yieldAmount { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
    }
}
