namespace Cookie.API.Protocol.Network.Messages.Game.Context.Fight
{
    using Utils.IO;

    public class GameFightRemoveTeamMemberMessage : NetworkMessage
    {
        public const ushort ProtocolId = 711;
        public override ushort MessageID => ProtocolId;
        public ushort FightId { get; set; }
        public byte TeamId { get; set; }
        public double CharId { get; set; }

        public GameFightRemoveTeamMemberMessage(ushort fightId, byte teamId, double charId)
        {
            FightId = fightId;
            TeamId = teamId;
            CharId = charId;
        }

        public GameFightRemoveTeamMemberMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUhShort(FightId);
            writer.WriteByte(TeamId);
            writer.WriteDouble(CharId);
        }

        public override void Deserialize(IDataReader reader)
        {
            FightId = reader.ReadVarUhShort();
            TeamId = reader.ReadByte();
            CharId = reader.ReadDouble();
        }

    }
}
