namespace Cookie.API.Protocol.Network.Messages.Game.Context.Fight
{
    using Utils.IO;

    public class GameFightJoinRequestMessage : NetworkMessage
    {
        public const ushort ProtocolId = 701;
        public override ushort MessageID => ProtocolId;
        public double FighterId { get; set; }
        public ushort FightId { get; set; }

        public GameFightJoinRequestMessage(double fighterId, ushort fightId)
        {
            FighterId = fighterId;
            FightId = fightId;
        }

        public GameFightJoinRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteDouble(FighterId);
            writer.WriteVarUhShort(FightId);
        }

        public override void Deserialize(IDataReader reader)
        {
            FighterId = reader.ReadDouble();
            FightId = reader.ReadVarUhShort();
        }

    }
}
