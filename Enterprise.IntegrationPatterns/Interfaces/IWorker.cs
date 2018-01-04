using Enterprise.IntegrationPatterns.Messages;
using System;

namespace Enterprise.IntegrationPatterns
{
    public interface IWorker<TMessage>
    {
        void DoJob(Message<TMessage> message);
        event EventHandler<AckEventArgs> JobDone;
        event EventHandler<NackEventArgs> JobFailed;
    }
}
