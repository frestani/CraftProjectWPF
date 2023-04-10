using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftProjectWPF
{
    class Player : Person
    {
        public Cookbook cookbook = new Cookbook();

        //contrsuctor - no return type
        public Player()
        {
            Inventory = new List<Item>()
        {
            new Item(){Name = "Water", Description = "Clean water", Quantity = 2, Price = 1.00},
            new Item(){Name = "Milk", Description = "Dairy product", Quantity = 2, Price = 3.00},
            new Item(){Name = "Vanilla extract", Description = "Concentrated vanilla", Quantity = 1, Price = 4.00},
            new Item(){Name = "Powdered sugar", Description = "A fine, fluffy form of sugar", Quantity = 2, Price = 5.00}
        };
        }
        

        public string ChooseRecipe()
        {
            //call list of recipes
            string output = "";
            // lowercase cookbook is instance in this program
            // Recipe list defined in Cookbook class
            //foreach (var item in cookbook.Recipes)
            //{
                //output += item.Name + Environment.NewLine;
                //foreach(var ingredient in item.Ingredients)
                //{
                //    output += "    " + ingredient.Name + Environment.NewLine;
                //}
                
            //}
            //return output;

            //credit to Karen Spriggs for creating numbers in front of recipes
            for (int i = 0; i < cookbook.Recipes.Count; i++)
            {
                output += $"{i + 1} {cookbook.Recipes[i].Name} \n";
                foreach (var ingredient in cookbook.Recipes[i].Ingredients)
                {
                    output += "    " + ingredient.Name + Environment.NewLine;

                }
            }
            return output;
        }

        

        



    }
}
