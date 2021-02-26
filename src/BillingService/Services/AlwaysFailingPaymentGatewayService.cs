// -----------------------------------------------------------------------
// <copyright file="AlwaysFailingPaymentGatewayService.cs" company="XYZ Inc">
//   Copyright (c) XYZ Inc. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace BillingService.Services
{
    using System.Threading.Tasks;

    using BillingService.Interfaces;
    using BillingService.Models;

    /// <summary>
    /// Dummy PaymentGateway service that always returns false for whether the payment has succeeded.
    /// </summary>
    public class AlwaysFailingPaymentGatewayService : IPaymentGatewayService
    {
        /// <summary>
        /// Processes payment order.
        /// </summary>
        /// <param name="orderDetails">The order details.</param>
        /// <returns>A task that represents the asynchronous payment processing operation. The task result contains whether the payment has succeed.</returns>
        public Task<bool> ProcessPayment(Order orderDetails)
        {
            return Task.FromResult(false);
        }
    }
}
