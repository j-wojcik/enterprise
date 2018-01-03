using System;

namespace Enterprise.IntegrationPatterns
{
    public interface ISender<TMessage> : IDisposable
    {
        void Send(TMessage message);
    }
}
