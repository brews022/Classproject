using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringProgram.Models;

namespace FlooringProgram.Data
{
    public class OrdersRepo : IManageOrders
    {
        public List<OrderInfo> LoadAll(DateTime fileDate)
        {
            // Loads file based on user input date, which affects the file name.   
            //string dateString = fileDate.ToString("MMddyyyy");
            string dateString = MakeFileName(fileDate);
            string fileName = ("Orders_" + dateString + ".txt");
            bool fileExist = File.Exists(fileName);
            if (fileExist == false)
            {
                return new List<OrderInfo>();
            }

            // Reads and parses the information from the desired file.
            using (StreamReader sr = new StreamReader(fileName))
            {
                List<OrderInfo> newOrders = new List<OrderInfo>();
                
                sr.ReadLine();
                
                while (!sr.EndOfStream)
                {
                    OrderInfo myOrder = new OrderInfo();
                    string[] parts = sr.ReadLine().Split(','); 
                    
                    myOrder.OrderNum = int.Parse(parts[0]);
                    myOrder.CustomerName = parts[1];
                    myOrder.State = new StateInfo();
                    myOrder.State.StateAbbreviation = parts[2];
                    myOrder.State.TaxRate = decimal.Parse(parts[3]);
                    myOrder.Product = new ProductInfo();
                    myOrder.Product.ProductType = parts[4];
                    myOrder.Area = decimal.Parse(parts[5]);
                    myOrder.Product.CostPerSqFoot = decimal.Parse(parts[6]);
                    myOrder.Product.LaborCostPerSqFoot = decimal.Parse(parts[7]);
                    myOrder.MaterialCost = decimal.Parse(parts[8]);
                    myOrder.LaborCost = decimal.Parse(parts[9]);
                    myOrder.TaxAmount = decimal.Parse(parts[10]);
                    myOrder.TotalCost = decimal.Parse(parts[11]);

                    newOrders.Add(myOrder);
                }
                
                return newOrders;
            }
        }

        // Writes information to the file named based on the current date.
        public void SaveAll(DateTime fileDate, List<OrderInfo> orders)
        {
            //string fileName = fileDate.ToString("MMddyyyy");
            string fileName = MakeFileName(fileDate);
            using (StreamWriter sw = new StreamWriter("Orders_" + fileName + ".txt"))
            {
                sw.WriteLine("OrderNumber,CustomerName,State,TaxRate,ProductType,Area,CostPerSquareFoot,LaborCostPerSquareFoot,MaterialCost,LaborCost,Tax,Total");
                foreach (var o in orders)
                {
                    sw.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}",
                        o.OrderNum,
                        o.CustomerName,
                        o.State.StateAbbreviation,
                        o.State.TaxRate,
                        o.Product.ProductType,
                        o.Area,
                        o.Product.CostPerSqFoot,
                        o.Product.LaborCostPerSqFoot,
                        o.MaterialCost,
                        o.LaborCost,
                        o.TaxAmount,
                        o.TotalCost);
                    
                }
            }
        }

        private string MakeFileName(DateTime fileDate)
        {
            string fileName = fileDate.ToString("MMddyyyy");
            return fileName;
        }
    }
      
}
