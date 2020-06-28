namespace NachoMatic.Models
{
    public class Ingredient : DbItemBase
    {
        #region Properties

        /// <summary>
        /// For a-la-carte orders, this value will be used if it is higher than the "ALaCarteAdditionalCosts" value for this item type.
        /// </summary>
        public int AdditionalCost { get; set; } = 0;

        public IngredientCategory Category { get; set; }

        public string Name { get; set; }

        #endregion Properties

        #region Constructors

        public Ingredient()
        {
        }

        internal Ingredient(int id) : base(id)
        {
        }

        #endregion Constructors
    }
}