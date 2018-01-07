# Itegration patterns
Library contains set of classes, that allows to easy integrate your aplications via message queues. It implements many of entities you know from articles/books, about integration (eg. channel, sender, receiver, exchange etc.)

## Code samples


### Send via channel
To send message via channel you must just pick destination address (broker/endpoint) and use method Send in generic sender.

```c#
using (var sender = new Sender<MyClass>(_factory, _converter, "myBroker", "publishEndpoint"))
{
    sender.Send(myClassInstance);
}
```

### Receive from channel
Messages are received by generic receivers that accepts instance of your worker (class that will Handle message). Your worker class must implement IWorker interface.
```c#
using (var receiver = new Receiver<MyClass>(worker, _factory, _converter, "integrationBroker", "integrationTestQueue"))
{
                receiver.StartListening();
}	
```

IWorker interface 
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
To publish just pick configured exchange as your endpont. 
```c#
using (var sender = new Sender<MyClass>(_factory, _converter, "integrationBroker", "publishTestExchange"))
{
    sender.Send(myClassInstance);
}
```

There is no difference in subscribing and receiving for channel (because you are doing the same thing).
```c#
using (var receiver = new Receiver<MyClass>(worker, _factory, _converter, "integrationBroker", "subscribeQueue"))
{
                receiver.StartListening();
}	
```

### Work balancer
To balance your work just send you mesage to balancing queue (exclusive=false). 

```c#
using (var sender = new Sender<MyClass>(_factory, _converter, "integrationBroker", "balacerQueue"))
{
    sender.Send(myClassInstance);
}
```

Connect all workers as clients.
```c#
using (var receiver = new Receiver<MyClass>(worker, _factory, _converter, "integrationBroker", "balacerQueue"))
{
                receiver.StartListening();
}	
```

### RPC Server
To start server that publish your method via queue just create object with endpoint and pass delegate to your method.
```c#
using (var server = new RPCServer<ArgumentType, ReturnType>(_factory, _converter, "integrationBroker", "rpcTestQueue"))
{
                server.Start(YourMethod);
}
```

### RPC Client
To be able to call RPC servers just create  client bject and use Call method.
```c#
using (var client = new RPCClient<ArgumentType, ReturnType>(_factory, _converter, "integrationBroker", "rpcTestQueue"))
{
                server.Call(argumentInstance);
}
```

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
To add enterprise integration config section you must register it in \<configSections>
```xml
<configSections>
    <section name="EnterpriseIntegration" type="Enterprise.IntegrationPatterns.RabbitMq.Configuration.EnterpriseIntegration, Enterprise.IntegrationPatterns.RabbitMq" />
  </configSections>
``` 
After registration you are able to add your wn brokers, queues and exchanges inside \<EnterpriseIntegration>
### Broker
You may unfderstand broker as instance of your AMQP hosting.
```xml
<broker key="configTestBroker" address="localhost" virtualHost="test" username="testUser" password="testPass"/>
```
|PropertyName|Type|Description|
|---|---|---|
|key|string|Uinique key to find your defeinition in configuration collection. This thing you should use in your code to connect.|
|address|string|Your broker address|
|virtualHost|string|Virtual host on your broker that contains our endpoints| 
|username|string|User name|
|password|string|Password|

### Queue
Queue is basic type of channel you may publish to or read messages from. 
```xml
<queue key="configTestQueue" name="test" durable="true" exclusive="true" autoDelete="true"/>
```

|PropertyName|Type|Description|
|---|---|---|
|key|string|Uinique key to find your defeinition in configuration collection. This thing you should use in your code to connect.|
|name|string|Name of queue on broker|
|durable|bool|To set if yur queue is durable| 
|exclusive|bool|To set if yur queue is durable|
|autoDelete|bool|To set if yur queue is autoDelete|
### Exchange
Exhange i more complex type used to publish to it. You may read mode here: https://www.rabbitmq.com/tutorials/amqp-concepts.html
```xml
<exchange key="publishTestExchange" durable="true" name="publishTest" type="fanout"/>
```

|PropertyName|Type|Description|
|---|---|---|
|key|string|Uinique key to find your defeinition in configuration collection. This thing you should use in your code to connect.|
|name|string|Name of exchange on broker|
|durable|bool|To set if yur queue is durable| 
|type|{direct, fanout, topic, header}|Type of you exchange|
