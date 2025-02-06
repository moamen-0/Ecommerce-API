using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ECommerceApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class PaymentsController : ControllerBase
	{
		private readonly IPaymentService _paymentService;

		public PaymentsController(IPaymentService paymentService)
		{
			_paymentService = paymentService;
		}

		[HttpGet("get-intent")]
		public async Task<ActionResult<CustomerBasket>> GetPaymentIntent([FromQuery] string basketId)
		{
			var basket = await _paymentService.CreateOrUpdatePaymentIntent(basketId);
			return basket ?? new CustomerBasket();
		}

		[HttpPost("update-intent")]
		public async Task<ActionResult<CustomerBasket>> CreateOrUpdatePaymentIntent([FromQuery] string basketId)
		{
			var basket = await _paymentService.CreateOrUpdatePaymentIntent(basketId);
			return basket ?? new CustomerBasket();
		}
	}
}
