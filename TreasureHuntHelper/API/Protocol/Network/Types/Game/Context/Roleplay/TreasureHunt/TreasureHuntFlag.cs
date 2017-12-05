namespace Cookie.API.Protocol.Network.Types.Game.Context.Roleplay.TreasureHunt
{
    using Utils.IO;

    public class TreasureHuntFlag : NetworkType
    {
        public const ushort ProtocolId = 473;
        public override ushort TypeID => ProtocolId;
        public double MapId { get; set; }
        public byte State { get; set; }

        public TreasureHuntFlag(double mapId, byte state)
        {
            MapId = mapId;
            State = state;
        }

        public TreasureHuntFlag() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteDouble(MapId);
            writer.WriteByte(State);
        }

        public override void Deserialize(IDataReader reader)
        {
            MapId = reader.ReadDouble();
            State = reader.ReadByte();
        }

    }
}
