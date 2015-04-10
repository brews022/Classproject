using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringProgram.Models
{
    public class NewOrderInfo
    {
        public string CustomerName { get; set; }
        public StateInfo State { get; set; }
        public int OrderNum { get; set; }
        public ProductInfo Product { get; set; }
        public decimal? Area { get; set; }      


    }
}
