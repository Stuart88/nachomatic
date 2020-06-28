using System;

namespace NachoMatic.Models
{
    public class IngredientCategory : DbItemBase
    {
        #region Properties

        public int ALaCarteCost { get; }

        public string Name { get; }

        public IngredientType Type { get; }

        #endregion Properties

        #region Constructors

        public IngredientCategory(IngredientType type)
        {
            this.Name = Enum.GetName(typeof(IngredientType), type);
            this.Type = type;
        }

        public IngredientCategory(IngredientType type, int aLaCarteCost)
        {
            this.Name = Enum.GetName(typeof(IngredientType), type);
            this.Type = type;
            this.ALaCarteCost = aLaCarteCost;
        }

        internal IngredientCategory(int id, IngredientType type, int aLaCarteCost) : base(id)
        {
            this.Name = Enum.GetName(typeof(IngredientType), type);
            this.Type = type;
            this.ALaCarteCost = aLaCarteCost;
        }

        #endregion Constructors
    }
}