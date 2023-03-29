using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace CraftProjectWPF
{
    internal class Recipe
    {
        //public string Name { get; set; }
        public double yieldAmount { get; set; }
        public double Price { get; set; }
        //public string Description { get; set; }

        //public List<Item> Ingredients = new List<Item>();

        //from PROG 201 class code
        public string Name;
        public string Description;
        public float YieldAmount = 1;
        public string YieldType = "cup(s)";
        public float Value = 0;

        public string LocationPath;
        public BitmapImage RecipeImage = new BitmapImage();

        public List<Item> Ingredients = new List<Item>();

        public BitmapImage ShowBitmapImage()
        {
            RecipeImage.BeginInit();
            RecipeImage.UriSource = new Uri(LocationPath);
            RecipeImage.EndInit();
            return RecipeImage;
        }
    }
}
