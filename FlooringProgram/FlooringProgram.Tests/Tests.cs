using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using FlooringProgram.BLL;
using FlooringProgram.Data;
using FlooringProgram.Models;
using FlooringProgram.UI;



namespace FlooringProgram.Tests
{
    [TestFixture]
    public class Tests
    {
        //Calculations calc = new Calculations();

        [TestCase(5, 4, 20)]
        [TestCase(2, 5.5, 11)]
        public void GetMaterialCostTest(decimal area, decimal costPerSquareFoot, decimal expected)
        {
            decimal result = Calculations.GetMaterialCost(area, costPerSquareFoot);
            Assert.AreEqual(expected, result);
        }

        [TestCase(5, 2.2, 11)]
        [TestCase(2, 2.5, 5)]
        public void GetLaborCostTest(decimal area, decimal laborCostPerSquareFoot, decimal expected)
        {
            decimal result = Calculations.GetLaborCost(area, laborCostPerSquareFoot);
            Assert.AreEqual(expected, result);
        }

        [TestCase(5, 4, 2.2, 31)]
        [TestCase(2, 5.5, 2.5, 16)]
        public void GetSubTotalTest(decimal area, decimal costPerSquareFoot, decimal laborCostPerSquareFoot, decimal expected)
        {
            decimal materialCost = Calculations.GetMaterialCost(area, costPerSquareFoot);
            decimal laborCost = Calculations.GetLaborCost(area, laborCostPerSquareFoot);
            decimal result = Calculations.GetSubTotal(materialCost, laborCost);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Test()
        {
            FlooringManager.Configure(true);
            MockOrderRepo orderRepo = FlooringManager.Orders as MockOrderRepo;
            Assert.AreEqual(0, orderRepo.SavedOrders.Count);

            Func<OrderInfo, bool> mockFunction = (order) =>
            {
                Assert.AreEqual(order.CustomerName, "Test Name");
                return true;
            };
            NewOrderInfo newOrder = new NewOrderInfo();
            newOrder.CustomerName = "Test Name";
            newOrder.Area = 0;

            ProductInfo myProduct = new ProductInfo();
            myProduct.ProductType = "carpet";
            myProduct.CostPerSqFoot = 0;
            myProduct.LaborCostPerSqFoot = 0;
            newOrder.Product = myProduct;

            StateInfo myState = new StateInfo();
            myState.StateAbbreviation = "MN";
            myState.StateName = "Minnesota";
            myState.TaxRate = 0;

            newOrder.State = myState;
            
            OrderManager.CreateOrder(newOrder, mockFunction);

            Assert.AreEqual(1, orderRepo.SavedOrders.Count);
            Assert.IsTrue(orderRepo.SavedOrders.ContainsKey(DateTime.Today));

            var todaysOrders = orderRepo.SavedOrders[DateTime.Today];
            Assert.AreEqual(1, todaysOrders.Count);
            Assert.AreEqual("Test Name", todaysOrders[0].CustomerName);
        }
        

        [TestCase(5, 4, 2.2, 7.5, 2.32)]
        [TestCase(2, 5.5, 2.5, 2.5, 0.4)]
        public void GetTaxAmtTest(decimal area, decimal costPerSquareFoot, decimal laborCostPerSquareFoot, decimal taxRate, decimal expected)
        {
            decimal materialCost = Calculations.GetMaterialCost(area, costPerSquareFoot);
            decimal laborCost = Calculations.GetLaborCost(area, laborCostPerSquareFoot);
            decimal subTotal = Calculations.GetSubTotal(materialCost, laborCost);
            decimal result = Calculations.GetTaxAmt(taxRate, subTotal);
            Assert.AreEqual(expected, result);
        }

        [TestCase(5, 4, 2.2, 7.5, 33.32)]
        [TestCase(2, 5.5, 2.5, 2.5, 16.40)]
        public void GetTotalAmountTest(decimal area, decimal costPerSquareFoot, decimal laborCostPerSquareFoot, decimal taxRate, decimal expected)
        {
            decimal materialCost = Calculations.GetMaterialCost(area, costPerSquareFoot);
            decimal laborCost = Calculations.GetLaborCost(area, laborCostPerSquareFoot);
            decimal subTotal = Calculations.GetSubTotal(materialCost, laborCost);
            decimal taxAmount = Calculations.GetTaxAmt(taxRate, subTotal);
            decimal result = Calculations.GetTotalCost(subTotal, taxAmount);
            Assert.AreEqual(expected, result);
        }


        [Test]
        public void LoadAllTests()
        {
            OrdersRepo myOrder = new OrdersRepo();
            DateTime myDate = new DateTime(2013, 06, 01);
            var orders = myOrder.LoadAll(myDate);
            Assert.AreEqual(2, orders.Count);
            Assert.AreEqual("Wise", orders[0].CustomerName);
            Assert.AreEqual(2, orders[1].OrderNum);
            Assert.AreEqual("MN", orders[1].State.StateAbbreviation);
            Assert.AreEqual("Wood", orders[0].Product.ProductType);
            Assert.AreEqual(20, orders[1].Area);
            Assert.AreEqual(1051.88, orders[0].TotalCost);

        }

        
        [Test]
        public void GetProductsTest()
        {
            ProductsRepo myProduct = new ProductsRepo();
            var products = myProduct.GetProducts();
            Assert.AreEqual("Laminate", products[1].ProductType);
            Assert.AreEqual(1.75, products[1].CostPerSqFoot);
            Assert.AreEqual(4.15, products[2].LaborCostPerSqFoot);
            Assert.AreEqual("Wood", products[3].ProductType);
        }
       

        [Test]
        public void GetStatesTest()
        {
            StatesRepo myStates = new StatesRepo();
            var states = myStates.GetStates();
            Assert.AreEqual("MN", states[0].StateAbbreviation);
            Assert.AreEqual("Iowa", states[1].StateName);
            Assert.AreEqual(5.75, states[2].TaxRate);
            Assert.AreEqual("ND", states[3].StateAbbreviation);
            Assert.AreEqual("South Dakota", states[4].StateName);
        }


    }
}
