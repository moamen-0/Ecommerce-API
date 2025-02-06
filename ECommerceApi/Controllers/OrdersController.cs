using Core.Entities.OrderAggregate;
using Core.IServices;
using ECommerceApi.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ECommerceApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class OrdersController : ControllerBase
	{
		private readonly IOrderService _orderService;
		public OrdersController(IOrderService orderService)
		{
			_orderService = orderService;
		}
		[HttpPost]
		public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto)
		{
			var email = HttpContext.User.FindFirstValue(ClaimTypes.Email);
			var address = new Address(orderDto.ShipToAddress.FirstName, orderDto.ShipToAddress.LastName, orderDto.ShipToAddress.Street, orderDto.ShipToAddress.City, orderDto.ShipToAddress.State, orderDto.ShipToAddress.ZipCode);
			var order = await _orderService.CreateOrderAsync(email, orderDto.DeliveryMethodId, orderDto.BasketId, address);
			if (order == null) return BadRequest();
			return Ok(order);
		}
		[HttpGet]
		public async Task<ActionResult<IReadOnlyList<Order>>> GetOrdersForUser()
		{
			var email = HttpContext.User.FindFirstValue(ClaimTypes.Email);
			var orders = await _orderService.GetOrdersForUserAsync(email);
			return Ok(orders);
		}
		[HttpGet("{id}")]
		public async Task<ActionResult<Order>> GetOrderById(int id)
		{
			var email = HttpContext.User.FindFirstValue(ClaimTypes.Email);
			var order = await _orderService.GetOrderByIdAsync(id, email);
			if (order == null) return NotFound();
			return order;
		}
		[HttpGet("deliveryMethods")]
		public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
		{
			return Ok(await _orderService.GetDeliveryMethodsAsync());
		}



	}
}
