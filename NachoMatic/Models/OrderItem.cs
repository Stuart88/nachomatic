using NachoMatic.OrderHandling;
using System;
using System.Runtime.Serialization;

namespace NachoMatic.Models
{
    public class OrderItem : DbItemBase
    {
        #region Properties

        public MenuItem MenuItem { get; set; }
        public Guid OrderId { get; set; }
        public bool WithRice { get; set; }

        #endregion Properties

        #region Constructors

        public OrderItem(MenuItem menuItem, Order parent)
        {
            this.OrderId = parent.OrderId;
            this.MenuItem = menuItem;
        }

        internal OrderItem(int id) : base(id)
        {
        }

        #endregion Constructors

        #region Classes

        [Serializable]
        public class OrderItemException : Exception
        {
            #region Constructors

            public OrderItemException()
            {
            }

            public OrderItemException(string message) : base(message)
            {
            }

            public OrderItemException(string message, Exception inner) : base(message, inner)
            {
            }

            protected OrderItemException(
                SerializationInfo info,
                StreamingContext context) : base(info, context)
            {
            }

            #endregion Constructors
        }

        #endregion Classes
    }
}