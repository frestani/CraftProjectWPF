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

namespace CraftProjectWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public enum ApplicationMode
    {
        Craft, Buy, Sell
    }
    public partial class MainWindow : Window
    {
        Player player = new Player();
        Vendor vendor = new Vendor();
        List<Recipe> Recipes = new List<Recipe>();
        ApplicationMode mode = ApplicationMode.Craft;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            vendor.Inventory = LoadShopItems("../../../data/shopitems.xml");
            //GetAllItems... returns string, puts together string and returns it. Here, we are
            // showing the player the items
            // 
            Inventory.Text = player.GetAllItemsFromInventory();
            Recipes = LoadRecipes("../../../data/recipes.xml");
            Information.Text = GetRecipeListInformation();
            Message.Text = "Enter the number of the recipe you want to craft: ";
            PlayerCurrency.Text = $"{player.Currency}";
            VendorCurrency.Text = $"{vendor.Currency}";
        }

        //credit to PROG 201 class code
        private List<Recipe> LoadRecipes(string fileName)
        {
            List<Recipe> Recipes = new List<Recipe>();
            XmlDocument doc = new XmlDocument();
            doc.Load(fileName);
            XmlNode root = doc.DocumentElement;
            XmlNodeList recipeList = root.SelectNodes("/recipes/recipe");
            XmlNodeList ingredientsList;

            foreach (XmlElement recipe in recipeList)
            {
                Recipe recipeToAdd = new Recipe();
                recipeToAdd.Name = recipe.GetAttribute("title");
                recipeToAdd.Description = recipe.GetAttribute("description");
                string yieldAmount = recipe.GetAttribute("yieldAmount");
                if (float.TryParse(yieldAmount, out float amount))
                { recipeToAdd.YieldAmount = amount; }

                recipeToAdd.YieldType = recipe.GetAttribute("yieldType");
                string recipevalue = recipe.GetAttribute("value");
                if (float.TryParse(recipevalue, out float value))
                { recipeToAdd.Value = value; }

                ingredientsList = recipe.ChildNodes; //for ingredients

                foreach (XmlElement i in ingredientsList)
                {
                    string ingredientName = i.GetAttribute("itemName");
                    string ingredientAmountString = i.GetAttribute("amount");
                    float ingredientAmount = 0;
                    if (float.TryParse(ingredientAmountString, out float e))
                    { ingredientAmount = e; }
                    string ingredientAmountType = i.GetAttribute("amountType");
                    string tempIngredientValue = i.GetAttribute("value");
                    float ingredientValue = 0;
                    if (float.TryParse(tempIngredientValue, out float ingValue))
                    { ingredientValue = ingValue; }

                    recipeToAdd.Ingredients.Add(new Item() { Name = ingredientName, Quantity = ingredientAmount, AmountType = ingredientAmountType, Price = ingredientValue });
                }
                Recipes.Add(recipeToAdd);
            }
            return Recipes;
        }

        private List<Item> LoadShopItems(string fileName)
        {
            List<Item> ShopItems = new List<Item>();
            XmlDocument doc = new XmlDocument();
            doc.Load(fileName);
            XmlNode root = doc.DocumentElement;
            XmlNodeList shopItemList = root.SelectNodes("/shopItems/shopItem");
            //XmlNodeList ingredientsList;

            foreach (XmlElement shopItem in shopItemList)
            {
                Item shopItemToAdd = new Item();
                shopItemToAdd.Name = shopItem.GetAttribute("itemName");
                string amountCost = shopItem.GetAttribute("amount");
                if (float.TryParse(amountCost, out float amount))
                { shopItemToAdd.Quantity = amount; }

                string Cost = shopItem.GetAttribute("cost");
                if (float.TryParse(Cost, out float cost))
                { shopItemToAdd.Price = cost; }
                ShopItems.Add(shopItemToAdd);
            }
            return ShopItems;
        }

        //credit to PROG 201 class code
        private string GetRecipeListInformation()
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
        private string GetShopItemListInformation()
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

        //credit to Grace Anders for string type and return strings
        private string CraftRecipe(Recipe recipe)
        {
            if (CheckIngredients(recipe))
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
                    player.ChangeItemAmount(recipe.Name, recipe.yieldAmount);
                    return $"An additional {recipe.Name} has been added to your inventory.";
                 }
                else
                {
                    player.Inventory.Add(new Item() { Name = recipe.Name, Quantity = recipe.yieldAmount, Description = recipe.Description, Price = recipe.Price });
                    return $"{recipe.Name} has been added to your inventory.";
                }
            }
            //let player pick recipe (has to happen elsewhere)
            return "You do not have enough ingredients to make this recipe.";

            //rest can only happen if CanCraft = true

        }

        private bool CheckIngredients(Recipe recipe)
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
                    if (amount >= item.Quantity)
                    {
                        //they have the right amount, equal or greater
                        Message.Text = "You have the correct amount of ingredients!";
                    }
                    else
                    {
                        //they don't have enough
                        Message.Text = "You do not have enough ingredients.";
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

        private void btn_Submit_Click(object sender, RoutedEventArgs e)
        {
            //credit to Grace Anders for switch statement
            switch (mode)
            {
                case ApplicationMode.Craft:
                    CraftSubmit();
                    break;
                case ApplicationMode.Buy:
                    BuySubmit();
                    break;
                case ApplicationMode.Sell:
                    SellSubmit();
                    break;
            }
           
        }
        public void CraftSubmit()
        {
            //lets player choose recipe
            //credit to Karen Spriggs for indexing
            string playerChoice = Input.Text;
            int recipeIndex = -1;
            try
            {
                //convert to int, craft recipe, display inventory in Inventory text block
                recipeIndex += Int32.Parse(Input.Text);
                Message.Text = CraftRecipe(Recipes[recipeIndex]);
                Inventory.Text = player.GetAllItemsFromInventory();
            }
            catch
            {

            }
        }

        public void BuySubmit()
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
                PlayerCurrency.Text = $"{player.Currency}";
                VendorCurrency.Text = $"{vendor.Currency}";
            }
            else
            {

            }
            Inventory.Text = player.Buy();
            Information.Text = vendor.Buy();
        }

        private void SellSubmit()
        {
            string playerChoice = Input.Text;
            int itemIndex = -1;
            int amount = 1;
            itemIndex += Int32.Parse(Input.Text);
            if (vendor.Currency < player.Inventory[itemIndex].Price)
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
                    vendor.Currency -= player.Inventory[itemIndex].Price;
                    //add cost of item to player's currency
                    player.Currency += player.Inventory[itemIndex].Price;
                PlayerCurrency.Text = $"{player.Currency}";
                VendorCurrency.Text = $"{vendor.Currency}";
            }
                else
                {
                //handle if amount of item is 0
                //print message saying player doesn't have enough items
                Message.Text = "You do have enough items.";
                    
                }
                Information.Text = player.Sell();
            Inventory.Text = vendor.Sell();
        }
        private void btn_Craft_Click(object sender, RoutedEventArgs e)
        {
            mode = ApplicationMode.Craft;
            Mode.Text = "Craft Mode";
            Information.Text = GetRecipeListInformation();
        }
        private void btn_Buy_Click(object sender, RoutedEventArgs e)
        {
            mode = ApplicationMode.Buy;
            Mode.Text = "Buy Mode";
            Information.Text = player.Buy();
            Inventory.Text = vendor.Buy();
            Message.Text = "Enter the number of the item you want to buy: ";
        }

        private void btn_Sell_Click(object sender, RoutedEventArgs e)
        {
            mode = ApplicationMode.Sell;
            Mode.Text = "Sell Mode";
            Information.Text = player.Sell();
            Message.Text = "Enter the number of the item you want to sell: ";
        }
    }
}
