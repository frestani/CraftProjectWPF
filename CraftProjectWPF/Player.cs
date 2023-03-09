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
        public List<Item> Inventory = new List<Item>()
        {
            new Item(){Name = "Water", Description = "Clean water", Quantity = 0.50, Price = 1.00},
            new Item(){Name = "Water", Description = "Clean water", Quantity = 0.50, Price = 1.00},
            new Item(){Name = "Water", Description = "Clean water", Quantity = 0.50, Price = 1.00},
            new Item(){Name = "Water", Description = "Clean water", Quantity = 0.50, Price = 1.00}
        };
        public string GetAllItemsFromInventory()
        {
            string output = "";
            foreach (var item in Inventory)
            {
                output += item.Name + Environment.NewLine;
            }
            return output; 
        }

        public bool SearchInventoryForItem(string name)
        {
            foreach (var item in Inventory)
            {
                // name.ToLower() is passed through th method
                if (item.Name.ToLower() == name.ToLower())
                {
                    return true;
                }
            }
            return false;
        }

        public double SearchInventoryForAmount(string name)
        {
            foreach (var item in Inventory)
            {
                // name.ToLower() is passed through the method
                if (item.Name.ToLower() == name.ToLower())
                {
                    return item.Quantity;
                }
            }
            return 0;
        }

        public bool ChangeItemAmount(string name, double amount)
        {
            foreach (var item in Inventory)
            {
                // name.ToLower() is passed through the method
                if (item.Name.ToLower() == name.ToLower())
                {
                    item.Quantity += amount;
                    return true;
                }
            }
            return false;
        }

        public string ChooseRecipe()
        {
            //call list of recipes
            string output = "";
            foreach (var item in cookbook.Recipes)
            {
                output += item.Name + Environment.NewLine;
                foreach(var ingredient in item.Ingredients)
                {
                    output += "    " + ingredient.Name + Environment.NewLine;
                }
            }
            return output;
        }

        



    }
}
