using ExitGames.Client.Photon;
using Photon.Realtime;

namespace AffenCode
{
    public struct MessageOptions
    {
        public ReceiverGroup ReceiverGroup;
        public EventCaching EventCaching;
        public SendOptions SendOptions;

        public static MessageOptions Default => new MessageOptions()
        {
            ReceiverGroup = ReceiverGroup.Others,
            EventCaching = EventCaching.DoNotCache,
            SendOptions = SendOptions.SendReliable
        };
    }
}