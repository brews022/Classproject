using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using FlooringProgram.BLL;
using FlooringProgram.Data;
using FlooringProgram.Models;
using FlooringProgram.UI.Screens;

namespace FlooringProgram.UI
{
    public static class UserInput
    {
        // Converts user input from AddOrderScreen to decimal.
        public static decimal GetDecimal(string prompt)
        {
            while (true)
            {
                decimal numDecimal;
                Console.Write(prompt);
                string userInput = Console.ReadLine();

                if (Decimal.TryParse(userInput, out numDecimal))
                {
                    return numDecimal;
                }
                
                Log("That is not valid input.");
            }
        }

        // Verifies that string input from AddOrderScreen isn't null.
        public static string GetString(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string userInput = Console.ReadLine();
                if (userInput != "")
                {
                    return userInput;
                }

                Log("That is not valid input.");
            }
            
        }

        // Checks user input for desired product type against the mock producs repo.  
        //Returns product info if possible.
        public static ProductInfo GetProductType(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string userInput = Console.ReadLine();
                var repo = FlooringManager.Products;
                var list = repo.GetProducts();
                var final = (from l in list
                             where l.ProductType.Equals(userInput, StringComparison.OrdinalIgnoreCase)
                             select l).SingleOrDefault();
                if (final != null)
                {
                    ProductInfo myProduct = final;                   
                    return myProduct;
                }                Log("Sorry!  We don't carry that product.");
            }            
        }

        public static StateInfo GetState(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string userInput = Console.ReadLine();
                var repo = FlooringManager.States;
                var list = repo.GetStates();
                var final = (from l in list
                             where l.StateAbbreviation.Equals(userInput, StringComparison.OrdinalIgnoreCase)
                             select l).SingleOrDefault();
                if (final != null)
                {
                    StateInfo userState = final;
                    return userState;
                }
                Log("Sorry!  We do not have information for that state.");
            }    
        }


        public static DateTime GetDate()
        {
            while (true)
            {
                DateTime userDate;
                Console.Write("Please enter the order date (MM/DD/YYYY): ");
                string userDateString = Console.ReadLine();
                bool success = DateTime.TryParse(userDateString, out userDate);
                if (success)
                {
                    return userDate;
                }
                Log("Sorry, that is not a valid date.");     
            }
     
        }

        // Prompts user for the desired order number and validates the response.
        public static int GetUserOrderNumber()
        {
            while (true)
            {
                int userOrderNumber;
                Console.Write("Please enter the order number: ");
                string userOrderNumberString = Console.ReadLine();
                bool validOrderNumber = Int32.TryParse(userOrderNumberString, out userOrderNumber);
                if (validOrderNumber)
                {
                    return userOrderNumber;
                }
                Log("That is not a valid order number.");
            }
        }

        //Prompts the user for whether they want something done and validates response.
        public static bool PromptAndValidate(string prompt)
        {
            while (true)
            {
                
                Console.Write(prompt);
                string userResponse = Console.ReadLine();

                if (userResponse.Equals("Y", StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
                if (userResponse.Equals("N", StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }

                Log("That is not a valid response.");
            }
        }

        public static void Log(string errorMessage)
        {
            using (StreamWriter w = File.AppendText("log.txt"))
            {
                w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(), errorMessage);
                Console.WriteLine(errorMessage);
                
            }

        }
            
    }
}
