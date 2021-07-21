using System.IO;
using System.Threading.Tasks;
using API.Errors;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Stripe;
using Order = Core.Entities.OrderAggregate.Order;

namespace API.Controllers {
    public class PaymentsController : BaseApiController {
        private readonly IPaymentService _paymentService;
        private readonly string _whSecret;
        private readonly ILogger<PaymentsController> _logger;

        public PaymentsController (IPaymentService paymentService, ILogger<PaymentsController> logger, IConfiguration config) {
            _logger = logger;
            _paymentService = paymentService;
            _whSecret = config.GetSection("StripeSettings:WhSecret").Value;
        }

        [Authorize]
        [HttpPost ("{basketId}")]
        public async Task<ActionResult<CustomerBasket>> CreateOrUpdatePaymentIntent (string basketId) {
            // return await _paymentService.CreateOrUpdatePaymentIntent(basketId);     // chnaged to accommodate PaymentService if null
            var basket = await _paymentService.CreateOrUpdatePaymentIntent (basketId);

            if (basket == null) return BadRequest (new ApiResponse (400, "Problem with your basket"));

            return basket;
        }

        [HttpPost ("webhook")]
        public async Task<ActionResult> StripeWebHook () {
            var json = await new StreamReader (HttpContext.Request.Body).ReadToEndAsync ();
            var stripeEvent = EventUtility.ConstructEvent (json, Request.Headers["Stripe-Signature"], _whSecret);

            PaymentIntent intent;
            Order order; // needs to be our own order not stripe order

            switch (stripeEvent.Type) {
                case "payment_intent.succeeded":
                    intent = (PaymentIntent)stripeEvent.Data.Object;
                    _logger.LogInformation("Payment Succeeded: ", intent.Id);
                    // TODO: update order with new status
                    order = await _paymentService.UpdateOrderPaymentSucceeded(intent.Id);
                    _logger.LogInformation("Order updated to payment received: ", order.Id);
                    break;
                    case "payment_intent.payment_failed":
                    intent = (PaymentIntent)stripeEvent.Data.Object;
                    _logger.LogInformation("Payment Failed: ", intent.Id);
                    // TODO: update order status
                    order = await _paymentService.UpdateOrderPaymentFailed(intent.Id);
                    _logger.LogInformation("Payment Failed: ", order.Id);
                    break;
            }
            return new EmptyResult();
        }
    }
}