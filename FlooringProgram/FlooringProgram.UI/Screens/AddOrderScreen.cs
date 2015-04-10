using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringProgram.Models;
using FlooringProgram.Data;
using FlooringProgram.BLL;


namespace FlooringProgram.UI.Screens
{
    public class AddOrderScreen : Screen
    {
        public override void Display()
        {
            //Prompt user for new order information, verifying with methods on UserInput screen, and assigning them to variables.
         
            NewOrderInfo myNewOrder = new NewOrderInfo();
            Console.WriteLine("Please enter the following information");
 
            myNewOrder.CustomerName = UserInput.GetString("Please enter your name: ");

            myNewOrder.Area = UserInput.GetDecimal("Area of project in square feet: ");

            myNewOrder.Product = UserInput.GetProductType("Product type: ");
            
            myNewOrder.State = UserInput.GetState("State (abbreviation): ");
            
            bool committed = OrderManager.CreateOrder(myNewOrder, ConfirmAddOrder);
            
            Console.WriteLine(committed ? "Your order was saved." : "Your order was not saved.");
            Console.ReadLine();
           
        }

        private bool ConfirmAddOrder(OrderInfo myOrder)
        {
            //Prints out order summary
            Console.WriteLine("Order Number: {0}", myOrder.OrderNum);
            Console.WriteLine("Customer Name: {0}", myOrder.CustomerName);
            Console.WriteLine("State: {0}", myOrder.State.StateAbbreviation);
            Console.WriteLine("Tax Rate: {0}", myOrder.State.TaxRate);
            Console.WriteLine("Product Type: {0}", myOrder.Product.ProductType);
            Console.WriteLine("Project Area: {0}", myOrder.Area);
            Console.WriteLine("Cost Per Square Foot: {0}", myOrder.Product.CostPerSqFoot);
            Console.WriteLine("Labor Cost Per Square Foot: {0}", myOrder.Product.LaborCostPerSqFoot);
            Console.WriteLine("Material cost: {0}", myOrder.MaterialCost);
            Console.WriteLine("Labor Cost: {0}", myOrder.LaborCost);
            Console.WriteLine("Tax Amount: {0}", myOrder.TaxAmount);
            Console.WriteLine("Order Total: {0}", myOrder.TotalCost);

            //Asks the user if he/she wants to save the order.
            bool userChoice = UserInput.PromptAndValidate("Do you want to save this order Y/N? ");
            return userChoice;
        }
        
    }
}

