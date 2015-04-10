using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringProgram.Models;

namespace FlooringProgram.Data
{

    public class MockOrderRepo : IManageOrders
    {
        // Sets up a dictionary to hold the information in test mode.
        public Dictionary<DateTime, List<OrderInfo>> SavedOrders = new Dictionary<DateTime, List<OrderInfo>>();
        
        public List<OrderInfo> LoadAll(DateTime fileDate)
        {
            if (SavedOrders.ContainsKey(fileDate.Date))
            {
                return SavedOrders[fileDate.Date];
            }
            else
            {
                return new List<OrderInfo>();
            }
             
        }

        public void SaveAll(DateTime fileDate, List<OrderInfo> orders)
        {
            
            SavedOrders[fileDate.Date] = orders;  
        }
    }
}


    