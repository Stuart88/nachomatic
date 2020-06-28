using System.Collections.Generic;

namespace NachoMatic.DataLayer
{
    public interface IRepository<T>
    {
        #region Methods

        void Delete(int itemId);

        IEnumerable<T> GetAll();

        T GetById(int itemId);

        void Insert(T item);

        void InsertRange(List<T> items);

        void Update(T item);

        #endregion Methods
    }
}