namespace Cookie.API.Protocol.Network.Messages.Game.Context.Roleplay.Fight
{
    using Utils.IO;

    public class GameRolePlayPlayerFightFriendlyRequestedMessage : NetworkMessage
    {
        public const ushort ProtocolId = 5937;
        public override ushort MessageID => ProtocolId;
        public ushort FightId { get; set; }
        public ulong SourceId { get; set; }
        public ulong TargetId { get; set; }

        public GameRolePlayPlayerFightFriendlyRequestedMessage(ushort fightId, ulong sourceId, ulong targetId)
        {
            FightId = fightId;
            SourceId = sourceId;
            TargetId = targetId;
        }

        public GameRolePlayPlayerFightFriendlyRequestedMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUhShort(FightId);
            writer.WriteVarUhLong(SourceId);
            writer.WriteVarUhLong(TargetId);
        }

        public override void Deserialize(IDataReader reader)
        {
            FightId = reader.ReadVarUhShort();
            SourceId = reader.ReadVarUhLong();
            TargetId = reader.ReadVarUhLong();
        }

    }
}
