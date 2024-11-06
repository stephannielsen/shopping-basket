namespace ShoppingBasket;

public record Product
{
    public required string Id { get; set; }
    public required decimal Price { get; set; }
    public bool Buy1Get1Free { get; set; } = false;
    public decimal Discount { get; set; } = 0m;
}
