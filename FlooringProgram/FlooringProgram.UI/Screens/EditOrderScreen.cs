using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using FlooringProgram.BLL;
using FlooringProgram.Models;
using FlooringProgram.UI;

namespace FlooringProgram.UI.Screens
{
    public class EditOrderScreen : Screen
    {
        public DateTime UserDate;

        public override void Display()
        {
            //Prompts user for date and order number.

            UserDate = UserInput.GetDate();
            int userOrderNumber = UserInput.GetUserOrderNumber();

            OrderInfo existingOrder = OrderManager.GetOrder(UserDate, userOrderNumber);

            if (existingOrder == null)
            {
                UserInput.Log("That order number doesn't exist.");
                return;
            }

            NewOrderInfo newOrder = new NewOrderInfo();
            newOrder.OrderNum = existingOrder.OrderNum;


            //Prompts user for new information based on stored information.
            Console.WriteLine("Edit order details, press enter to accept current order details: ");
            Console.WriteLine();

            Console.WriteLine("Order Number: {0}", userOrderNumber);
            newOrder.CustomerName = Edit("Name", existingOrder.CustomerName);

            newOrder.Area = EditDecimal("Project Area", existingOrder.Area);
            newOrder.State = EditState("State Abbreviation", existingOrder.State);

            Console.WriteLine("State Name: {0}", newOrder.State.StateName);

            Console.WriteLine("Tax Rate: {0}", newOrder.State.TaxRate);

            newOrder.Product = EditProductType("Product Type", existingOrder.Product);

            Console.WriteLine("Labor cost per square foot: {0}", newOrder.Product.LaborCostPerSqFoot);

            Console.WriteLine("Cost per square foot: {0}", newOrder.Product.CostPerSqFoot);

            bool committed = OrderManager.UpdateOrder(UserDate, existingOrder, newOrder, ConfirmUpdateOrder);

            Console.WriteLine(committed ? "Your changes were saved." : "Your changes were not saved.");
            Console.ReadLine();

        }

        private bool ConfirmUpdateOrder(OrderInfo newOrder)
        {
            bool userChoice = UserInput.PromptAndValidate("Do you want to save these changes?  Y/N");
            return userChoice;
        }
        

        private string Edit(string prompt, string initialValue)
        {
            Console.Write("{0} ({1}): ", prompt, initialValue);
            string input = Console.ReadLine();

            if (String.IsNullOrEmpty(input))
                return initialValue;

            if (input == "NONE")
                return String.Empty;

            return input;
        }

        private decimal? EditDecimal(string prompt, decimal? initialValue)
        {
            decimal? originalValue = initialValue;
            decimal input;
            Console.Write("{0} ({1}): ", prompt, initialValue);
            string inputString = Console.ReadLine();
            bool result = decimal.TryParse(inputString, out input);
            if ( result == false)
            {
                return originalValue;
            }
            
            return input;
        }

        private static StateInfo EditState(string prompt, StateInfo initialValue)
        {
            Console.Write("{0} ({1}): ", prompt, initialValue.StateAbbreviation);
            string userInput = Console.ReadLine();
            
            var repo = FlooringManager.States;
            var list = repo.GetStates();

            var final = (from l in list
                        where l.StateAbbreviation.Equals(userInput, StringComparison.OrdinalIgnoreCase)
                        select l).FirstOrDefault();
            if (final == null)
            {
                return initialValue;
            }

            return final;
        }

        private static ProductInfo EditProductType(string prompt, ProductInfo initialValue)
        {
            Console.Write("{0} ({1}): ", prompt, initialValue.ProductType);
            string userInput = Console.ReadLine();

            var repo = FlooringManager.Products;
            var list = repo.GetProducts();

            var final = (from l in list
                where l.ProductType.Equals(userInput, StringComparison.OrdinalIgnoreCase)
                select l).FirstOrDefault();
            if (final == null)
            {
                return initialValue;
            }
            return final;
        }
    }
}




           