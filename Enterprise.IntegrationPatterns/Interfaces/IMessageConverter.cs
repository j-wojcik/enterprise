namespace Enterprise.IntegrationPatterns
{
    public interface IMessageConverter
    {
        byte[] Serialize<TMessage>(TMessage message);
        TMessage Deserialize<TMessage>(byte[] message);
    }
}
