// -----------------------------------------------------------------------
// <copyright file="IPaymentGatewayService.cs" company="XYZ Inc">
//   Copyright (c) XYZ Inc. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace BillingService.Interfaces
{
    using System.Threading.Tasks;

    using BillingService.Models;

    /// <summary>
    /// Interface for PaymentGateways services.
    /// </summary>
    public interface IPaymentGatewayService
    {
        /// <summary>
        /// Processes payment order.
        /// </summary>
        /// <param name="orderDetails">The order details.</param>
        /// <returns>A task that represents the asynchronous payment processing operation. The task result contains whether the payment has succeed.</returns>
        Task<bool> ProcessPayment(Order orderDetails);
    }
}
