using System;

namespace Enterprise.IntegrationPatterns
{
    public class AckEventArgs : EventArgs
    {
        public ulong DeliveryTag { get; set; }
        public bool MultipleAck { get; set; }
    }
}
