using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NachoMatic.DataLayer;
using NachoMatic.Models;

namespace Tests
{

   [TestClass]
    public class DataStoreTests
    {
        private NachoMaticContext Context = new NachoMaticContext();

        [TestMethod]
        public void InsertItemToDataStore()
        {
            var category = Context.IngredientCategories.FirstOrDefault(c => c.Type == NachoMatic.IngredientType.Meat);

            if (category != null)
            {
                Ingredient newIngredient = new Ingredient
                {
                    Category = category,
                    AdditionalCost = 0,
                    Name = "Pork",
                };

                Context.Ingredients.Insert(newIngredient);
                Context.SaveChanges();

                //Check save successful by opening new context and inspecting data

                NachoMaticContext newContext = new NachoMaticContext();

                bool hasNewItem = newContext.Ingredients.Any(i => i.Name == "Pork");

                Assert.IsTrue(hasNewItem);
            }
        }

        [TestMethod]
        public void InsertRangeToDataStore()
        {
            List<Ingredient> itemsToAdd = new List<Ingredient>
            {
                new Ingredient
                {
                    Category = UnitTestItems.IngredientCategories.Meat,
                    AdditionalCost = 0,
                    Name = "Pork",
                },
                new Ingredient
                {
                    Category = UnitTestItems.IngredientCategories.Topping,
                    AdditionalCost = 0,
                    Name = "BBQ Sauce",
                },
            };

            Context.Ingredients.InsertRange(itemsToAdd);
            Context.SaveChanges();

            //Check save successful by opening new context and inspecting data

            NachoMaticContext newContext = new NachoMaticContext();

            bool hasNewItems = newContext.Ingredients.Any(i => i.Name == "Pork") && newContext.Ingredients.Any(i => i.Name == "BBQ Sauce");

            Assert.IsTrue(hasNewItems);
        }

        [TestMethod]
        public void DeleteItemFromDataStore()
        {
            IngredientCategory removing = Context.IngredientCategories.First();

            Context.IngredientCategories.Delete(removing.Id);

            Context.SaveChanges();

            //Check save successful by opening new context and inspecting data

            NachoMaticContext newContext = new NachoMaticContext();

            bool itemGone = newContext.IngredientCategories.All(i => i.Name != removing.Name);

            Assert.IsTrue(itemGone);
        }

        [TestMethod]
        public void UpdateItemInDataStore()
        {
            NachoItem originalItem = Context.NachoItems.First();

            originalItem.Name = "New Name";

            Context.NachoItems.Update(originalItem);

            Context.SaveChanges();

            //Check save successful by opening new context and inspecting data

            NachoMaticContext newContext = new NachoMaticContext();

            NachoItem updatedItem = newContext.NachoItems.GetById(originalItem.Id);

            Assert.AreEqual("New Name", updatedItem.Name);
        }

        [TestMethod]
        public void GetAllFromDataStore()
        {
            var allIngredients = Context.Ingredients.GetAll();

            int expectedAmount = 10; //This will nned to change if data store has items manually added or removed...

            Assert.AreEqual(expectedAmount, allIngredients.Count());
        }

        [TestMethod]
        public void GetByIdFromDataStore()
        {
            var setupItem = Context.Ingredients.First();

            if (setupItem != null)
            {
                var testItem = Context.Ingredients.GetById(setupItem.Id);

                Assert.AreEqual(setupItem.Id, testItem.Id);
            }

           
        }
    }
}
