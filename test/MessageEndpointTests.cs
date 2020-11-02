using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MessageBus.UnitTests
{
    class UserEmailChangedEvent : IEvent
    {
        public int UserId { get; set; }
        public string Email { get; set; }
    }

    class Handler : IMessageHandler<UserEmailChangedEvent>
    {
        public static bool isHandled = false;
        public Task Handle(UserEmailChangedEvent message)
        {
            isHandled = true;
            return Task.CompletedTask;
        }
    }


    public class MessageEndpointTests
    {
        [Test]
        public void Should_Not_Be_Initialized_With_Empty_Endpoint_Name()
        {
            Assert.Throws<ArgumentException>(() => SimpleMessageBus.CreateEndpoint(string.Empty));
            Assert.Throws<ArgumentException>(() => SimpleMessageBus.CreateEndpoint(null));
        }

        [Test]
        public void Should_Build_A_Message_Endpoint()
        {
            Assert.DoesNotThrow(() => SimpleMessageBus.CreateEndpoint("test"));
        }

        [Test]
        public void Should_Raise_An_Event()
        {
        }



 
    }
}
