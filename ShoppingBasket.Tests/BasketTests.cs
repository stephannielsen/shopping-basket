namespace ShoppingBasket.Tests;

public class BasketTests
{
    [Fact]
    public void Scan_AddsProductToBasket()
    {
        List<Product> products = [new Product { Id = "A0001", Price = 12.99m }, new Product { Id = "A0002", Price = 3.99m }];
        Warehouse warehouse = new(products);
        Basket basket = new(warehouse);

        var productId = "A0001";

        basket.Scan(productId);

        Assert.Contains(basket.Items, (i) => i.Id == productId);
    }

    [Fact]
    public void Scan_AddProductNotInInventory_NotAdded()
    {
        List<Product> products = [new Product { Id = "A0001", Price = 12.99m }, new Product { Id = "A0002", Price = 3.99m }];
        Warehouse warehouse = new(products);
        Basket basket = new(warehouse);

        var productId = "A0003";

        basket.Scan(productId);

        Assert.DoesNotContain(basket.Items, (i) => i.Id == productId);
    }

    [Fact]
    public void Scan_AddsManyProductToBasket()
    {
        List<Product> products = [new Product { Id = "A0001", Price = 12.99m }, new Product { Id = "A0002", Price = 3.99m }];
        Warehouse warehouse = new(products);
        Basket basket = new(warehouse);

        List<string> productIds = ["A0001", "A0002"];

        productIds.ForEach(productId =>
        {
            basket.Scan(productId);
        });

        Assert.Equal(basket.Items.Select(i => i.Id), productIds);
    }

    [Fact]
    public void Scan_TotalSingle_ReturnsPriceSum()
    {
        List<Product> products = [new Product { Id = "A0001", Price = 12.99m }, new Product { Id = "A0002", Price = 3.99m }];
        Warehouse warehouse = new(products);
        Basket basket = new(warehouse);

        basket.Scan("A0001");

        Assert.Equal(12.99m, basket.Total);
    }

    [Fact]
    public void Scan_TotalMany_ReturnsPriceSum()
    {
        List<Product> products = [new Product { Id = "A0001", Price = 12.99m }, new Product { Id = "A0002", Price = 3.99m }];
        Warehouse warehouse = new(products);
        Basket basket = new(warehouse);

        basket.Scan("A0001");
        basket.Scan("A0002");
        basket.Scan("A0003");

        Assert.Equal(12.99m + 3.99m, basket.Total);
    }

    [Fact]
    public void Scan_TotalNone_ReturnsPriceSum()
    {
        List<Product> products = [new Product { Id = "A0001", Price = 12.99m }, new Product { Id = "A0002", Price = 3.99m }];
        Warehouse warehouse = new(products);
        Basket basket = new(warehouse);

        Assert.Equal(0m, basket.Total);
    }

    [Fact]
    public void Scan_WithDiscount_ReturnsPriceSum()
    {
        List<Product> products = [new Product { Id = "A0001", Price = 12.99m, Discount = 0.1m }, new Product { Id = "A0002", Price = 3.99m }];
        Warehouse warehouse = new(products);
        Basket basket = new(warehouse);

        basket.Scan("A0002");
        basket.Scan("A0001");
        basket.Scan("A0002");

        Assert.Equal(19.67m, basket.Total);
    }

    [Fact]
    public void Scan_WithBuy1Get1Free_ReturnsPriceSum()
    {
        List<Product> products = [new Product { Id = "A0001", Price = 12.99m }, new Product { Id = "A0002", Price = 3.99m, Buy1Get1Free = true }];
        Warehouse warehouse = new(products);
        Basket basket = new(warehouse);

        basket.Scan("A0002");
        basket.Scan("A0001");
        basket.Scan("A0002");

        Assert.Equal(16.98m, basket.Total);
    }

    [Fact]
    public void Scan_WithBuy1Get1FreeAndDiscount_ReturnsPriceSum()
    {
        List<Product> products = [new Product { Id = "A0001", Price = 12.99m, Discount = 0.1m }, new Product { Id = "A0002", Price = 3.99m, Buy1Get1Free = true }];
        Warehouse warehouse = new(products);
        Basket basket = new(warehouse);

        basket.Scan("A0002");
        basket.Scan("A0001");
        basket.Scan("A0002");

        Assert.Equal(15.68m, basket.Total);
    }

}