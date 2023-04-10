using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftProjectWPF.UI
{
    internal class AmountConversion
    {
        public double CupToTsp(double cupAmount)
        {
            //convert cup into tsp
            double tspAmount = 0;

            if (cupAmount > 0)
            {
                tspAmount = cupAmount * 48;            
            }

            return tspAmount;
        }

        public double CupToTbsp(double cupAmount)
        {
            double tbspAmount = 0;

            if (cupAmount > 0)
            {
                tbspAmount = cupAmount * 16;
            }
            return tbspAmount;
        }

        public double CupsToOunces(double cupAmount)
        {
            double ounces = 0;

            if (cupAmount > 0)
            {
                ounces = cupAmount * 8;
            }
            return ounces;
        }
    }
}
