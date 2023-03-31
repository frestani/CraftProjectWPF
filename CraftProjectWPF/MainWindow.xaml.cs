using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Xml;
using System.Security.Cryptography.X509Certificates;
using static CraftProjectWPF.UI.ApplicationUI;

namespace CraftProjectWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public enum ApplicationMode
    {
        Craft, Buy, Sell, Display
    }
    public partial class MainWindow : Window
    {
        Game game;
        ApplicationMode mode = ApplicationMode.Craft;
        public MainWindow()
        {
            InitializeComponent();
            game = new Game();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            game.GameLoaded(Inventory, Information, Message, PlayerCurrency, VendorCurrency);
        }


        private void btn_Submit_Click(object sender, RoutedEventArgs e)
        {
            //credit to Grace Anders for switch statement
            switch (mode)
            {
                case ApplicationMode.Craft:
                    CraftSubmit(Input, Inventory, Message, game.craft.Recipes, game.craft, game.player);
                    break;
                case ApplicationMode.Buy:
                    BuySubmit(PlayerCurrency, VendorCurrency, Inventory, Information, Input, game.player, game.vendor);
                    break;
                case ApplicationMode.Sell:
                    SellSubmit(PlayerCurrency, VendorCurrency, Inventory, Information, Input, game.player, game.vendor);
                    break;
                case ApplicationMode.Display:
                    DisplaySubmit(Input, Inventory, Image, game.craft.Recipes);
                    break;
            }
           
        }

        private void btn_Craft_Click(object sender, RoutedEventArgs e)
        {
            mode = ApplicationMode.Craft;
            Mode.Text = "Craft Mode";
            Information.Text = GetRecipeListInformation(game.craft.Recipes);
            Inventory.Text = game.player.GetAllItemsFromInventory();
            Message.Text = "Enter the number of the recipe you want to craft: ";
        }
        private void btn_Buy_Click(object sender, RoutedEventArgs e)
        {
            mode = ApplicationMode.Buy;
            Mode.Text = "Buy Mode";
            Information.Text = game.vendor.Buy();
            Inventory.Text = game.player.Buy();
            Message.Text = "Enter the number of the item you want to buy: ";
        }

        private void btn_Sell_Click(object sender, RoutedEventArgs e)
        {
            mode = ApplicationMode.Sell;
            Mode.Text = "Sell Mode";
            Information.Text = game.vendor.Sell();
            Inventory.Text = game.player.Sell();
            Message.Text = "Enter the number of the item you want to sell: ";
        }

        private void btn_Display_Click(object sender, RoutedEventArgs e)
        {
            mode = ApplicationMode.Display;
            Mode.Text = "Display Mode";
            Information.Text = GetRecipeListInformation(game.craft.Recipes);
            Inventory.Text = "Type in the number of the recipe you want to look up: ";
        }
    }
}
