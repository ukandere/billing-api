// -----------------------------------------------------------------------
// <copyright file="OrdersController.cs" company="XYZ Inc">
//   Copyright (c) XYZ Inc. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

#nullable enable
namespace BillingService.Controllers
{
    using System.Threading.Tasks;

    using BillingService.Exceptions;
    using BillingService.Interfaces;
    using BillingService.Models;

    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// The OrdersController is responsible for processing purchase orders and generating the response receipt.
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        /// <summary>
        /// The PaymentGatewaySelection service for mapping payment gateway.
        /// </summary>
        private readonly IPaymentGatewaySelectionService paymentPaymentGatewaySelectionService;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrdersController"/> class.
        /// </summary>
        /// <param name="paymentPaymentGatewaySelectionService">The PaymentGatewaySelection service.</param>
        public OrdersController(IPaymentGatewaySelectionService paymentPaymentGatewaySelectionService)
        {
            this.paymentPaymentGatewaySelectionService = paymentPaymentGatewaySelectionService;
        }

        /// <summary>
        /// Processes order and returns payment receipt.
        /// </summary>
        /// <param name="orderDetails">The order details.</param>
        /// <returns>A task that represents the asynchronous payment processing operation. The task result contains instance of Receipt wrapped in ActionResult.</returns>
        [HttpPost]
        [Route("/purchases")]
        public async Task<ActionResult<Receipt>> Post([FromBody] Order orderDetails)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var error = this.ValidateOrderDetails(orderDetails);
            if (error != null)
            {
                return this.BadRequest(error);
            }

            IPaymentGatewayService paymentGateway;

            try
            {
                paymentGateway = this.paymentPaymentGatewaySelectionService.FindPaymentGateway(orderDetails.PaymentGateway);
            }
            catch (PaymentGatewayNotFoundException)
            {
                return this.NotFound("PaymentGatewayNotFound");
            }

            var paymentSucceeded = await paymentGateway.ProcessPayment(orderDetails);

            return paymentSucceeded ? new ActionResult<Receipt>(new Receipt(orderNumber: orderDetails.OrderNumber, amount: orderDetails.Amount)) : this.StatusCode(503);
        }

        /// <summary>
        /// The validate order details.
        /// </summary>
        /// <param name="orderDetails">The order details.</param>
        /// <returns> Returns validation error message as string on failed validation, otherwise returns null.</returns>
        private string? ValidateOrderDetails(Order orderDetails)
        {
            if (string.IsNullOrWhiteSpace(orderDetails.OrderNumber))
            {
                return $"{nameof(orderDetails.OrderNumber)} not set.";
            }

            if (string.IsNullOrWhiteSpace(orderDetails.UserId))
            {
                return $"{nameof(orderDetails.OrderNumber)} not set.";
            }

            if (string.IsNullOrWhiteSpace(orderDetails.PaymentGateway))
            {
                return $"{nameof(orderDetails.OrderNumber)} not set.";
            }

            if (orderDetails.Amount < 0)
            {
                return $"Invalid {nameof(orderDetails.OrderNumber)}.";
            }

            return null;
        }
    }
}
