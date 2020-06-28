using NachoMatic.DataLayer;
using NachoMatic.OrderHandling;
using System.Collections.Generic;
using System.Linq;

namespace NachoMatic.Models
{
    public class NachoItem : MenuItem
    {
        #region Properties

        public bool HasTortilla { get; set; }

        public List<IngredientCategory> IngredientsIncluded { get; set; } = new List<IngredientCategory>();

        public bool IsALaCarte { get; set; }

        public List<Ingredient> SelectedIngredients { get; set; } = new List<Ingredient>();

        #endregion Properties

        #region Constructors

        public NachoItem(int id) : base(id)
        {
        }

        public NachoItem()
        {
        }

        #endregion Constructors

        #region Methods

        public ProcessResult AddIngredient(Ingredient ingredient)
        {
            // LIMIT: Cannot add more than the whole item allows
            if (HasReachedLimitForMenuItem())
            {
                return new ProcessResult(false, "Can't add any more ingredients!");
            }

            // RULE: The Nacho-in-a-bowl product can NOT have a tortilla
            if (!this.HasTortilla && ingredient.Name == "Tortilla")
            {
                return new ProcessResult(false, "This product does not include tortilla.");
            }
            else if (ingredient.Name == "Tortilla")
            {
                return AddIngredient();
            }

            // RICE: No rule specified but let's assume a limit of 1 portion of rice per meal
            if (this.SelectedIngredients.Any(i => i.Name == "Rice") && ingredient.Name == "Rice")
            {
                return new ProcessResult(false, "Only 1 serving of rice per meal");
            }
            else if (ingredient.Name == "Rice")
            {
                return AddIngredient();
            }

            // RULE: On all products, you can select only ONE meat ingredient (a-la-carte presumed to be an exception)
            if (!this.IsALaCarte && this.SelectedIngredients.Any(i => i.Category.Type == IngredientType.Meat) && ingredient.Category.Type == IngredientType.Meat)
            {
                return new ProcessResult(false, "Nachos are limited to 1 meat option per meal.");
            }
            else if (ingredient.Category.Type == IngredientType.Meat)
            {
                return AddIngredient();
            }

            // LIMIT: Cannot add more than each specific ingredient allows
            if (ReachedLimitForIngredient(ingredient))
            {
                return new ProcessResult(false, "Can't add any more of this item!");
            }

            return AddIngredient();

            ProcessResult AddIngredient()
            {
                this.SelectedIngredients.Add(ingredient);
                return new ProcessResult(true);
            }
        }

        /// <summary>
        /// Returns ingredient items that will not need to be paid for. Only applies to a-la-carte menu items.
        /// </summary>
        /// <returns></returns>
        internal List<Ingredient> IngredientsNotPayingFor()
        {
            if (this.IsALaCarte)
            {
                // First clones original list of selected ingredients
                // Then get items being paid for, cycle through those items and remove their instance from the cloned list.
                // Thus the remaining items are only those for which no charge is applied

                List<Ingredient> clonedList = new List<Ingredient>(this.SelectedIngredients);

                List<Ingredient> payingFor = IngredientsToPayFor();

                foreach (Ingredient i in payingFor)
                {
                    clonedList.Remove(i);
                }

                return clonedList;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Returns ingredient items that will need to be paid for. Only applies to a-la-carte menu items.
        /// </summary>
        /// <returns></returns>
        internal List<Ingredient> IngredientsToPayFor()
        {
            // "For the purpose of calculating the price, assume the base
            // ingredients are free and the 3 included ingredients consist
            // of the 3 most expensive ingredients."

            if (this.IsALaCarte)
            {
                return this.SelectedIngredients
                    .Where(i => i.AdditionalCost > 0 || i.Category.ALaCarteCost > 0)
                    .OrderByDescending(i => i.AdditionalCost)
                    .ThenByDescending(i => i.Category.ALaCarteCost)
                    .Skip(3)
                    .ToList();
            }
            else
            {
                return null;
            }
        }

        public ProcessResult RemoveIngredient(Ingredient ingredient)
        {
            Ingredient toRemove = this.SelectedIngredients.FirstOrDefault(i => i == ingredient);

            if (toRemove != null)
            {
                this.SelectedIngredients.Remove(toRemove);
                return new ProcessResult(true);
            }
            else
            {
                return new ProcessResult(false, "This ingredient was not added to this menu item.");
            }
        }

        /// <summary>
        /// Checks if order item has already reached the maximum amount of allowed ingredients
        /// </summary>
        /// <returns></returns>
        private bool HasReachedLimitForMenuItem()
        {
            if (!this.IsALaCarte)
            {
                int maxAllowed = this.IngredientsIncluded.Count;

                //Count the amount of ingredients that have been added from the 'IngredientsIncluded' list
                int alreadyAddedCount = this.SelectedIngredients.Count(i => this.IngredientsIncluded.Contains(i.Category));

                return maxAllowed == alreadyAddedCount;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Checks if order has reached the limit for this ingredient
        /// </summary>
        /// <param name="ingredient"></param>
        /// <returns></returns>
        private bool ReachedLimitForIngredient(Ingredient ingredient)
        {
            int amountAdded = this.SelectedIngredients.Count(i => i.Category == ingredient.Category);

            bool reachedMaximumAllowed = amountAdded == DataStore.MaximumAllowed;

            bool reachedLimitForMenuItem = !this.IsALaCarte && amountAdded == this.IngredientsIncluded.Count(i => i.Type == ingredient.Category.Type);

            return reachedMaximumAllowed || reachedLimitForMenuItem;
        }

        #endregion Methods
    }
}