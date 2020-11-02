using MessageBus.Extensions;
using MessageBus.Utils;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MessageBus.UnitTests
{
    public class EncodingTests
    {
        
        [Test]
        public void Should_Convert_String_To_Byte_Array()
        {
            var phrase = "The best code to maintain is the code has never written.";
            Assert.AreEqual(Encoding.UTF8.GetBytes(phrase), phrase.ToByteArray());
        }

        [Test]
        public void Should_Convert_Bytes_To_String()
        {
            var phrase = "The best code to maintain is the code has never written.";
            var bytes = Encoding.UTF8.GetBytes(phrase);
            Assert.AreEqual(bytes, bytes.GetPayloadString());
        }
    }
}
