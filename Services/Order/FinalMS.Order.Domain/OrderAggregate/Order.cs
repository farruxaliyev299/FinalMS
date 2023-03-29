﻿using FinalMS.Order.Domain.Core;

namespace FinalMS.Order.Domain.OrderAggregate;

public class Order: Entity, IAggregateRoot
{
    public DateTime CreatedDate { get; private set; }
    public Address Address { get; private set; }
    public string BuyerId { get; private set; }

    private readonly List<OrderItem> _orderItems;
    public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

    public Order()
    {
        
    }

    public Order(string buyerId, Address address)
    {
        _orderItems = new List<OrderItem>();
        CreatedDate = DateTime.Now;
        BuyerId = buyerId;
        Address = address;
    }

    public void AddOrderItem(string productId, string productName, decimal price, int productQuantity, string pictureUrl)
    {
        var existProduct = _orderItems.Find(x => x.ProductId == productId);

        if (existProduct is null)
        {
            var newOrderItem = new OrderItem(productId, productName, pictureUrl, productQuantity, price);
            _orderItems.Add(newOrderItem);
        }
    }

    public decimal GetTotalPrice => _orderItems.Sum(x => x.TotalPrice);
}