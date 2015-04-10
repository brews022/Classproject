using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringProgram.Data;



namespace FlooringProgram.BLL
{
    public class FlooringManager
    {
        public static IManageProducts Products;
        public static IManageStates States;
        public static IManageOrders Orders;

        public void Startup()
        {
            // Checks for test vs. prod mode in config file.
            string mode = ConfigurationManager.AppSettings["Mode"];

            Configure(mode == "Test");
        }

        public static void Configure(bool isDev)
        {
            if (isDev)
            {
                Products = new MockProductsRepo();
                States = new MockStatesRepo();
                Orders = new MockOrderRepo();

            }
            else
            {
                Products = new ProductsRepo();
                States = new StatesRepo();
                Orders = new OrdersRepo();
            }
        }
    }
}
