using NachoMatic.Models;

namespace NachoMatic.DataLayer
{
    public class NachoMaticContext
    {
        #region Properties

        public DbSet<BasicItem> BasicItems { get; set; }
        public DbSet<IngredientCategory> IngredientCategories { get; set; }

        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<NachoItem> NachoItems { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        #endregion Properties

        #region Constructors

        public NachoMaticContext()
        {
            this.BasicItems = DataStore.MenuItems;
            this.NachoItems = DataStore.NachoItems;
            this.Ingredients = DataStore.Ingredients;
            this.IngredientCategories = DataStore.IngredientCategories;
            this.OrderItems = DataStore.OrderItems;
        }

        #endregion Constructors

        #region Methods

        public void SaveChanges()
        {
            // This obviously isn't ideal, but I don't think the aim of this challenge is to write an entire ORM system from scratch!
            // I would normally use Entity Framework, but that would make this part of the challenge so easy that it didn't seem correct to use it.

            DataStore.MenuItems = new DbSet<BasicItem>(this.BasicItems);
            DataStore.NachoItems = new DbSet<NachoItem>(this.NachoItems);
            DataStore.NachoItems = new DbSet<NachoItem>(this.NachoItems);
            DataStore.IngredientCategories = new DbSet<IngredientCategory>(this.IngredientCategories);
            DataStore.OrderItems = new DbSet<OrderItem>(this.OrderItems);
        }

        #endregion Methods
    }
}