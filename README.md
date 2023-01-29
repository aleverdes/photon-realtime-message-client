# Photon Realtime Messages

NetworkClient running on SerializableMessage

# Requirements

* [Photon Realtime](https://www.photonengine.com/en-us/Realtime)
* [SerializableMessage](https://github.com/aleverdes/serializable-message)

# Usage

```csharp
var networkClient = new NetworkClient(LoadBalancingClient);
networkClient.SendMessage(
    new TestSerializableMessage()
    {
        StringValue = "test"
    }
);

if (networkClient.TryTakeMessage<TestSerializableMessage>(out var message))
{
    Debug.Log(message.StringValue);
}
```