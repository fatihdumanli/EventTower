using Newtonsoft.Json.Linq;
using NUnit.Framework;
using SimpleMessageBus;
using SimpleMessageBus.Extensions;
using System.Collections.Generic;

namespace MessageBus.UnitTests
{
    public class MessageSerializationTests
    {
        class BasketItem
        {
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public decimal UnitPrice { get; set; }
        }

        class CreateOrderCommand : ICommand
        {
            public int CustomerId { get; set; }
            public List<BasketItem> OrderItems { get; set; }
        }

        [Test]
        public void Serialization_Test_1()
        {
            var createOrderCommand = new CreateOrderCommand();
            createOrderCommand.CustomerId = 101;

            List<BasketItem> items = new List<BasketItem>()
            {
                new BasketItem() { ProductId = 50, ProductName = "CellPhone", UnitPrice = 1099 },
                new BasketItem() { ProductId = 67, ProductName = "Laptop", UnitPrice = 1490 },
                new BasketItem() { ProductId = 772, ProductName = "Keyboard", UnitPrice = 49 }
            };

            createOrderCommand.OrderItems = items;
            var json = createOrderCommand.ToJson();
            JObject jobject = JObject.Parse(json);
            Assert.IsTrue(jobject["type"].ToString().Equals("CreateOrderCommand"));
            Assert.NotNull(jobject["publishDate"]);
            Assert.NotNull(jobject["payload"]);
            JObject payload = JObject.Parse(jobject["payload"].ToString());
            var customerId = payload["CustomerId"].ToString();
            Assert.IsTrue(customerId.Equals("101"));
            JArray orderItems = JArray.Parse(payload["OrderItems"].ToString());
            Assert.NotNull(orderItems);
            Assert.IsTrue(orderItems.Count.Equals(3));
            Assert.IsTrue(orderItems[0]["ProductId"].ToString().Equals("50"));
            Assert.IsTrue(orderItems[2]["UnitPrice"].ToString().Equals("49"));
        }
    }
}