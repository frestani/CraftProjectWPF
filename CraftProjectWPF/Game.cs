using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CraftProjectWPF.UI.ContentUtility;
using static CraftProjectWPF.UI.ApplicationUI;
using System.Windows.Controls;

namespace CraftProjectWPF
{
    internal class Game
    {
        public Player player { get; set; }
        public Vendor vendor { get; set; }

        public Craft craft { get; set; }

        public Game()
        {
            player = new Player();
            vendor = new Vendor();
            craft = new Craft();
        }

        public void GameLoaded(TextBlock Inventory, TextBlock Information, TextBlock Message, TextBlock PlayerCurrency, TextBlock VendorCurrency)
        {
            vendor.Inventory = LoadShopItems("../../../data/shopitems.xml");
            //GetAllItems... returns string, puts together string and returns it. Here, we are
            // showing the player the items
            // 
            Inventory.Text = player.GetAllItemsFromInventory();
            Information.Text = GetRecipeListInformation(craft.Recipes);
            Message.Text = "Enter the number of the recipe you want to craft: ";
            PlayerCurrency.Text = $"{player.Currency.ToString("c")}";
            VendorCurrency.Text = $"{vendor.Currency.ToString("c")}";
        }
    }
}
