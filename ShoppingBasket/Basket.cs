
namespace ShoppingBasket;

public class Basket(Warehouse warehouse)
{
    private readonly List<Product> _items = [];

    public IReadOnlyList<Product> Items { get => _items.AsReadOnly(); }

    public decimal Total
    {
        get
        {
            var discount = CalculateBuy1Get1FreeDiscount();
            return Math.Round(_items.Sum(i => i.Price - i.Price * i.Discount) - discount, 2);
        }
    }

    public void Scan(string productId)
    {
        Product? product = warehouse.FindProduct(productId);
        if (product == null)
        {
            Console.WriteLine($"Product not found in inventory: {productId}");
            return;
        }
        _items.Add(product);
    }

    decimal CalculateBuy1Get1FreeDiscount()
    {
        var grouped = _items.GroupBy(i => i).ToDictionary(g => g.Key, g => g.Count());
        decimal discount = 0;
        foreach (var item in grouped)
        {
            if (!item.Key.Buy1Get1Free) continue;
            var freeItems = item.Value / 2;
            discount += freeItems * item.Key.Price;
        }
        return discount;
    }
}