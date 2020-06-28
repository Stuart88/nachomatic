namespace NachoMatic.Models
{
    public abstract class DbItemBase
    {
        #region Properties

        public int Id { get; private set; }

        #endregion Properties

        #region Constructors

        internal DbItemBase()
        {
        }

        internal DbItemBase(int id)
        {
            this.Id = id;
        }

        #endregion Constructors

        #region Methods

        internal void SetId(int id)
        {
            this.Id = id;
        }

        #endregion Methods
    }
}