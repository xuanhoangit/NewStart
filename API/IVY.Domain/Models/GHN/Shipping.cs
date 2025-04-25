namespace IVY.Domain.Models.GHN;
public class Shipping
{
    public required int OrderId { get; set; }
    public required string RequiredNote { get; set; }
    public required int ServiceTypeId { get; set; }
    public required int PaymentTypeId { get; set; }
}