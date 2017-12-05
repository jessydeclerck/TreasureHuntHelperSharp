namespace Cookie.API.Protocol.Network.Messages.Game.Context.Roleplay
{
    using Utils.IO;

    public class ChangeMapMessage : NetworkMessage
    {
        public const ushort ProtocolId = 221;
        public override ushort MessageID => ProtocolId;
        public double MapId { get; set; }
        public bool Autopilot { get; set; }

        public ChangeMapMessage(double mapId, bool autopilot)
        {
            MapId = mapId;
            Autopilot = autopilot;
        }

        public ChangeMapMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteDouble(MapId);
            writer.WriteBoolean(Autopilot);
        }

        public override void Deserialize(IDataReader reader)
        {
            MapId = reader.ReadDouble();
            Autopilot = reader.ReadBoolean();
        }

    }
}
