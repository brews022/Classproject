using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using  System.Configuration;
using FlooringProgram.Data;
using FlooringProgram.Models;
using FlooringProgram.UI.Screens;
using FlooringProgram.BLL;

namespace FlooringProgram.UI
{
    public class Program
    {
        
        static void Main(string[] args)
        {
            FlooringManager flooringManager = new FlooringManager();
            flooringManager.Startup();

            HomeScreen homeScreen = new HomeScreen();
            homeScreen.Display();

            Console.ReadLine();
        }
    }
}
