namespace Cookie.API.Protocol.Network.Types.Game.Context.Fight
{
    using Utils.IO;

    public class FightTeamMemberCompanionInformations : FightTeamMemberInformations
    {
        public new const ushort ProtocolId = 451;
        public override ushort TypeID => ProtocolId;
        public byte CompanionId { get; set; }
        public ushort Level { get; set; }
        public double MasterId { get; set; }

        public FightTeamMemberCompanionInformations(byte companionId, ushort level, double masterId)
        {
            CompanionId = companionId;
            Level = level;
            MasterId = masterId;
        }

        public FightTeamMemberCompanionInformations() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteByte(CompanionId);
            writer.WriteVarUhShort(Level);
            writer.WriteDouble(MasterId);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            CompanionId = reader.ReadByte();
            Level = reader.ReadVarUhShort();
            MasterId = reader.ReadDouble();
        }

    }
}
