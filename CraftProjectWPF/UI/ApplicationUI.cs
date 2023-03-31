using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CraftProjectWPF.UI
{
    internal static class ApplicationUI
    {

        //credit to PROG 201 class code
        public static string GetRecipeListInformation(List<Recipe> Recipes)
        {
            string output = "Recipes:\n";
            int i = 0;
            foreach (Recipe recipe in Recipes)
            {
                output += $"{i + 1} {recipe.Name}\n";
                foreach (var ingredient in recipe.Ingredients)
                {
                    output += "    " + ingredient.Name + Environment.NewLine;
                }
                i++;
            }

            return output;
        }
        public static string GetShopItemListInformation(Vendor vendor)
        {
            string output = "Shop Items:\n";
            int i = 0;
            foreach (Item shopItem in vendor.Inventory)
            {
                output += $"{i + 1} {shopItem.Name}, cost: {shopItem.Price}\n";

                i++;
            }

            return output;
        }




        public static void CraftSubmit(TextBox Input, TextBlock Inventory, TextBlock Message, List<Recipe> Recipes, Craft craft, Player player)
        {
            //lets player choose recipe
            //credit to Karen Spriggs for indexing
            string playerChoice = Input.Text;
            int recipeIndex = -1;
            try
            {
                //convert to int, craft recipe, display inventory in Inventory text block
                recipeIndex += Int32.Parse(Input.Text);
                Message.Text = craft.CraftRecipe(player, Recipes[recipeIndex]);
                Inventory.Text = player.GetAllItemsFromInventory();
            }
            catch
            {

            }
        }

        public static void BuySubmit(TextBlock PlayerCurrency, TextBlock VendorCurrency, TextBlock Inventory, TextBlock Information, TextBox Input, Player player, Vendor vendor)
        {
            string playerChoice = Input.Text;
            int itemIndex = -1;
            int amount = 1;
            itemIndex += Int32.Parse(Input.Text);
            if (player.Currency < vendor.Inventory[itemIndex].Price)
            {
                //print message saying player doesn't have enough money
            }
            if (vendor.SearchInventoryForAmount(vendor.Inventory[itemIndex].Name) >= amount)
            {
                vendor.Inventory[itemIndex].Quantity -= amount;
                player.Inventory.Add(vendor.Inventory[itemIndex]);
                player.Currency -= vendor.Inventory[itemIndex].Price;
                vendor.Currency += vendor.Inventory[itemIndex].Price;
                PlayerCurrency.Text = $"{player.Currency.ToString("c")}";
                VendorCurrency.Text = $"{vendor.Currency.ToString("c")}";
            }
            else
            {

            }
            Inventory.Text = player.Buy();
            Information.Text = vendor.Buy();
        }

        public static void SellSubmit(TextBlock PlayerCurrency, TextBlock VendorCurrency, TextBlock Inventory, TextBlock Information, TextBox Input, Player player, Vendor vendor)
        {
            int itemIndex = -1;
            int amount = 1;
            itemIndex += Int32.Parse(Input.Text);

            //credit to Sebastian Pedersen for profit margin and pricing system
            Random random = new Random();
            int itemRarityIndex = random.Next(1, 11);
            double profitMargin = 0;
            string rarity = "";

            if (itemRarityIndex <= 7)
            {
                profitMargin = 10;
                rarity = "common";
            }
            else if (itemRarityIndex > 7 && itemRarityIndex < 10)
            {
                profitMargin = 15;
                rarity = "uncommon";
            }
            else
            {
                profitMargin = 20;
                rarity = "rare";
            }
            double percentToAdd = Math.Round((profitMargin / 100) * player.Inventory[itemIndex].Price, 2);
            double newPrice = player.Inventory[itemIndex].Price + percentToAdd;

            if (vendor.Currency < newPrice)
            {
                //print message saying vendor doesn't have enough currency
            }
            //find item in inventory, see if they have more than 0
            //if true, minus 1 from amount
            if (player.SearchInventoryForAmount(player.Inventory[itemIndex].Name) >= amount)
            {
                //check - is the vendor's currency greater than or equal to the price of that item
                player.Inventory[itemIndex].Quantity -= amount;
                //add item to vendor's inventory
                vendor.Inventory.Add(player.Inventory[itemIndex]);
                //minus from vendor's currency the cost of the item
                vendor.Currency -= newPrice;
                //add cost of item to player's currency
                player.Currency += newPrice;
                PlayerCurrency.Text = $"{player.Currency.ToString("c")}";
                VendorCurrency.Text = $"{vendor.Currency.ToString("c")}";
                MessageBox.Show($"You made a profit of {percentToAdd.ToString("c")}.\n You sold a {rarity} {(player.Inventory[itemIndex].Name)}.");
                // "You made a profit of (newPrice - player.Inventory[itemIndex].Price)
                // "You sold a (rarity) item!
            }
            else
            {
                //handle if amount of item is 0
                //print message saying player doesn't have enough items
                //Message.Text = "You do have enough items.";

            }
            Information.Text = player.Sell();
            Inventory.Text = vendor.Sell();
        }

        public static void DisplaySubmit(TextBox Input, TextBlock Inventory, Image Image, List<Recipe> Recipes)
        {
            string playerChoice = Input.Text;
            int recipeIndex = -1;
            try
            {
                //convert to int
                recipeIndex += Int32.Parse(Input.Text);
                Inventory.Text = $"{Recipes[recipeIndex].Name}\n{Recipes[recipeIndex].Description}\n";
                Image.Source = Recipes[recipeIndex].ShowBitmapImage();
            }
            catch
            {

            }
        }
    }
}
