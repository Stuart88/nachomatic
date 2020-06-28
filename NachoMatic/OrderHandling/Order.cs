using NachoMatic.DataLayer;
using NachoMatic.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NachoMatic.OrderHandling
{
    public class Order
    {
        #region Fields

        private NachoMaticContext _context = new NachoMaticContext();

        #endregion Fields

        #region Properties

        public List<OrderItem> Items { get; set; } = new List<OrderItem>();

        public Guid OrderId { get; }
        public int Total => GetSubTotals().Sum(s => s.ItemTotal);

        #endregion Properties

        #region Constructors

        public Order()
        {
            this.OrderId = Guid.NewGuid();
        }

        public Order(Guid savedOrderId)
        {
            this.OrderId = savedOrderId;
            this.Items = GetSavedOrder(savedOrderId);
        }

        #endregion Constructors

        #region Methods

        public ProcessResult AddItem(MenuItem item)
        {
            //For Nacho orders, need to check enough ingredients have been chosen
            if (item is NachoItem nachoItem && nachoItem.SelectedIngredients.Count < nachoItem.IngredientsIncluded.Count)
            {
                return new ProcessResult(false, "Order incomplete! Not enough ingredients.");
            }
            else
            {
                this.Items.Add(new OrderItem(item, this));
                return new ProcessResult(true);
            }
        }

        public List<SubTotal> GetSubTotals()
        {
            return this.Items.Select(i => new SubTotal(i)).ToList();
        }

        public ProcessResult RemoveItem(MenuItem item)
        {
            OrderItem toRemove = this.Items.FirstOrDefault(i => i.MenuItem == item);

            if (toRemove != null)
            {
                this.Items.Remove(toRemove);
                return new ProcessResult(true);
            }
            else
            {
                return new ProcessResult(false, "Item does not exist in this order.");
            }
        }

        /// <summary>
        /// Completes the order and saves to data store
        /// </summary>
        public void SaveOrder()
        {
            _context.OrderItems.InsertRange(this.Items);
            _context.SaveChanges();
        }

        private List<OrderItem> GetSavedOrder(Guid id)
        {
            return _context.OrderItems.Where(o => o.OrderId == id).ToList();
        }

        #endregion Methods
    }
}