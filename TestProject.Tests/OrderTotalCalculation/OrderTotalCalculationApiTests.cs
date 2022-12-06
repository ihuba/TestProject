using FluentAssertions;
using FluentAssertions.Execution;
using NUnit.Framework;
using TestProject.Client.Builders;
using TestProject.Client.Models.Order;

namespace TestProject.Tests.OrderTotalCalculation
{
    [TestFixture, Parallelizable(ParallelScope.All)]
    public class OrderTotalCalculationApiTests : BaseApiTest
    {
        private List<int> _testOrderIds = new();

        [Test]
        [Description("Scenario: Calculation Of Order Without Discount")]
        
        public void VerifyCalculationOfOrderTotalWithoutDiscount()
        {
            var order = Builders.OrderBuilders.AddGuestsOrderBuilder
                .WithDrinksWithoutDiscount(1).WithDrinksWithDiscount(0).WithMainsCount(1).WithStartersCount(1)
                .AddGuestsOrder()
                .WithDrinksWithoutDiscount(1).WithDrinksWithDiscount(0).WithMainsCount(1).WithStartersCount(1)
                .AddGuestsOrder()
                .WithDrinksWithoutDiscount(1).WithDrinksWithDiscount(0).WithMainsCount(1).WithStartersCount(1)
                .AddGuestsOrder()
                .WithDrinksWithoutDiscount(1).WithDrinksWithDiscount(0).WithMainsCount(1).WithStartersCount(1).Build();

            var expectedTotal = CalculateOrdersTotal(order);
            var orderReceipt = TestProjectClient.OrderActions.PostRequestOrderCalculation(order);
            _testOrderIds.Add(orderReceipt.OrderId);

            var orderTotal = orderReceipt.OrderTotal;

            orderTotal.Should().Be(expectedTotal);
        }

        [Test]
        [Description("Scenario: Calculation Of Order With Discount")]
        public void VerifyCalculationOfOrderTotalWithDiscount()
        {
            var orderBuilder = Builders.OrderBuilders.AddGuestsOrderBuilder
                .WithDrinksWithDiscount(1).WithDrinksWithoutDiscount(0).WithMainsCount(1).WithStartersCount(0)
                .AddGuestsOrder()
                .WithDrinksWithDiscount(1).WithDrinksWithoutDiscount(0).WithMainsCount(1).WithStartersCount(1);
            var orderOfTwoGuests = orderBuilder.Build();

            var expectedTotalOfOrderWithDiscountOfTwoGuests = CalculateOrdersTotal(orderOfTwoGuests);
            var orderReceiptForTwoGuests = TestProjectClient.OrderActions.PostRequestOrderCalculation(orderOfTwoGuests);
            _testOrderIds.Add(orderReceiptForTwoGuests.OrderId);

            var updatedOrder = orderBuilder.AddGuestsOrder()
                .WithMainsCount(1).WithStartersCount(0).WithDrinksWithoutDiscount(1).WithDrinksWithDiscount(0)
                .AddGuestsOrder()
                .WithMainsCount(1).WithStartersCount(0).WithDrinksWithoutDiscount(1).WithDrinksWithDiscount(0).Build();
            var orderReceiptForAllGuests = TestProjectClient.OrderActions.PostRequestOrderCalculation(updatedOrder);

            var expectedTotalOfOrderForAllGuests = CalculateOrdersTotal(updatedOrder);

            using var _ = new AssertionScope();
            orderReceiptForTwoGuests.OrderTotal.Should().Be(expectedTotalOfOrderWithDiscountOfTwoGuests);
            orderReceiptForAllGuests.OrderTotal.Should().Be(expectedTotalOfOrderForAllGuests);
        }

        [Test]
        [Description("{TestCaseId}")]
        public void VerifyCalculationOfOrderTotalWithCancellation()
        {
            var orderBuilder = Builders.OrderBuilders.AddGuestsOrderBuilder
                .WithDrinksWithDiscount(0).WithDrinksWithoutDiscount(1).WithMainsCount(1).WithStartersCount(1)
                .AddGuestsOrder()
                .WithDrinksWithDiscount(0).WithDrinksWithoutDiscount(1).WithMainsCount(1).WithStartersCount(1)
                .AddGuestsOrder()
                .WithDrinksWithDiscount(0).WithDrinksWithoutDiscount(1).WithMainsCount(1).WithStartersCount(1)
                .AddGuestsOrder()
                .WithDrinksWithDiscount(0).WithDrinksWithoutDiscount(1).WithMainsCount(1).WithStartersCount(1);
            var orderOfFourGuests = orderBuilder.Build();

            var expectedTotalOfOrderWithoutDiscountOfFourGuests = CalculateOrdersTotal(orderOfFourGuests);
            var orderReceiptForFourGuests =
                TestProjectClient.OrderActions.PostRequestOrderCalculation(orderOfFourGuests);
            _testOrderIds.Add(orderReceiptForFourGuests.OrderId);

            var orderAfterCancellation = Builders.OrderBuilders.AddGuestsOrderBuilder
                .AddOrdersList(orderOfFourGuests.GuestsOrderModels.Take(orderOfFourGuests.GuestsOrderModels.Count - 1)
                    .ToList()).Build();
            var orderReceiptAfterCancellation =
                TestProjectClient.OrderActions.PostRequestOrderCalculation(orderAfterCancellation);

            var expectedTotalAfterCancellation = CalculateOrdersTotal(orderAfterCancellation);

            using var _ = new AssertionScope();
            orderReceiptForFourGuests.OrderTotal.Should().Be(expectedTotalOfOrderWithoutDiscountOfFourGuests);
            orderReceiptAfterCancellation.OrderTotal.Should().Be(expectedTotalAfterCancellation);
        }

        [OneTimeTearDown]
        public void DeleteTestOrders()
        {
            foreach (var orderId in _testOrderIds)
            {
                //TestProjectClient.OrderActions.TestActions.DeleteOrder(orderId);
            }
        }

        private static decimal CalculateOrdersTotal(RequestCalculateOrderTotalModel orderModel)
        {
            decimal total = 0;
            const int startersCost = 4;
            const int mainsCost = 7;
            const decimal drinksCost = 2.5m;
            const decimal drinksDiscountPercentage = 0.3m;

            foreach (var order in orderModel.GuestsOrderModels)
            {
                total += (order.StartersCount * startersCost
                          + order.MainsCount * mainsCost
                          + order.DrinksWithoutDiscountCount * drinksCost
                          + order.DrinksWithDiscountCount * (drinksCost * (1 - drinksDiscountPercentage)));
            }

            return total;
        }
    }
}