# NachoMatic

Class library and mock data store for the "NachoMatic" project

# Data Store

All data store items can be created or modified from within `DataStore.cs`. 

No code changes should be required unless adding a new ingredient category, for which an accompanying enum value must be added to `Enums.cs`.

# Usage

```c#
using NachoMatic;
using NachoMatic.Models;
using NachoMatic.OrderHandling;

// Instantiate data context

NachoMaticContext context = new NachoMaticContext();

// Get data items

IEnumarable<NachoItem> nachos = context.NachoItems.GetAll();
IEnumarable<OrderItem> orders = context.OrderItems.GetAll();
IEnumarable<Ingredient> ingredients = context.Ingredients.GetAll();
IEnumarable<BasicItem> basicItems = context.BasicItems.GetAll(); // soft drinks, chips etc

```

## Basic item creation

```c#

// Select nacho meal option

var nachoMeal = context.NachoItems.GetById(4); // Id number sent in via event, user click, etc

// Add ingredient choices

Ingredient ingredient = context.Ingredients.GetById(3);

ProcessResult result = mealOption.AddIngredient(ingredient);

if(result.Success)
{
  //added okay!
}
else
{
  string errorMsg = result.Message; // e.g., "Nachos are limited to 1 meat option per meal."
}

// Or remove item with mealOption.RemoveIngredient()


```

## Basic order creation

```c#
//Create order item

Order myOrder = new Order();

ProcessResult result = myOrder.AddItem(nachoMeal);

if(result.Success)
{
  //Added successfully!
}
else
{
  string errorMsg = result.Message; //e.g. "Order incomplete! Not enough ingredients."
}

// Or remove order item with myOrder.RemoveItem()


```

## Order retrieval

```c#

//An order can be quickly retrieved from the data store using its orderId

Order savedOrder = new Order(savedOrderId); // savedOrderId is a Guid which is generated upon creation of the order.

```

## Order subtotals

```c#

//Generate a list of detailed subtotals (e.g. for a receipt)

Order myOrder = new Order();

// After items have been added to order...

List<SubTotal> subtotals = myOrder.GetSubTotals();

```

## Order totals

```c#

Order myOrder = new Order();

// After items have been added to order...

int total = myOrder.Total;  // Value in pence, e.g. 1798 = £17.98

```
