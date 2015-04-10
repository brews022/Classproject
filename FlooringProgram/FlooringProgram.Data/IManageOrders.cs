using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using FlooringProgram.Models;


namespace FlooringProgram.Data
{
    public interface IManageOrders
    {
        List<OrderInfo> LoadAll(DateTime fileDate);
        void SaveAll(DateTime fileDate, List<OrderInfo> orders);   
        
    }
}
