﻿namespace FinalMS.Discount.DTOs.Update;

public class DiscountUpdateDto
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public int Rate { get; set; }
    public string Code { get; set; }
    public DateTime CreatedDate { get; set; }
}
