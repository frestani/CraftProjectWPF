using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CraftProjectWPF.UI
{
    internal static class ContentUtility
    {
        //credit to Alex Gartner
        static string ImagePath(string file) => $"/../images/{file}";

        //credit to PROG 201 class code
        public static List<Recipe> LoadRecipes(string fileName)
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
                { recipeToAdd.Price = value; }

                //creates image with ImagePath()
                recipeToAdd.LocationPath = ImagePath(recipe.GetAttribute("imagePath"));

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

        public static List<Item> LoadShopItems(string fileName)
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
    }
}
