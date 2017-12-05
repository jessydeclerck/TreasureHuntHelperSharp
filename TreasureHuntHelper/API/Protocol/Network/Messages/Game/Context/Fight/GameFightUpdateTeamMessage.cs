namespace Cookie.API.Protocol.Network.Messages.Game.Context.Fight
{
    using Types.Game.Context.Fight;
    using Utils.IO;

    public class GameFightUpdateTeamMessage : NetworkMessage
    {
        public const ushort ProtocolId = 5572;
        public override ushort MessageID => ProtocolId;
        public ushort FightId { get; set; }
        public FightTeamInformations Team { get; set; }

        public GameFightUpdateTeamMessage(ushort fightId, FightTeamInformations team)
        {
            FightId = fightId;
            Team = team;
        }

        public GameFightUpdateTeamMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUhShort(FightId);
            Team.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            FightId = reader.ReadVarUhShort();
            Team = new FightTeamInformations();
            Team.Deserialize(reader);
        }

    }
}
