using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CraftProjectWPF
{
    internal class Cookbook
    {
        public List<Recipe> Recipes = new List<Recipe>()
        {
            new Recipe(){Name = "Chocolate Cake", Ingredients = new List<Item>()
            {
            new Item(){Name = "Powdered sugar icing", Quantity = 1},
            new Item(){Name = "Banana", Quantity = 2},
            new Item(){Name = "Egg", Quantity = 2},
            new Item(){Name = "Cacao powder", Quantity = 2}
            }, yieldAmount = 1},

            new Recipe(){Name = "Powdered Sugar Icing", Ingredients = new List<Item>()
            {
              new Item(){Name = "Powdered sugar", Quantity= 1},
              new Item(){Name = "Vanilla extract", Quantity = 0.5},
              new Item(){Name = "Milk", Quantity = 2}
            },
            yieldAmount = 1
            }
        };
        
    }
}
