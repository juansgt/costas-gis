using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utils.Algorithms
{
    public static class Maths
    {
        public static double StockEnShockRound(double price)
        {

            int priceInt = Convert.ToInt32(price);
            priceInt = priceInt > price ? priceInt - 1 : priceInt;
            double priceAndHalf = priceInt + 0.50;
            double finalPrice;

            if (price == priceInt)
            {
                finalPrice = price;
            }
            else
            {
                if ((price > priceInt) && (price <= priceAndHalf))
                {
                    finalPrice = priceAndHalf;
                }
                else
                {
                    finalPrice = priceInt + 1;
                }
            }

            return Math.Round(finalPrice, 2); 
        }
    }
}
