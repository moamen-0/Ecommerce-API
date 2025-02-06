using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Core.IServices;
using Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
	public class OrderService : IOrderService
	{
		private readonly IBasketRepository _basketRepo;

		private readonly IUnitOfWork _unitOfWork;


		public OrderService(
	IBasketRepository basketRepo,
			
			IUnitOfWork unitOfWork)
		{
			_basketRepo = basketRepo;
		
			_unitOfWork = unitOfWork;
		}
		public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethod, string basketId, Address shippingAddress)
		{
			// get basket from the repo
			var basket = await _basketRepo.GetBasketAsync(basketId);

			// get items from the product repo
			var items = new List<OrderItem>();

			foreach (var item in basket.Items)
			{
				var productItem = await _unitOfWork.Repository<Product>().GetAsync(item.Id);
				var itemOrdered = new ProductItemOrder(productItem.Id, productItem.Name, productItem.PictureUrl);
				var orderItem = new OrderItem(itemOrdered, productItem.Price, item.Quantity);
				items.Add(orderItem);
			}
			var deliveryMethodEntity = await _unitOfWork.Repository<DeliveryMethod>().GetAsync(deliveryMethod);

			// calculate subtotal
			var subtotal = items.Sum(item => item.Price * item.Quantity);

			// create order
			var order = new Order(items, buyerEmail, shippingAddress, deliveryMethodEntity, subtotal);
			_unitOfWork.Repository<Order>().Add(order);
			var result = await _unitOfWork.Complete();
			var basketDeleted = await _basketRepo.DeleteBasketAsync(basketId);
			if (result <= 0 || !basketDeleted) return null;

			return order;


		}

		public Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
		{
			var spec = new OrdersWithItemsAndOrderingSpecification(buyerEmail);
			return _unitOfWork.Repository<Order>().ListAsync(spec);

		}
		public Task<Order> GetOrderByIdAsync(int id, string buyerEmail)
		{
			var spec = new OrdersWithItemsAndOrderingSpecification(id, buyerEmail);
			return _unitOfWork.Repository<Order>().GetWithSpecAsync(spec);
		}
		public Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
		{
			return _unitOfWork.Repository<DeliveryMethod>().ListAllAsync();
		}
		
	}
}
