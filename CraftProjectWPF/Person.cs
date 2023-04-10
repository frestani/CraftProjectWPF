using CraftProjectWPF.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftProjectWPF
{
    internal class Person
    {
        //public - convention to capitalize
        public double Currency = 100.00;
        public List<Item> Inventory = new List<Item>();

        AmountConversion conversion = new AmountConversion();

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

        public string GetAllItemsFromInventory()
        {
            string output = "";
            //foreach (var item in Inventory)
            //{
            //    output += item.Name + Environment.NewLine;
            //}
            //return output; 

            //conversion
            for (int i = 0; i < Inventory.Count; i++)
            {
                output += $"{Inventory[i].Name} {Inventory[i].Quantity} {Inventory[i].AmountType}";
                
                if (Inventory[i].AmountType == "cup(s)")
                {
                    output += $" ({conversion.CupToTsp(Inventory[i].Quantity)} tsp) \n";
                }
            }
            return output;
        }

        public string Buy()
        {
            string output = "";
            //foreach (var item in Inventory)
            //{
            //    output += item.Name + Environment.NewLine;
            //}
            //return output; 
            for (int i = 0; i < Inventory.Count; i++)
            {
                output += $"{i + 1}) {Inventory[i].Name}, Price: {Inventory[i].Price.ToString("C")}. Quantity: {Inventory[i].Quantity} \n";
            }
            return output;
        }
        public string Sell()
        {
            string output = "";
            //foreach (var item in Inventory)
            //{
            //    output += item.Name + Environment.NewLine;
            //}
            //return output; 
            for (int i = 0; i < Inventory.Count; i++)
            {
                output += $"{i + 1}) {Inventory[i].Name}, Price: {Inventory[i].Price.ToString("C")}. Quantity: {Inventory[i].Quantity} \n";
            }
            return output;
        }
    }
}
