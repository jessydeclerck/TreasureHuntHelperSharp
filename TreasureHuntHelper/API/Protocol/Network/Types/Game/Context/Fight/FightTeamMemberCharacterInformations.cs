namespace Cookie.API.Protocol.Network.Types.Game.Context.Fight
{
    using Utils.IO;

    public class FightTeamMemberCharacterInformations : FightTeamMemberInformations
    {
        public new const ushort ProtocolId = 13;
        public override ushort TypeID => ProtocolId;
        public string Name { get; set; }
        public ushort Level { get; set; }

        public FightTeamMemberCharacterInformations(string name, ushort level)
        {
            Name = name;
            Level = level;
        }

        public FightTeamMemberCharacterInformations() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteUTF(Name);
            writer.WriteVarUhShort(Level);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Name = reader.ReadUTF();
            Level = reader.ReadVarUhShort();
        }

    }
}
