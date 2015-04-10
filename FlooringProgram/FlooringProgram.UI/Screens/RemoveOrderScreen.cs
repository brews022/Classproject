using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using FlooringProgram.BLL;
using FlooringProgram.Models;

namespace FlooringProgram.UI.Screens
{
    public class RemoveOrderScreen : Screen
    {

        public DateTime UserDate;
        public override void Display()
        {
            //Prompts user for order number and date of desired order.  

            UserDate = UserInput.GetDate();
            int userOrderNumber = UserInput.GetUserOrderNumber();

            //Determines if the order exists.
            var userOrder = OrderManager.GetOrder(UserDate, userOrderNumber);
            if (userOrder == null)
            {
                UserInput.Log("That order number doesn't exist.");
                return;
            }


            bool committed = OrderManager.DeleteOrder(UserDate, userOrder, ConfirmDeleteOrder);

            Console.WriteLine(committed ? "Your order was deleted." : "Your order was not deleted.");
            Console.ReadLine();
        }

        private bool ConfirmDeleteOrder(OrderInfo userOrder)
        {
                //Displays order
            Console.WriteLine("Order Number: {0}", userOrder.OrderNum);
            Console.WriteLine("Customer Name: {0}", userOrder.CustomerName);
            Console.WriteLine("State: {0}", userOrder.State.StateAbbreviation);
            Console.WriteLine("Tax Rate: {0}", userOrder.State.TaxRate);
            Console.WriteLine("Product Type: {0}", userOrder.Product.ProductType);
            Console.WriteLine("Product Area: {0}", userOrder.Area);
            Console.WriteLine("Cost Per Square Foot: {0}", userOrder.Product.CostPerSqFoot);
            Console.WriteLine("Labor Cost Per Square Foot: {0}", userOrder.Product.LaborCostPerSqFoot);
            Console.WriteLine("Material Cost: {0}", userOrder.MaterialCost);
            Console.WriteLine("Labor Cost: {0}", userOrder.LaborCost);
            Console.WriteLine("Tax Amount: {0}", userOrder.TaxAmount);
            Console.WriteLine("Total Cost: {0}", userOrder.TotalCost);

            bool userResponse = UserInput.PromptAndValidate("Are you sure you want to delete this order? Y/N");
            return userResponse;
            
        }
            //Prompts the user about whether they want to delete the displayed order and responds accordingly.
            
    }       
    
}

