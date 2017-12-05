namespace Cookie.API.Protocol.Network.Types.Game.Context.Roleplay
{
    using Utils.IO;

    public class HumanOptionOrnament : HumanOption
    {
        public new const ushort ProtocolId = 411;
        public override ushort TypeID => ProtocolId;
        public ushort OrnamentId { get; set; }
        public ushort Level { get; set; }

        public HumanOptionOrnament(ushort ornamentId, ushort level)
        {
            OrnamentId = ornamentId;
            Level = level;
        }

        public HumanOptionOrnament() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarUhShort(OrnamentId);
            writer.WriteVarUhShort(Level);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            OrnamentId = reader.ReadVarUhShort();
            Level = reader.ReadVarUhShort();
        }

    }
}
