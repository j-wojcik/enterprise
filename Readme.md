# Itegration patterns

## Code samples


### Send via channel
To send message via channel you must just pick destination address (broker/andpoint) and use method Send in generic sender.

```c#
using (var sender = new Sender<MyClass>(_factory, _converter, "myBroker", "publishEndpoint"))
{
    sender.Send(myClassInstance);
}
```

### Receive from channel
Messages are received by generic receivers that accepts instance of your worker (class that Handle message). Your worker class must implement IWorker interface.
```c#
using (var receiver = new Receiver<MyClass>(worker, _factory, _converter, "integrationBroker", "integrationTestQueue"))
{
                receiver.StartListening();
}	
```


```c#
class Worker : IWorker<MyClass>
{
    public event EventHandler<AckEventArgs> JobDone;
    public event EventHandler<NackEventArgs> JobFailed;

    public void DoJob(Message<MyClass> message)
    {
          //your stuff here     
    }
}
```
### Publish/Subscribe

```c#
using (var sender = new Sender<MyClass>(_factory, _converter, "integrationBroker", "publishTestExchange"))
{
    sender.Send(myClassInstance);
}
```

### Work balancer

## Configuation

### Config section

```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <configSections>
    <section name="EnterpriseIntegration" type="Enterprise.IntegrationPatterns.RabbitMq.Configuration.EnterpriseIntegration, Enterprise.IntegrationPatterns.RabbitMq" />
  </configSections>
  <EnterpriseIntegration>
    <brokers>
      <broker key="configTestBroker" address="localhost" virtualHost="test" username="testUser" password="testPass"/>
    </brokers>
    <queues>
      <queue key="configTestQueue" name="test" durable="true" exclusive="true" autoDelete="true"/>
      <queue key="integrationTestQueue" name="inegrationTests" durable="true" autoDelete="false"/>
      <queue key="worloadTestQueue" name="workloadTests" durable="true" autoDelete="false"/>
      <queue key="subscriberOneTestQueue" name="subscriberOne" durable="true" autoDelete="false"/>
      <queue key="subscriberTwoTestQueue" name="subscriberTwo" durable="true" autoDelete="false"/>
      <queue key="configTestDefaultQueue" name="defaultTest"/>
    </queues>
    <exchanges>
      <exchange key="configTestExchange" name="test" type="fanout"/>
      <exchange key="publishTestExchange" durable="true" name="publishTest" type="fanout"/>
    </exchanges>
  </EnterpriseIntegration>
</configuration>
```
### Broker

```xml
<broker key="configTestBroker" address="localhost" virtualHost="test" username="testUser" password="testPass"/>
```
### Queue
```xml
<queue key="configTestQueue" name="test" durable="true" exclusive="true" autoDelete="true"/>
```
### Exchange
```xml
<exchange key="publishTestExchange" durable="true" name="publishTest" type="fanout"/>
```
