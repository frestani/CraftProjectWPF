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

namespace CraftProjectWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Cookbook cookbook = new Cookbook();
        Player player = new Player();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            Inventory.Text = player.GetAllItemsFromInventory();
        }

        private void CraftRecipe(Recipe recipe)
        {
            bool CanCraft = true;
            //let player pick recipe (has to happen elsewhere)
            //1) for each ingredient in the recipe, search player inventory and see if they have the item
            // check if they have the right amount
            foreach (Item item in recipe.Ingredients)
            {
                if (player.SearchInventoryForItem(item.Name.ToLower()))
                {
                    //item is in inventory
                    //distill down into if true or if false
                    double amount = player.SearchInventoryForAmount(item.Name.ToLower());
                    if (amount >= item.Quantity)
                    {
                        //they have the right amount, equal or greater
                    }
                    else
                    {
                        //they don't have enough
                        CanCraft = false;
                    }
                }
                else
                {
                    CanCraft = false;
                }
            }
            //rest can only happen if CanCraft = true
            //2) if all items and right amount are there, subtract amount needed by recipe from player's inventory
            //3) create a new item of the thing the recipe is supposed to make, then add that to player's inventory
            //4) let player know it was successful
        }
    }
}
