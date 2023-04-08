namespace FinalMS.Discount.DTOs.Create;

public class DiscountCreateDto
{
    public string UserId { get; set; }
    public int Rate { get; set; }
    public string Code { get; set; }
    public DateTime CreatedDate { get; set; }
}
