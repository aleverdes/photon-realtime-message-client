using System;
using System.Collections.Generic;
using System.Linq;
using ExitGames.Client.Photon;
using Photon.Realtime;
using UnityEngine;

namespace AffenCode
{
    public class NetworkClient
    {
        private LoadBalancingClient _loadBalancingClient;
        private Dictionary<Type, Queue<object>> _messages;
        private readonly byte _photonEventCode;

        public NetworkClient(LoadBalancingClient loadBalancingClient, byte photonEventCode = 0)
        {
            _loadBalancingClient = loadBalancingClient;
            _loadBalancingClient.EventReceived += OnEventReceived;
            _messages = new Dictionary<Type, Queue<object>>();
            _photonEventCode = photonEventCode;
        }

        ~NetworkClient()
        {
            _loadBalancingClient.EventReceived -= OnEventReceived;
            _loadBalancingClient = null;
            _messages = null;
        }

        public void SendMessage(SerializableMessage message)
        {
            SendMessage(message, MessageOptions.Default);
        }

        public void SendMessage(SerializableMessage message, MessageOptions options)
        {
            var data = new Hashtable()
            {
                [0] = message.Serialize()
            };

            var raiseEventOptions = new RaiseEventOptions
            {
                Receivers = options.ReceiverGroup,
                CachingOption = options.EventCaching
            };
            
            _loadBalancingClient.OpRaiseEvent(_photonEventCode, data, raiseEventOptions, options.SendOptions);
        }

        public bool TryTakeMessage<T>(out T message) where T : SerializableMessage
        {
            if (!_messages.ContainsKey(typeof(T)))
            {
                message = null;
                return false;
            }

            if (_messages[typeof(T)].TryDequeue(out var messageObject))
            {
                message = (T)messageObject;
                return true;
            }

            message = null;
            return false;
        }
        
        private void OnEventReceived(EventData photonEvent)
        {
            if (photonEvent.Code != _photonEventCode)
            {
                return;
            }

            var value = ((Hashtable) photonEvent.Parameters.FirstOrDefault().Value)[0];
            var bytes = (byte[]) value;
            var message = SerializableMessage.Deserialize(bytes);

            if (!_messages.ContainsKey(message.GetType()))
            {
                _messages.Add(message.GetType(), new Queue<object>());
            }
            
            _messages[message.GetType()].Enqueue(message);
        }
    }
}