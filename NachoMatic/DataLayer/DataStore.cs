using NachoMatic.Models;
using System.Collections.Generic;

namespace NachoMatic.DataLayer
{
    /// <summary>
    /// In-memory data store
    /// </summary>
    internal static class DataStore
    {
        #region Fields

        /// <summary>
        /// "Reasonable Maximum" allowed for any one item
        /// </summary>
        public static readonly int MaximumAllowed = 5;

        public static DbSet<IngredientCategory> IngredientCategories = new DbSet<IngredientCategory>
        {
            IngredientCategoryHelper.Base,
            IngredientCategoryHelper.Meat,
            IngredientCategoryHelper.Salsa,
            IngredientCategoryHelper.Topping
        };

        public static DbSet<Ingredient> Ingredients = new DbSet<Ingredient>
        {
            new Ingredient(1)
            {
                Category = IngredientCategoryHelper.Base,
                Name = "Tortilla"
            },
            new Ingredient(2)
            {
                Category = IngredientCategoryHelper.Base,
                Name = "Rice"
            },
            new Ingredient(3)
            {
                Category = IngredientCategoryHelper.Meat,
                Name = "Chicken"
            },
            new Ingredient(4)
            {
                Category = IngredientCategoryHelper.Meat,
                Name = "Steak"
            },
            new Ingredient(5)
            {
                Category = IngredientCategoryHelper.Salsa,
                Name = "Red Salsa"
            },
            new Ingredient(6)
            {
                Category = IngredientCategoryHelper.Salsa,
                Name = "Green Salsa"
            },
            new Ingredient(7)
            {
                Category = IngredientCategoryHelper.Salsa,
                Name = "Queso",
                AdditionalCost = 150
            },
            new Ingredient(8)
            {
                Category = IngredientCategoryHelper.Topping,
                Name = "Grated Cheese"
            },
            new Ingredient(9)
            {
                Category = IngredientCategoryHelper.Topping,
                Name = "Sour Cream"
            },
            new Ingredient(10)
            {
                Category = IngredientCategoryHelper.Topping,
                Name = "Guacamole",
                AdditionalCost = 125
            }
        };

        public static DbSet<BasicItem> BasicItems = new DbSet<BasicItem>
        {
            new BasicItem(1)
            {
                Name = "Soft Drink",
                BasePrice = 125
            },
            new BasicItem(2)
            {
                Name = "Brownie",
                BasePrice = 150
            },
            new BasicItem(3)
            {
                Name = "Chips",
                BasePrice = 200
            }
        };

        public static DbSet<NachoItem> NachoItems = new DbSet<NachoItem>
        {
            new NachoItem(1)
            {
                Name = "2-Ingredient Nacho-in-a-bowl",
                IngredientsIncluded = new List<IngredientCategory>
                {
                    IngredientCategoryHelper.Meat,
                    IngredientCategoryHelper.Salsa
                },
                BasePrice = 399,
                HasTortilla = false
            },
            new NachoItem(2)
            {
                Name = "2-Ingredient Nacho",
                IngredientsIncluded = new List<IngredientCategory>
                {
                    IngredientCategoryHelper.Meat,
                    IngredientCategoryHelper.Salsa
                },
                BasePrice = 499,
                HasTortilla = true
            },
            new NachoItem(3)
            {
                Name = "3-Ingredient Nacho",
                IngredientsIncluded = new List<IngredientCategory>
                {
                    IngredientCategoryHelper.Meat,
                    IngredientCategoryHelper.Salsa,
                    IngredientCategoryHelper.Topping
                },
                BasePrice = 599,
                HasTortilla = true
            },
            new NachoItem(4)
            {
                Name = "A-La-Carte Nacho",
                BasePrice = 599,
                HasTortilla = true,
                IsALaCarte = true
            }
        };

        public static DbSet<OrderItem> OrderItems = new DbSet<OrderItem>();

        #endregion Fields

        #region Classes

        /// <summary>
        /// Ingredient category types, to be used with Ingredient and NachoItem data store items
        /// </summary>
        private static class IngredientCategoryHelper
        {
            #region Fields

            internal static readonly IngredientCategory Base = new IngredientCategory(1, IngredientType.Base, 0);
            internal static readonly IngredientCategory Meat = new IngredientCategory(2, IngredientType.Meat, 50);
            internal static readonly IngredientCategory Salsa = new IngredientCategory(3, IngredientType.Salsa, 50);
            internal static readonly IngredientCategory Topping = new IngredientCategory(4, IngredientType.Topping, 33);

            #endregion Fields
        }

        #endregion Classes
    }
}