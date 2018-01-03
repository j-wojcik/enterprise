using System;
using System.Text;

namespace Enterprise.IntegrationPatterns.Converters
{
    public class JsonMessageConverter : IMessageConverter
    {
        public TMessage Deserialize<TMessage>(byte[] message)
        {
            var json = Encoding.UTF8.GetString(message, 0, message.Length);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<TMessage>(json);
        }

        public byte[] Serialize<TMessage>(TMessage message)
        {
            string messageJson = Newtonsoft.Json.JsonConvert.SerializeObject(message);
            return Encoding.UTF8.GetBytes(messageJson);
        }
    }
}
