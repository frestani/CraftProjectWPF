using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static CraftProjectWPF.UI.ContentUtility;

namespace CraftProjectWPF
{
    internal class Craft
    {
        public List<Recipe> Recipes { get; set; }

        public Craft() 
        {
            Recipes = LoadRecipes("../../../data/recipes.xml");
        }



        //credit to Grace Anders for string type and return strings
        public string CraftRecipe(Player player,Recipe recipe)
        {
            if (CheckIngredients(player ,recipe))
            {
                //2) if all items and right amount are there, subtract amount needed by recipe from player's inventory
                foreach (Item item in recipe.Ingredients)
                {
                    //item is in inventory
                    //distill down into if true or if false
                    player.ChangeItemAmount(item.Name, -item.Quantity);

                    /////////3) MOVE OUT OF FOREACH create a new item of the thing the recipe is supposed to make, then add that to player's inventory (CAN'T HAPPEN IN FOREACH LOOP!!!)
                    //4) let player know it was successful
                    //Message.Text = "You have all of the ingredients!";
                    //returns can break out of loop; don't want one here
                }
                if (player.SearchInventoryForItem(recipe.Name))
                {
                    player.ChangeItemAmount(recipe.Name, recipe.YieldAmount);
                    return $"An additional {recipe.Name} has been added to your inventory.";
                }
                else
                {
                    player.Inventory.Add(new Item() { Name = recipe.Name, Quantity = recipe.YieldAmount, Description = recipe.Description, Price = recipe.Price });
                    return $"{recipe.Name} has been added to your inventory.";
                }
            }
            //let player pick recipe (has to happen elsewhere)
            return "You do not have enough ingredients to make this recipe.";

            //rest can only happen if CanCraft = true

        }

        public bool CheckIngredients(Player player,Recipe recipe)
        {
            //for each ingredient in the recipe, search player inventory and see if they have the item
            // check if they have the right amount
            foreach (Item item in recipe.Ingredients)
            {
                if (player.SearchInventoryForItem(item.Name.ToLower()))
                {
                    //item is in inventory
                    //distill down into if true or if false
                    double amount = player.SearchInventoryForAmount(item.Name.ToLower());
                    if (amount !>= item.Quantity)
                    {
                        //they don't have enough
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
    }
}
