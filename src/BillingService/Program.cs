// -----------------------------------------------------------------------
// <copyright file="Program.cs" company="XYZ Inc">
//   Copyright (c) XYZ Inc. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace BillingService
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Hosting;

    /// <summary>
    /// BillingService app for submitting order details, processing payment and receiving payment receipt.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">Input arguments.</param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Creates and configures a host builder object.
        /// </summary>
        /// <param name="args">The command line args.</param>
        /// <returns>The <see cref="IHostBuilder"/>.</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
