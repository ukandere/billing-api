// -----------------------------------------------------------------------
// <copyright file="Receipt.cs" company="XYZ Inc">
//   Copyright (c) XYZ Inc. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace BillingService.Models
{
    using System;

    /// <summary>
    /// Class describing purchase receipt.
    /// </summary>
    public class Receipt
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Receipt"/> class.
        /// </summary>
        /// <param name="orderNumber"> The reference order number.</param>
        /// <param name="amount">The paid amount.</param>
        public Receipt(string orderNumber, decimal amount)
        {
            this.ReferenceOrderNumber = orderNumber;
            this.AmountPaid = amount;
            this.Date = DateTime.Now;
        }

        /// <summary>
        /// Gets the reference order number.
        /// </summary>
        public string ReferenceOrderNumber { get; }

        /// <summary>
        /// Gets the amount paid.
        /// </summary>
        public decimal AmountPaid { get; }

        /// <summary>
        /// Gets the current date and time.
        /// </summary>
        public DateTime Date { get; }
    }
}
