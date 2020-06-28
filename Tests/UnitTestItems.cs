using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NachoMatic;
using NachoMatic.Models;
using NachoMatic.OrderHandling;

namespace Tests
{
    public static class UnitTestItems
    {
        public static class IngredientCategories
        {
            public static IngredientCategory Base = new IngredientCategory(IngredientType.Base, 0);
            public static IngredientCategory Meat = new IngredientCategory(IngredientType.Meat, 50);
            public static IngredientCategory Salsa = new IngredientCategory(IngredientType.Salsa, 50);
            public static IngredientCategory Topping = new IngredientCategory(IngredientType.Topping, 33);
        }

        public static class Ingredients
        {
            public static Ingredient Tortilla = new Ingredient()
            {
                Category = IngredientCategories.Base,
                Name = "Tortilla"
            };
            public static Ingredient Rice = new Ingredient()
            {
                Category = IngredientCategories.Base,
                Name = "Rice"
            };
            public static Ingredient Chicken = new Ingredient()
            {
                Category = IngredientCategories.Meat,
                Name = "Chicken"
            };
            public static Ingredient Steak = new Ingredient()
            {
                Category = IngredientCategories.Meat,
                Name = "Steak"
            };
            public static Ingredient RedSalsa = new Ingredient()
            {
                Category = IngredientCategories.Salsa,
                Name = "Red Salsa"
            };
            public static Ingredient GreenSalsa = new Ingredient()
            {
                Category = IngredientCategories.Salsa,
                Name = "Green Salsa"
            };
            public static Ingredient Queso = new Ingredient()
            {
                Category = IngredientCategories.Salsa,
                Name = "Queso",
                AdditionalCost = 150
            };
            public static Ingredient GratedCheese = new Ingredient()
            {
                Category = IngredientCategories.Topping,
                Name = "Grated Cheese"
            };
            public static Ingredient SourCream = new Ingredient()
            {
                Category = IngredientCategories.Topping,
                Name = "Sour Cream"
            };

            public static Ingredient Guacamole = new Ingredient()
            {
                Category = IngredientCategories.Topping,
                Name = "Guacamole",
                AdditionalCost = 125,
            };
        }
        public static class NachoItems
        {
            public static NachoItem TwoIngredientNachoInBowl = new NachoItem
            {
                Name = "2-Ingredient Nacho-in-a-bowl",
                IngredientsIncluded = new List<IngredientCategory>
                {
                    IngredientCategories.Meat,
                    IngredientCategories.Salsa,
                },
                BasePrice = 399,
                HasTortilla = false
            };

            public static NachoItem TwoIngredientNacho = new NachoItem()
            {
                Name = "2-Ingredient Nacho",
                IngredientsIncluded = new List<IngredientCategory>
                {
                    IngredientCategories.Meat,
                    IngredientCategories.Salsa,
                },
                BasePrice = 499,
                HasTortilla = true
            };
            public static NachoItem ThreeIngredientNacho = new NachoItem
            {
                Name = "3-Ingredient Nacho",
                IngredientsIncluded = new List<IngredientCategory>
                {
                    IngredientCategories.Meat,
                    IngredientCategories.Salsa,
                    IngredientCategories.Topping,
                },
                BasePrice = 599,
                HasTortilla = true
            };
            public static NachoItem ALaCarte = new NachoItem
            {
                Name = "A-La-Carte Nacho",
                BasePrice = 599,
                HasTortilla = true,
                IsALaCarte = true
            };
        }

        public static class BasicItems
        {
            public static BasicItem SoftDrink = new BasicItem
            {
                BasePrice = 125,
                Name = "Soft Drink"
            };
            public static BasicItem Brownie = new BasicItem
            {
                BasePrice = 150,
                Name = "Brownie"
            };
            public static BasicItem Chips = new BasicItem
            {
                BasePrice = 200,
                Name = "Chips"
            };
        }

        /// <summary>
        /// An order consisting of two nacho items, an a-la-carte nacho, and two basic items.
        /// </summary>
        /// <returns></returns>
        public static Order GetSimulatedOrderItem()
        {
            NachoItem nachosBowl = NachoItems.TwoIngredientNachoInBowl;   // Price 399
            _ = nachosBowl.AddIngredient(Ingredients.Chicken);
            _ = nachosBowl.AddIngredient(Ingredients.RedSalsa);

            NachoItem nachos = NachoItems.ThreeIngredientNacho;          // Price 749
            _ = nachos.AddIngredient(Ingredients.Chicken);
            _ = nachos.AddIngredient(Ingredients.Queso);
            _ = nachos.AddIngredient(Ingredients.GratedCheese);

            NachoItem nachoALaCarte = NachoItems.ALaCarte;               // Price 732
            _ = nachoALaCarte.AddIngredient(Ingredients.Tortilla);
            _ = nachoALaCarte.AddIngredient(Ingredients.Rice);
            _ = nachoALaCarte.AddIngredient(Ingredients.Steak);
            _ = nachoALaCarte.AddIngredient(Ingredients.Steak);
            _ = nachoALaCarte.AddIngredient(Ingredients.RedSalsa);
            _ = nachoALaCarte.AddIngredient(Ingredients.Queso);
            _ = nachoALaCarte.AddIngredient(Ingredients.GratedCheese);
            _ = nachoALaCarte.AddIngredient(Ingredients.Guacamole);


            BasicItem chips = BasicItems.Chips;                          // Price 200
            BasicItem cola = BasicItems.SoftDrink;                       // Price 125


            Order order = new Order();
            _ = order.AddItem(nachosBowl);
            _ = order.AddItem(nachos);
            _ = order.AddItem(nachoALaCarte);
            _ = order.AddItem(chips);
            _ = order.AddItem(cola);

            return order;                                                               // Total 2205
        }

        public static int SimulatedOrderItemCost = 2205;


        /// <summary>
        /// Clears any ingredients that have been added to menu items in other test runs.
        /// </summary>
        public static void ClearSelectedIngredients()
        {
            NachoItems.TwoIngredientNacho.SelectedIngredients.Clear();
            NachoItems.ThreeIngredientNacho.SelectedIngredients.Clear();
            NachoItems.ALaCarte.SelectedIngredients.Clear();
            NachoItems.TwoIngredientNachoInBowl.SelectedIngredients.Clear();
        }
    }
}
