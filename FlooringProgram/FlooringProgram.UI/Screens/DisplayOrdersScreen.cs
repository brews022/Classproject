using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using FlooringProgram.BLL;
using FlooringProgram.Models;

namespace FlooringProgram.UI.Screens
{
    public class DisplayOrdersScreen : Screen 
    {
        public override void Display()
        {
            // Displays order info based on user input.
            
            DateTime userDate = UserInput.GetDate();

            List<OrderInfo> orderInfosForDate = OrderManager.GetOrders(userDate); 
                 
            if (orderInfosForDate.Count == 0)
            {
                UserInput.Log("There are no orders for that date.");
                Console.ReadLine();
                return;  
            }

            foreach (var o in orderInfosForDate)
            {
                Console.WriteLine("Order Number: {0}", o.OrderNum);
                Console.WriteLine("Customer Name: {0}", o.CustomerName);
                Console.WriteLine("State: {0}", o.State.StateAbbreviation);
                Console.WriteLine("Tax Rate: {0}", o.State.TaxRate);
                Console.WriteLine("Product Type: {0}", o.Product.ProductType);
                Console.WriteLine("Product Area: {0}", o.Area);
                Console.WriteLine("Cost Per Square Foot: {0}", o.Product.CostPerSqFoot);
                Console.WriteLine("Labor Cost Per Square Foot: {0}", o.Product.LaborCostPerSqFoot);
                Console.WriteLine("Material Cost: {0}", o.MaterialCost);
                Console.WriteLine("Labor Cost: {0}", o.LaborCost);
                Console.WriteLine("Tax Amount: {0}", o.TaxAmount);
                Console.WriteLine("Total Cost: {0}", o.TotalCost);
                
                Console.WriteLine();
            }
            Console.ReadLine();

        }
    }
}
         
            
           