namespace Cookie.API.Protocol.Network.Types.Game.Character
{
    using Utils.IO;

    public class CharacterMinimalInformations : CharacterBasicMinimalInformations
    {
        public new const ushort ProtocolId = 110;
        public override ushort TypeID => ProtocolId;
        public ushort Level { get; set; }

        public CharacterMinimalInformations(ushort level)
        {
            Level = level;
        }

        public CharacterMinimalInformations() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarUhShort(Level);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Level = reader.ReadVarUhShort();
        }

    }
}
