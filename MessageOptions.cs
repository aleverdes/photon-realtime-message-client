using ExitGames.Client.Photon;
using Photon.Realtime;

namespace AleVerDes.PhotonRealtimeMessages
{
    public struct MessageOptions
    {
        public ReceiverGroup ReceiverGroup;
        public EventCaching EventCaching;
        public SendOptions SendOptions;

        public static MessageOptions ToOthers => new()
        {
            ReceiverGroup = ReceiverGroup.Others,
            EventCaching = EventCaching.DoNotCache,
            SendOptions = SendOptions.SendReliable
        };
        
        public static MessageOptions ToAll => new()
        {
            ReceiverGroup = ReceiverGroup.Others,
            EventCaching = EventCaching.DoNotCache,
            SendOptions = SendOptions.SendReliable
        };

        public static MessageOptions ToOthersAndCache => new()
        {
            ReceiverGroup = ReceiverGroup.Others,
            EventCaching = EventCaching.AddToRoomCache,
            SendOptions = SendOptions.SendReliable
        };
        
        public static MessageOptions ToAllAndCache => new()
        {
            ReceiverGroup = ReceiverGroup.Others,
            EventCaching = EventCaching.AddToRoomCache,
            SendOptions = SendOptions.SendReliable
        };
    }
}