using NachoMatic.Models;
using System.Collections.Generic;
using System.Linq;

namespace NachoMatic.DataLayer
{
    public class DbSet<T> : List<T>, IRepository<T> where T : DbItemBase
    {
        #region Constructors

        public DbSet(DbSet<T> dbSet) : base(dbSet)
        {
        }

        internal DbSet()
        {
        }

        #endregion Constructors

        #region Methods

        public void Delete(int itemId)
        {
            T toRemove = this.FirstOrDefault(i => i.Id == itemId);

            if (toRemove != null)
            {
                Remove(toRemove);
            }
        }

        public IEnumerable<T> GetAll()
        {
            return this;
        }

        public T GetById(int itemId)
        {
            return this.FirstOrDefault(i => i.Id == itemId);
        }

        public void Insert(T item)
        {
            if (this.Count == 0)
            {
                item.SetId(1);
            }
            else
            {
                item.SetId(this.Max(i => i.Id) + 1);
            }

            Add(item);
        }

        public void InsertRange(List<T> items)
        {
            foreach (T i in items)
            {
                Insert(i);
            }
        }

        public void Update(T item)
        {
            int updateIndex = FindIndex(i => i.Id == item.Id);

            if (updateIndex > -1)
            {
                this[updateIndex] = item;
            }
        }

        #endregion Methods
    }
}