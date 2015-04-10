using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using FlooringProgram.Models;


namespace FlooringProgram.BLL
{
    public static class OrderManager
    {
        public static bool CreateOrder(NewOrderInfo info, Func<OrderInfo, bool> toConfirm)
        {
            OrderInfo myOrder = new OrderInfo();
            myOrder.CustomerName = info.CustomerName;
            myOrder.Area = info.Area;
            myOrder.Product = info.Product;
            myOrder.State = info.State;

            // Stores results of order calculations based on user input. 
            myOrder.MaterialCost = Calculations.GetMaterialCost(myOrder.Area.Value, myOrder.Product.CostPerSqFoot);
            myOrder.LaborCost = Calculations.GetLaborCost(myOrder.Area.Value, myOrder.Product.LaborCostPerSqFoot);
            myOrder.SubTotal = Calculations.GetSubTotal(myOrder.MaterialCost, myOrder.LaborCost);
            myOrder.TaxAmount = Calculations.GetTaxAmt(myOrder.State.TaxRate, myOrder.SubTotal);
            myOrder.TotalCost = Calculations.GetTotalCost(myOrder.SubTotal, myOrder.TaxAmount);

            List<OrderInfo> orders = FlooringManager.Orders.LoadAll(DateTime.Today);

            // If the orders list is empty, makeOrderNum will be 1.  Otherwise, it's the highest existing one plus one.
            var makeOrderNum = orders.Any() ? orders.Max(o => o.OrderNum) + 1 : 1;
            
            myOrder.OrderNum = makeOrderNum;

            bool wasConfirmed = toConfirm(myOrder);

            if (wasConfirmed)
            {
                orders.Add(myOrder);
                FlooringManager.Orders.SaveAll(DateTime.Today, orders);
            }

            return wasConfirmed;
        }

        public static List<OrderInfo> GetOrders(DateTime fileDate)
        {
            List<OrderInfo> orderInfosForDate = FlooringManager.Orders.LoadAll(fileDate);
            return orderInfosForDate;
        }

        public static OrderInfo GetOrder(DateTime fileDate, int orderNum)
        {
            List<OrderInfo> currentOrders = FlooringManager.Orders.LoadAll(fileDate);

            //Searches for desired file and either displays it or indicates that the order doesn't exist.
            var existingOrder = currentOrders.FirstOrDefault(o => o.OrderNum == orderNum);
            return existingOrder;
        }

        public static bool UpdateOrder(DateTime fileDate, OrderInfo existingOrder, NewOrderInfo edits, Func<OrderInfo, bool> toConfirm)
        {
            //Re-runs methods to update price information based on any changes made to the user order.
            OrderInfo newOrder = new OrderInfo();

            newOrder.OrderNum = edits.OrderNum;
            newOrder.CustomerName = edits.CustomerName;
            newOrder.Area = edits.Area;
            newOrder.Product = edits.Product;
            newOrder.State = edits.State;

            newOrder.MaterialCost = Calculations.GetMaterialCost(newOrder.Area.Value, newOrder.Product.CostPerSqFoot);
            newOrder.LaborCost = Calculations.GetLaborCost(newOrder.Area.Value, newOrder.Product.LaborCostPerSqFoot);
            newOrder.SubTotal = Calculations.GetSubTotal(newOrder.MaterialCost, newOrder.LaborCost);
            newOrder.TaxAmount = Calculations.GetTaxAmt(newOrder.State.TaxRate, newOrder.SubTotal);
            newOrder.TotalCost = Calculations.GetTotalCost(newOrder.SubTotal, newOrder.TaxAmount);

            bool wasConfirmed = toConfirm(newOrder);
            List<OrderInfo> currentOrders = FlooringManager.Orders.LoadAll(fileDate);

            if (wasConfirmed)
            {
                currentOrders.Remove(existingOrder);
                currentOrders.Add(newOrder);
                FlooringManager.Orders.SaveAll(fileDate, currentOrders);
            }

            return wasConfirmed;

        }

        public static bool DeleteOrder(DateTime fileDate, OrderInfo order, Func<OrderInfo, bool> toConfirm)
        {
            List<OrderInfo> orders = FlooringManager.Orders.LoadAll(fileDate);

            bool wasConfirmed = toConfirm(order);

            if (wasConfirmed)
            {
                orders.Remove(order);
                FlooringManager.Orders.SaveAll(fileDate, orders);
            }
            return wasConfirmed;
        }


    }
}
