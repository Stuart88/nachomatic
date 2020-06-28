using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NachoMatic.DataLayer;
using NachoMatic.Models;
using NachoMatic.OrderHandling;
using System.Linq;
using System.Runtime.Remoting.Contexts;

namespace Tests
{
    [TestClass]
    public class OrderTests
    {
        #region Methods

        [TestMethod]
        public void AddForbiddenIngredient()
        {
            UnitTestItems.ClearSelectedIngredients();

            var nacho = UnitTestItems.NachoItems.TwoIngredientNacho;                             // Allowed: 1 meat, 1 salsa

            _ = nacho.AddIngredient(UnitTestItems.Ingredients.Steak);                            //Add 1 meat
            _ = nacho.AddIngredient(UnitTestItems.Ingredients.Queso);                            //Add 1 Salsa

            ProcessResult result = nacho.AddIngredient(UnitTestItems.Ingredients.GratedCheese);  // Try to add a topping

            Assert.IsFalse(result.Success);
            Assert.AreEqual("Can't add any more ingredients!", result.Message);

            UnitTestItems.ClearSelectedIngredients();
        }

        [TestMethod]
        public void AddMoreThanWholeItemAllows()
        {
            UnitTestItems.ClearSelectedIngredients();

            var nacho = UnitTestItems.NachoItems.ThreeIngredientNacho;                              // Allowed: 1 meat, 1 salsa, 1 topping

            _ = nacho.AddIngredient(UnitTestItems.Ingredients.Steak);                               // Add 1 meat
            _ = nacho.AddIngredient(UnitTestItems.Ingredients.Queso);                               // Add 1 salsa
            _ = nacho.AddIngredient(UnitTestItems.Ingredients.GratedCheese);                        // Add 1 topping

            ProcessResult result = nacho.AddIngredient(UnitTestItems.Ingredients.GreenSalsa);       // Try to add another salsa

            Assert.IsFalse(result.Success);
            Assert.AreEqual("Can't add any more ingredients!", result.Message);

            UnitTestItems.ClearSelectedIngredients();
        }

       
        [TestMethod]
        public void AddSecondMeatItem()
        {
            UnitTestItems.ClearSelectedIngredients();

            var nacho = UnitTestItems.NachoItems.TwoIngredientNacho;                        // Allowed: 1 meat, 1 salsa

            _ = nacho.AddIngredient(UnitTestItems.Ingredients.Steak);                       // Add 1 meat item

            ProcessResult result = nacho.AddIngredient(UnitTestItems.Ingredients.Chicken);  // Try to add a second meat item

            Assert.IsFalse(result.Success);
            Assert.AreEqual("Nachos are limited to 1 meat option per meal.", result.Message);

            UnitTestItems.ClearSelectedIngredients();
        }

        [TestMethod]
        public void AddSurplusRice()
        {
            UnitTestItems.ClearSelectedIngredients();

            var nacho = UnitTestItems.NachoItems.ALaCarte;

            _ = nacho.AddIngredient(UnitTestItems.Ingredients.Rice);                        // Add rice
            ProcessResult result = nacho.AddIngredient(UnitTestItems.Ingredients.Rice);     // Add more rice!

            Assert.IsFalse(result.Success);
            Assert.AreEqual("Only 1 serving of rice per meal", result.Message);

            UnitTestItems.ClearSelectedIngredients();
        }

        [TestMethod]
        public void AddTooMuchOfOneIngredient()
        {
            UnitTestItems.ClearSelectedIngredients();

            var nacho = UnitTestItems.NachoItems.ThreeIngredientNacho;                              // Allowed: 1 meat, 1 salsa, 1 topping

            _ = nacho.AddIngredient(UnitTestItems.Ingredients.Steak);                               // Add 1 meat
            _ = nacho.AddIngredient(UnitTestItems.Ingredients.Queso);                               // Add 1 salsa
                                                                                                    // Still allowed to add a topping

            ProcessResult result = nacho.AddIngredient(UnitTestItems.Ingredients.GreenSalsa);       // Try to add another salsa

            Assert.IsFalse(result.Success);
            Assert.AreEqual("Can't add any more of this item!", result.Message);

            UnitTestItems.ClearSelectedIngredients();
        }


        [TestMethod]
        public void AddTortillaWhenNotAllowed()
        {
            UnitTestItems.ClearSelectedIngredients();

            var nacho = UnitTestItems.NachoItems.TwoIngredientNachoInBowl;                  // Tortilla NOT included!

            ProcessResult result = nacho.AddIngredient(UnitTestItems.Ingredients.Tortilla); // Try to add torilla

            Assert.IsFalse(result.Success);
            Assert.AreEqual("This product does not include tortilla.", result.Message);

            
            UnitTestItems.ClearSelectedIngredients();
        }

        [TestMethod]
        public void SimulateCompleteOrder()
        {
            UnitTestItems.ClearSelectedIngredients();

            Order order = UnitTestItems.GetSimulatedOrderItem();

            order.SaveOrder();

            //Check order was saved
            
            Order retrievedOrder = new Order(order.OrderId); //Finds order in data store

            Assert.AreEqual(order.OrderId, retrievedOrder.OrderId);   
            Assert.AreEqual(UnitTestItems.SimulatedOrderItemCost, retrievedOrder.Total);
        }


