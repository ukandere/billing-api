// -----------------------------------------------------------------------
// <copyright file="PaymentGatewayNotFoundException.cs" company="XYZ Inc">
// Copyright (c) XYZ Inc. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace BillingService.Exceptions
{
    using System;

    /// <summary>
    /// PaymentGatewayNotFoundException to be thrown when implementation for requested payment gateway has not been found.
    /// </summary>
    public class PaymentGatewayNotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentGatewayNotFoundException" /> class.
        /// </summary>
        public PaymentGatewayNotFoundException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentGatewayNotFoundException" /> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public PaymentGatewayNotFoundException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentGatewayNotFoundException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner exception.</param>
        public PaymentGatewayNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
