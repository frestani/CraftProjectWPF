using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftProjectWPF
{
    internal class Item
    {
        //public string Name { get; set; }
        public string Description { get; set; }

        public double Quantity { get; set; }
        public double Price { get; set; }

        //credit to PROG 201 class code
        public string Name;
        //public double Amount = 0;
        public string AmountType = "cup(s)";
    }
}
