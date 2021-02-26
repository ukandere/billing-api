// -----------------------------------------------------------------------
// <copyright file="IPaymentGatewaySelectionService.cs" company="XYZ Inc">
//   Copyright (c) XYZ Inc. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace BillingService.Interfaces
{
    /// <summary>
    /// Interface for PaymentGatewaySelection services.
    /// </summary>
    public interface IPaymentGatewaySelectionService
    {
        /// <summary>
        /// The finds and maps PaymentGateway to corresponding PaymentGateway service.
        /// </summary>
        /// <param name="paymentGatewayName">The payment gateway name.</param>
        /// <returns>The corresponding <see cref="IPaymentGatewayService"/>.</returns>
        IPaymentGatewayService FindPaymentGateway(string paymentGatewayName);
    }
}
