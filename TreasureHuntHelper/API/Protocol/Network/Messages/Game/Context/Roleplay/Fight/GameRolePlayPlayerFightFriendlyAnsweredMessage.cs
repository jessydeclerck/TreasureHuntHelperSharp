namespace Cookie.API.Protocol.Network.Messages.Game.Context.Roleplay.Fight
{
    using Utils.IO;

    public class GameRolePlayPlayerFightFriendlyAnsweredMessage : NetworkMessage
    {
        public const ushort ProtocolId = 5733;
        public override ushort MessageID => ProtocolId;
        public ushort FightId { get; set; }
        public ulong SourceId { get; set; }
        public ulong TargetId { get; set; }
        public bool Accept { get; set; }

        public GameRolePlayPlayerFightFriendlyAnsweredMessage(ushort fightId, ulong sourceId, ulong targetId, bool accept)
        {
            FightId = fightId;
            SourceId = sourceId;
            TargetId = targetId;
            Accept = accept;
        }

        public GameRolePlayPlayerFightFriendlyAnsweredMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUhShort(FightId);
            writer.WriteVarUhLong(SourceId);
            writer.WriteVarUhLong(TargetId);
            writer.WriteBoolean(Accept);
        }

        public override void Deserialize(IDataReader reader)
        {
            FightId = reader.ReadVarUhShort();
            SourceId = reader.ReadVarUhLong();
            TargetId = reader.ReadVarUhLong();
            Accept = reader.ReadBoolean();
        }

    }
}
