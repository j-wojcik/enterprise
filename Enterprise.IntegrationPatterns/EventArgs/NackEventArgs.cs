
namespace Enterprise.IntegrationPatterns
{
    public class NackEventArgs : AckEventArgs
    {
        public bool Requeue { get; set; }
    }
}
