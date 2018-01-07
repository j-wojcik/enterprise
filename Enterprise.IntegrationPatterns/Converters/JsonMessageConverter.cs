using System;
using System.Text;

namespace Enterprise.IntegrationPatterns.Converters
{
    public class JsonMessageConverter : IMessageConverter
    {
        Newtonsoft.Json.JsonSerializerSettings settings = 
            new Newtonsoft.Json.JsonSerializerSettings() {  ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Error };

        public TMessage Deserialize<TMessage>(byte[] message)
        {
            var json = Encoding.UTF8.GetString(message, 0, message.Length);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<TMessage>(json, settings);
        }

        public byte[] Serialize<TMessage>(TMessage message)
        {
            string messageJson = Newtonsoft.Json.JsonConvert.SerializeObject(message, settings);
            return Encoding.UTF8.GetBytes(messageJson);
        }
    }
}
