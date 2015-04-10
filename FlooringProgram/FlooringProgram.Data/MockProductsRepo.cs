using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using FlooringProgram.Models;

namespace FlooringProgram.Data
{
    public class MockProductsRepo : IManageProducts
    {
        
        // Hard-coded information for test mode.
        public List<ProductInfo> GetProducts()
        {
            ProductInfo carpet = new ProductInfo();
            carpet.ProductType = "carpet";
            carpet.CostPerSqFoot = 3M;
            carpet.LaborCostPerSqFoot = 3M; 

            ProductInfo tile = new ProductInfo();
            tile.ProductType = "tile";
            tile.CostPerSqFoot = 4M;
            tile.LaborCostPerSqFoot = 4M;

            ProductInfo wood = new ProductInfo();
            wood.ProductType = "wood";
            wood.CostPerSqFoot = 5M;
            wood.LaborCostPerSqFoot = 5M;

            List<ProductInfo> productList = new List<ProductInfo>();
            productList.Add(carpet);
            productList.Add(tile);
            productList.Add(wood);

            return productList;
        }
        
        


       
    }
}


     