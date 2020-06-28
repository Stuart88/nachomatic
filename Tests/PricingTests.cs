using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NachoMatic.DataLayer;
using NachoMatic.Models;
using NachoMatic.OrderHandling;

namespace Tests
{
    [TestClass]
    public class PricingTests
    {
        private NachoMaticContext Context = new NachoMaticContext();

        [TestMethod]
        public void AlaCarteCosts1()
        {
            UnitTestItems.ClearSelectedIngredients();

            NachoItem nacho = UnitTestItems.NachoItems.ALaCarte;
            nacho.AddIngredient(UnitTestItems.Ingredients.Tortilla);
            nacho.AddIngredient(UnitTestItems.Ingredients.Rice);
            nacho.AddIngredient(UnitTestItems.Ingredients.Chicken);
            nacho.AddIngredient(UnitTestItems.Ingredients.RedSalsa);
            nacho.AddIngredient(UnitTestItems.Ingredients.SourCream);

            Order order = new Order();
            order.AddItem(nacho);

            int result = order.Total;
            int expected = 599;
            
            Assert.AreEqual(expected, result);

            UnitTestItems.ClearSelectedIngredients();
        }

        [TestMethod]
        public void AlaCarteCosts2()
        {
            UnitTestItems.ClearSelectedIngredients();

            NachoItem nacho = UnitTestItems.NachoItems.ALaCarte;
            nacho.AddIngredient(UnitTestItems.Ingredients.Tortilla);
            nacho.AddIngredient(UnitTestItems.Ingredients.Rice);
            nacho.AddIngredient(UnitTestItems.Ingredients.Chicken);
            nacho.AddIngredient(UnitTestItems.Ingredients.RedSalsa);
            nacho.AddIngredient(UnitTestItems.Ingredients.GreenSalsa);
            nacho.AddIngredient(UnitTestItems.Ingredients.SourCream);

            Order order = new Order();
            order.AddItem(nacho);


            int result = order.Total;
            int expected = 632;

            Assert.AreEqual(expected, result);

            UnitTestItems.ClearSelectedIngredients();
        }

        [TestMethod]
        public void AlaCarteCosts3()
        {
            UnitTestItems.ClearSelectedIngredients();

            NachoItem nacho = UnitTestItems.NachoItems.ALaCarte;
            nacho.AddIngredient(UnitTestItems.Ingredients.Tortilla);
            nacho.AddIngredient(UnitTestItems.Ingredients.Rice);
            nacho.AddIngredient(UnitTestItems.Ingredients.Chicken);
            nacho.AddIngredient(UnitTestItems.Ingredients.Steak);
            nacho.AddIngredient(UnitTestItems.Ingredients.RedSalsa);
            nacho.AddIngredient(UnitTestItems.Ingredients.Queso);
            nacho.AddIngredient(UnitTestItems.Ingredients.GratedCheese);
            nacho.AddIngredient(UnitTestItems.Ingredients.Guacamole);

            Order order = new Order();
            order.AddItem(nacho);


            int result = order.Total;
            int expected = 732;

            Assert.AreEqual(expected, result);

            UnitTestItems.ClearSelectedIngredients();
        }

        [TestMethod]
        public void FullOrderCost()
        {
            UnitTestItems.ClearSelectedIngredients();

            Order order = UnitTestItems.GetSimulatedOrderItem();

            int result = order.Total;
            int expected = UnitTestItems.SimulatedOrderItemCost;

            Assert.AreEqual(expected, result);

            UnitTestItems.ClearSelectedIngredients();
        }
    }
}
