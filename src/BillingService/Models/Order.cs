// -----------------------------------------------------------------------
// <copyright file="Order.cs" company="XYZ Inc">
//   Copyright (c) XYZ Inc. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace BillingService.Models
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Class describing purchase order details.
    /// </summary>
    public class Order
    {
        /// <summary>
        /// Gets or sets the order number.
        /// </summary>
        [Required]
        public string OrderNumber { get; set; }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        [Required]
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the amount.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the payment gateway.
        /// </summary>
        [Required]
        public string PaymentGateway { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }
    }
}