        [TestMethod]
        public void AddIncompleteItemToOrder()
        {
            UnitTestItems.ClearSelectedIngredients();

            NachoItem nacho = UnitTestItems.NachoItems.TwoIngredientNachoInBowl;        // Create nacho item
                                                                                        // Don't select any ingredients!

            Order order = new Order();                                                  // Create an order and add the item
            ProcessResult result = order.AddItem(nacho);

            Assert.IsFalse(result.Success);
            Assert.AreEqual("Order incomplete! Not enough ingredients.", result.Message);

        }

        [TestMethod]
        public void AddItemToOrder()
        {
            UnitTestItems.ClearSelectedIngredients();

            NachoItem nacho = UnitTestItems.NachoItems.TwoIngredientNachoInBowl;
            nacho.AddIngredient(UnitTestItems.Ingredients.Chicken);
            nacho.AddIngredient(UnitTestItems.Ingredients.GreenSalsa);

            Order order = new Order();                                                  
            ProcessResult result = order.AddItem(nacho);

            Assert.IsTrue(result.Success);

            UnitTestItems.ClearSelectedIngredients();

        }

        [TestMethod]
        public void RemoveItemFromOrder()
        {
            UnitTestItems.ClearSelectedIngredients();

            NachoItem nacho = UnitTestItems.NachoItems.TwoIngredientNachoInBowl;
            nacho.AddIngredient(UnitTestItems.Ingredients.Chicken);
            nacho.AddIngredient(UnitTestItems.Ingredients.GreenSalsa);

            Order order = new Order();
            _ = order.AddItem(nacho);

            ProcessResult result = order.RemoveItem(nacho);

            Assert.IsTrue(result.Success);

            UnitTestItems.ClearSelectedIngredients();

        }

        [TestMethod]
        public void AttemptToRemoveNonexistentOrderItem()
        {
            UnitTestItems.ClearSelectedIngredients();

            NachoItem nacho = UnitTestItems.NachoItems.TwoIngredientNachoInBowl;
            nacho.AddIngredient(UnitTestItems.Ingredients.Chicken);
            nacho.AddIngredient(UnitTestItems.Ingredients.GreenSalsa);

            Order order = new Order();

            ProcessResult result = order.RemoveItem(nacho);

            Assert.IsFalse(result.Success);
            Assert.AreEqual("Item does not exist in this order.", result.Message);

            UnitTestItems.ClearSelectedIngredients();

        }

        [TestMethod]
        public void TestSubtotals()
        {
            UnitTestItems.ClearSelectedIngredients();

            Order order = UnitTestItems.GetSimulatedOrderItem();

            List<SubTotal> subtotals = order.GetSubTotals();


            //Subtotals should return as below. Ingredient items are ordered by price high to low

            // Item[0] (nacho bowl)
            // Base Price: 399
            // Chicken, Amount 1, Cost 0
            // Red Salsa, Amount 1, Cost 0
            //
            // Item[1] (3 item nacho)
            // Base Price: 599
            // Queso, Amount 1, Cost 150
            // Chicken, Amount 1, Cost 0
            // Grated Cheese, Amount 1, Cost 0
            // 
            // Item[2] (a-la-carte)
            // Base Price 599
            // Steak, Amount 2, Cost 50
            // Red Salsa, Amount 1, Cost 33
            // Grated Cheese, Amount 1, Cost 33
            // Queso, Amount 1, Cost 0
            // Guacamole, Amount 1, Cost 0
            //
            // Item[3] (chips)
            // Base Price: 200
            //
            // Item[4] (cola)
            // Base Price: 125

            Assert.AreEqual(5, subtotals.Count);

            Assert.AreEqual(399, subtotals[0].BasePrice);
            Assert.AreEqual(599, subtotals[1].BasePrice);
            Assert.AreEqual(599, subtotals[2].BasePrice);
            Assert.AreEqual(200, subtotals[3].BasePrice);
            Assert.AreEqual(125, subtotals[4].BasePrice);

            Assert.AreEqual(2, subtotals[0].SelectedIngredientsInfo.Count);
            Assert.AreEqual(3, subtotals[1].SelectedIngredientsInfo.Count);

            Assert.AreEqual(749, subtotals[1].ItemTotal);

            Assert.AreEqual(150, subtotals[1].SelectedIngredientsInfo[0].Cost);

            Assert.AreEqual(2, subtotals[2].SelectedIngredientsInfo[0].Amount);
            Assert.AreEqual(1, subtotals[2].SelectedIngredientsInfo[1].Amount);
            Assert.AreEqual(1, subtotals[2].SelectedIngredientsInfo[2].Amount);
            Assert.AreEqual(1, subtotals[2].SelectedIngredientsInfo[3].Amount);
            Assert.AreEqual(1, subtotals[2].SelectedIngredientsInfo[4].Amount);

            Assert.AreEqual(50, subtotals[2].SelectedIngredientsInfo[0].Cost);
            Assert.AreEqual(50, subtotals[2].SelectedIngredientsInfo[1].Cost);
            Assert.AreEqual(33, subtotals[2].SelectedIngredientsInfo[2].Cost);
            Assert.AreEqual(0, subtotals[2].SelectedIngredientsInfo[3].Cost);
            Assert.AreEqual(0, subtotals[2].SelectedIngredientsInfo[4].Cost);

            UnitTestItems.ClearSelectedIngredients();

        }

        #endregion Methods
    }
}