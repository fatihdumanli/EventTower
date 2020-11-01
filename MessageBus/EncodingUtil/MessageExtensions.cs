using Newtonsoft.Json;

namespace MessageBus.Extensions
{
    public static class MessageExtensions 
    {
        public static string ToJson(this IMessage message)
        {
            return JsonConvert.SerializeObject(message);
        }
    }
}