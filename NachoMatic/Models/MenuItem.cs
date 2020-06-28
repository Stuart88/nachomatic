namespace NachoMatic.Models
{
    public abstract class MenuItem : DbItemBase
    {
        #region Properties

        public int BasePrice { get; set; }

        public string Name { get; set; }

        #endregion Properties

        #region Constructors

        internal MenuItem(int id) : base(id)
        {
        }

        protected MenuItem()
        {
        }

        #endregion Constructors
    }
}