using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FlooringProgram.BLL
{
    public static class Calculations
    {
        public static decimal GetMaterialCost(decimal area, decimal costPerSquareFoot)
        {
            return Decimal.Round(area*costPerSquareFoot, 2); 
        }

        public static decimal GetLaborCost(decimal area, decimal laborCostPerSquareFoot)
        {
            return Decimal.Round(area*laborCostPerSquareFoot, 2);
        }

        public static decimal GetSubTotal(decimal materialCost, decimal laborCost)
        {
            return Decimal.Round(materialCost + laborCost, 2);
        }

        public static decimal GetTaxAmt(decimal taxRate, decimal subTotal)
        {
            return Decimal.Round(subTotal*(taxRate/100), 2);
        }

        public static decimal GetTotalCost(decimal subTotal, decimal taxAmt)
        {
            return Decimal.Round(subTotal + taxAmt, 2);
        }
    }
}
