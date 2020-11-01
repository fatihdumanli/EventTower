using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MessageBus.Extensions
{
    public static class MessageExtensions 
    {
        public static string ToJson(this IMessage message)
        {
            JObject o = new JObject();
            o.Add("type", message.GetType().Name);
            o.Add("publishDate", DateTime.UtcNow);
            o.Add("payload", JsonConvert.SerializeObject(message));
            Console.WriteLine(o.ToString());
            return o.ToString();
        }
    }
}