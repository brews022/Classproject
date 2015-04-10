using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringProgram.Models
{

    public class OrderInfo 
    {
        public string CustomerName { get; set; }
        public StateInfo State { get; set; }     
        public int OrderNum { get; set; }
        public ProductInfo Product { get; set; }        
        public decimal? Area { get; set; }      
        public decimal TotalCost { get; set; }  
        public decimal TaxAmount { get; set; }          
        public decimal MaterialCost { get; set; } 
        public decimal LaborCost { get; set; }
        public decimal SubTotal { get; set; }

        public OrderInfo()
        {
            this.Product = new ProductInfo();
            this.State = new StateInfo();
        }
    }
}
