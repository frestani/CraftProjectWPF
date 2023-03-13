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

namespace CraftProjectWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Player player = new Player();
        List<Recipe> Recipes = new List<Recipe>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            Inventory.Text = player.GetAllItemsFromInventory();
            Recipes = LoadRecipes("../../../data/recipes.xml");
            Information.Text = GetRecipeListInformation();
            Message.Text = "Enter the number of the recipe you want to craft: ";
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

                    recipeToAdd.Ingredients.Add(new Item() { Name = ingredientName, Amount = ingredientAmount, AmountType = ingredientAmountType, Value = ingredientValue });
                }
                Recipes.Add(recipeToAdd);
            }
            return Recipes;
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

        private void CraftRecipe(Recipe recipe)
        {
            if (CheckIngredients(recipe))
            {
                //2) if all items and right amount are there, subtract amount needed by recipe from player's inventory
                foreach (Item item in recipe.Ingredients)
                {
                    //item is in inventory
                    //distill down into if true or if false
                    player.ChangeItemAmount(item.Name, -item.Quantity);
                    
                    //3) create a new item of the thing the recipe is supposed to make, then add that to player's inventory
                    //4) let player know it was successful
                    //Message.Text = "You have all of the ingredients!";

                }
                if (player.SearchInventoryForItem(recipe.Name))
                {
                    player.ChangeItemAmount(recipe.Name, recipe.yieldAmount);
                 }
                else
                {
                    player.Inventory.Add(new Item() { Name = recipe.Name, Quantity = recipe.yieldAmount, Description = recipe.Description, Price = recipe.Price });
                }
            }
            //let player pick recipe (has to happen elsewhere)
            

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
            //    //LET PLAYER CHOOSE RECIPE!!!
            string playerChoice = Input.Text;
            int recipeIndex = -1;
            try
            {
                recipeIndex += Int32.Parse(Input.Text);
                CraftRecipe(Recipes[recipeIndex-1]);
                Inventory.Text = player.GetAllItemsFromInventory();
            }
            catch
            {

            }
        }
    }
}
