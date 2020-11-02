using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MessageBus.UnitTests
{
    public class MessageEndpointTests
    {
       
        [Test]
        public void Should_Not_Be_Initialized_With_Empty_Endpoint_Name()
        {
            Assert.Throws<ArgumentException>(() => new MessageBusEndpoint(null));
        }

        [Test]
        public void Should_Raise_An_Event()
        {
        }
    }
}
