
namespace ShoppingBasket;

public class Warehouse(List<Product> products)
{
    private readonly Dictionary<string, Product> _inventory = products.ToDictionary(p => p.Id);

    internal Product? FindProduct(string productId) => _inventory.GetValueOrDefault(productId);
}