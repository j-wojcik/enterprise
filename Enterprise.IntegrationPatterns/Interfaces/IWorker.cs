using System;

namespace Enterprise.IntegrationPatterns
{
    public interface IWorker<TMessage>
    {
        void DoJob(TMessage message);
        event EventHandler<AckEventArgs> JobDone;
        event EventHandler<NackEventArgs> JobFailed;
    }
}
