using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using FlooringProgram.Models;

namespace FlooringProgram.Data
{
    public class ProductsRepo : IManageProducts
    {
        public List<ProductInfo> GetProducts()
        {
            List<ProductInfo> loadProductInfo = new List<ProductInfo>();

            using (StreamReader sr = new StreamReader("Products.txt"))
            {
                bool foundHeader = false;
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    if (!foundHeader)
                    {
                        foundHeader = true;
                        continue;
                    }

                    string[] parts = line.Split(',');
                    ProductInfo product = new ProductInfo();
                    product.ProductType = parts[0];
                    product.CostPerSqFoot = decimal.Parse(parts[1]);
                    product.LaborCostPerSqFoot = decimal.Parse(parts[2]);

                    loadProductInfo.Add(product);

                }
                return loadProductInfo;
            }
        }
    }
}
