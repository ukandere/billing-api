// -----------------------------------------------------------------------
// <copyright file="PaymentGatewaySelectionService.cs" company="XYZ Inc">
//   Copyright (c) XYZ Inc. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace BillingService.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using BillingService.Exceptions;
    using BillingService.Interfaces;

    /// <summary>
    /// The PaymentGateway selection service.
    /// </summary>
    public class PaymentGatewaySelectionService : IPaymentGatewaySelectionService
    {
        /// <summary>
        /// The dictionary containing currently supported payment gateways.
        /// </summary>
        private readonly Dictionary<string, IPaymentGatewayService> supportedPaymentGateways;

        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentGatewaySelectionService"/> class.
        /// </summary>
        public PaymentGatewaySelectionService()
        {
            // Get all implementations of PaymentGateway services using reflection.
            this.supportedPaymentGateways = new Dictionary<string, IPaymentGatewayService>();
            var type = typeof(IPaymentGatewayService);
            var supportedTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p) && !p.IsInterface && !p.IsAbstract);

            foreach (var t in supportedTypes)
            {
                this.supportedPaymentGateways.Add(t.Name, (IPaymentGatewayService)Activator.CreateInstance(t));
            }
        }

        /// <summary>
        /// The finds and maps PaymentGateway to corresponding PaymentGateway service.
        /// </summary>
        /// <param name="paymentGatewayName">The payment gateway name.</param>
        /// <returns>The corresponding <see cref="IPaymentGatewayService"/>.</returns>
        /// <exception cref="PaymentGatewayNotFoundException">Thrown when given payment gateway name doesn't match any supported implementations.</exception>
        public IPaymentGatewayService FindPaymentGateway(string paymentGatewayName)
        {
            var gatewayExists = this.supportedPaymentGateways.TryGetValue(paymentGatewayName, out var paymentGateway);
            if (!gatewayExists)
            {
                throw new PaymentGatewayNotFoundException();
            }

            return paymentGateway;
        }
    }
}
