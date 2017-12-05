namespace Cookie.API.Protocol.Network.Messages.Game.Context.Fight
{
    using Utils.IO;

    public class GameFightStartingMessage : NetworkMessage
    {
        public const ushort ProtocolId = 700;
        public override ushort MessageID => ProtocolId;
        public byte FightType { get; set; }
        public ushort FightId { get; set; }
        public double AttackerId { get; set; }
        public double DefenderId { get; set; }

        public GameFightStartingMessage(byte fightType, ushort fightId, double attackerId, double defenderId)
        {
            FightType = fightType;
            FightId = fightId;
            AttackerId = attackerId;
            DefenderId = defenderId;
        }

        public GameFightStartingMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteByte(FightType);
            writer.WriteVarUhShort(FightId);
            writer.WriteDouble(AttackerId);
            writer.WriteDouble(DefenderId);
        }

        public override void Deserialize(IDataReader reader)
        {
            FightType = reader.ReadByte();
            FightId = reader.ReadVarUhShort();
            AttackerId = reader.ReadDouble();
            DefenderId = reader.ReadDouble();
        }

    }
}
