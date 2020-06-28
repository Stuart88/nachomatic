using NachoMatic.Models;
using System.Collections.Generic;
using System.Linq;

namespace NachoMatic.OrderHandling
{
    /// <summary>
    /// For creating itemised listing of costs relating to an individual order item
    /// </summary>
    public class SubTotal
    {
        #region Properties

        public int BasePrice { get; set; }

        public string ItemName { get; set; }
        public int ItemTotal => this.BasePrice + this.SelectedIngredientsInfo.Sum(s => s.Cost);
        public List<IngredientTotals> SelectedIngredientsInfo { get; set; }
        public bool WithRice { get; set; }

        #endregion Properties

        #region Constructors

        public SubTotal(OrderItem item)
        {
            this.ItemName = item.MenuItem.Name;
            this.BasePrice = item.MenuItem.BasePrice;
            this.WithRice = item.WithRice;
            this.SelectedIngredientsInfo = CreateSelectedIngredientsInfo(item);
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Returns a list of subtotal details for each ingredient in this order
        /// </summary>
        private List<IngredientTotals> CreateSelectedIngredientsInfo(OrderItem item)
        {
            List<IngredientTotals> returnList = new List<IngredientTotals>();

            NachoItem nacho = item.MenuItem as NachoItem;

            if (nacho != null && nacho.IsALaCarte)
            {
                List<Ingredient> toPayFor = nacho.IngredientsToPayFor();
                List<Ingredient> notPayingFor = nacho.IngredientsNotPayingFor();

                foreach (Ingredient p in toPayFor)
                {
                    int extraCost = p.AdditionalCost > 0 ? p.AdditionalCost : p.Category.ALaCarteCost;

                    if (returnList.Any(x => x.Name == p.Name))
                    {
                        IngredientTotals addingTo = returnList.First(x => x.Name == p.Name);
                        int index = returnList.IndexOf(addingTo);

                        addingTo.Amount += 1;
                        addingTo.Total += extraCost;

                        returnList[index] = addingTo;
                    }
                    else
                    {
                        returnList.Add(new IngredientTotals
                        {
                            Name = p.Name,
                            Amount = 1,
                            Cost = extraCost,
                            Total = extraCost
                        });
                    }
                }

                foreach (Ingredient p in notPayingFor)
                {
                    if (returnList.Any(x => x.Name == p.Name))
                    {
                        IngredientTotals addingTo = returnList.First(x => x.Name == p.Name);
                        int index = returnList.IndexOf(addingTo);

                        addingTo.Amount += 1;

                        returnList[index] = addingTo;
                    }
                    else
                    {
                        returnList.Add(new IngredientTotals
                        {
                            Name = p.Name,
                            Amount = 1,
                            Cost = 0,
                            Total = 0
                        });
                    }
                }
            }
            else if (nacho != null)
            {
                foreach (Ingredient i in nacho.SelectedIngredients)
                {
                    if (returnList.Any(x => x.Name == i.Name))
                    {
                        IngredientTotals addingTo = returnList.First(x => x.Name == i.Name);
                        int index = returnList.IndexOf(addingTo);

                        addingTo.Amount += 1;
                        addingTo.Total += i.AdditionalCost;

                        returnList[index] = addingTo;
                    }
                    else
                    {
                        returnList.Add(new IngredientTotals
                        {
                            Name = i.Name,
                            Amount = 1,
                            Cost = i.AdditionalCost,
                            Total = i.AdditionalCost
                        });
                    }
                }
            }

            return returnList.OrderByDescending(i => i.Cost).ToList();
        }

        #endregion Methods

        #region Classes

        public class IngredientTotals
        {
            #region Properties

            public int Amount { get; set; }
            public int Cost { get; set; }
            public string Name { get; set; }
            public int Total { get; set; }

            #endregion Properties
        }

        #endregion Classes
    }
}