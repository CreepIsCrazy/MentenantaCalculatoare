namespace Calculatoare.Shared.Order;

public class Item
{
    public int ItemId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; }
    public byte[] PictureURL { get; set; }
}
