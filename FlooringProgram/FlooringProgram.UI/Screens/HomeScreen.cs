using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringProgram.Data;
using FlooringProgram.Models;

namespace FlooringProgram.UI.Screens
{
    public class HomeScreen : Screen
    {
        //Creates opening display menu and sends user to the page they select.
        public override void Display()
        {

            Console.WriteLine("Main Menu");
            Console.WriteLine("1. Display Orders");
            Console.WriteLine("2. Add an Order");
            Console.WriteLine("3. Edit an Order");
            Console.WriteLine("4. Remove an Order");
            Console.WriteLine("5. Quit");

            Screen nextStep = GetNextStep();
            if (nextStep != null)
            {
                this.GoTo(nextStep);
            }
        }

        private Screen GetNextStep()
        {
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.KeyChar)
                {
                    case '1':
                        return new DisplayOrdersScreen();
                    case '2':
                        return new AddOrderScreen();
                    case '3':
                        return new EditOrderScreen();
                    case '4':
                        return new RemoveOrderScreen();
                    case '5':
                        Environment.Exit(0);
                        break;

                }
            }
        }
    }
}
