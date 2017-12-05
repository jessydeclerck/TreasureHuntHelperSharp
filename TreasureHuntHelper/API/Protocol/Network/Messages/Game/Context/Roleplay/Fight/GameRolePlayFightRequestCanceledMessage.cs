namespace Cookie.API.Protocol.Network.Messages.Game.Context.Roleplay.Fight
{
    using Utils.IO;

    public class GameRolePlayFightRequestCanceledMessage : NetworkMessage
    {
        public const ushort ProtocolId = 5822;
        public override ushort MessageID => ProtocolId;
        public ushort FightId { get; set; }
        public double SourceId { get; set; }
        public double TargetId { get; set; }

        public GameRolePlayFightRequestCanceledMessage(ushort fightId, double sourceId, double targetId)
        {
            FightId = fightId;
            SourceId = sourceId;
            TargetId = targetId;
        }

        public GameRolePlayFightRequestCanceledMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUhShort(FightId);
            writer.WriteDouble(SourceId);
            writer.WriteDouble(TargetId);
        }

        public override void Deserialize(IDataReader reader)
        {
            FightId = reader.ReadVarUhShort();
            SourceId = reader.ReadDouble();
            TargetId = reader.ReadDouble();
        }

    }
}
