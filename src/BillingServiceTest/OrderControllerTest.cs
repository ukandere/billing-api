
namespace BillingServiceTest
{
    using System.Threading.Tasks;

    using BillingService.Controllers;
    using BillingService.Exceptions;
    using BillingService.Interfaces;
    using BillingService.Models;

    using Microsoft.AspNetCore.Mvc;

    using Moq;

    using Xunit;

    public class OrderControllerTest
    {
        private readonly Mock<IPaymentGatewaySelectionService> successfulPaymentGatewaySelectionService;
        private readonly Mock<IPaymentGatewaySelectionService> failingPaymentGatewaySelectionService;
        private readonly Mock<IPaymentGatewaySelectionService> invalidPaymentGatewaySelectionService;
        private readonly OrdersController controller;

        private readonly Order validOrder;

        public OrderControllerTest()
        {
            this.failingPaymentGatewaySelectionService = new Mock<IPaymentGatewaySelectionService>();
            this.successfulPaymentGatewaySelectionService = new Mock<IPaymentGatewaySelectionService>();
            this.invalidPaymentGatewaySelectionService = new Mock<IPaymentGatewaySelectionService>();

            var failingPaymentGateway = new Mock<IPaymentGatewayService>();
            failingPaymentGateway.Setup(f => f.ProcessPayment(It.IsAny<Order>())).Returns(Task.FromResult(false));
            
            var successfulGateway = new Mock<IPaymentGatewayService>();
            successfulGateway.Setup(f => f.ProcessPayment(It.IsAny<Order>())).Returns(Task.FromResult(true));

            this.failingPaymentGatewaySelectionService.Setup(s => s.FindPaymentGateway(It.IsAny<string>())).Returns(failingPaymentGateway.Object);
            this.successfulPaymentGatewaySelectionService.Setup(s => s.FindPaymentGateway(It.IsAny<string>())).Returns(successfulGateway.Object);
            this.invalidPaymentGatewaySelectionService.Setup(s => s.FindPaymentGateway(It.IsAny<string>())).Throws<PaymentGatewayNotFoundException>();

            this.controller = new OrdersController(this.successfulPaymentGatewaySelectionService.Object);
            
            this.validOrder = new Order()
                                  {
                                      OrderNumber = "TestOrder123",
                                      UserId = "TestUser123",
                                      Amount = 1.00M,
                                      PaymentGateway = string.Empty
                                  };
        }

        [Fact]
        public async Task Post_InvalidOrderPassed_OrderNumberNotSet_ReturnsBadRequest()
        {
            var testOrder = new Order();

            this.controller.ModelState.AddModelError(nameof(testOrder.OrderNumber), "Required");
            var response = await this.controller.Post(testOrder);

            Assert.IsType<BadRequestObjectResult>(response.Result);
        }

        [Fact]
        public async Task Post_InvalidOrderPassed_UserIdNotSet_ReturnsBadRequest()
        {
            var testOrder = new Order();

            this.controller.ModelState.AddModelError(nameof(testOrder.UserId), "Required");
            var response = await this.controller.Post(testOrder);

            Assert.IsType<BadRequestObjectResult>(response.Result);
        }

        [Fact]
        public async Task Post_InvalidOrderPassed_PaymentGatewayNotSet_ReturnsBadRequest()
        {
            var testOrder = new Order();

            this.controller.ModelState.AddModelError(nameof(testOrder.PaymentGateway), "Required");
            var response = await this.controller.Post(testOrder);

            Assert.IsType<BadRequestObjectResult>(response.Result);
        }

        [Fact]
        public async Task Post_InvalidOrderPassed_InvalidAmount_ReturnsBadRequest()
        {
            var testOrder = this.validOrder;
            testOrder.Amount = -1.00M;

            var response = await this.controller.Post(testOrder);

            Assert.IsType<BadRequestObjectResult>(response.Result);
        }

        [Fact]
        public async Task Post_InvalidOrderPassed_InvalidPaymentGateway_ReturnsNotFoundResult()
        {
            var testOrder = new Order();
            var controllerWithInvalidPaymentGateway = new OrdersController(this.invalidPaymentGatewaySelectionService.Object);

            var response = await controllerWithInvalidPaymentGateway.Post(testOrder);

            Assert.IsType<NotFoundObjectResult>(response.Result);
        }

        [Fact]
        public async Task Post_ValidOrderPassed_PaymentFailed_ReturnsServiceUnavailable()
        {
            var controllerWithFailingPaymentGateway = new OrdersController(this.failingPaymentGatewaySelectionService.Object);

            var response = await controllerWithFailingPaymentGateway.Post(this.validOrder);
            var result = response.Result as StatusCodeResult;

            Assert.Equal(503, result.StatusCode);
        }

        [Fact]
        public async Task Post_ValidOrderPassed_PaymentSucceeded_ReturnsCreatedResponse()
        {
            var testOrder = this.validOrder;

            var response = await this.controller.Post(testOrder);

            Assert.IsType<ActionResult<Receipt>>(response);
        }

        [Fact]
        public async Task Post_ValidOrderPassed_PaymentSucceeded_ReturnsReceipt()
        {
            var testOrder = this.validOrder;

            var response = await this.controller.Post(testOrder);

            Assert.IsType<Receipt>(response.Value);
        }

        [Fact]
        public async Task Post_ValidOrderPassed_PaymentSucceeded_ReturnedResponseHasCreatedValidReceipt()
        {
            var testOrder = this.validOrder;

            var response = await this.controller.Post(this.validOrder);
            var receipt = response.Value as Receipt;

            Assert.Equal(testOrder.OrderNumber, receipt.ReferenceOrderNumber);
            Assert.Equal(testOrder.Amount, receipt.AmountPaid);
        }
    }
}