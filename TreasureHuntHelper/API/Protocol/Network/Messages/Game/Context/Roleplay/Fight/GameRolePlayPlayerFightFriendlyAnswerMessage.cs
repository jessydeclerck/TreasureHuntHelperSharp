namespace Cookie.API.Protocol.Network.Messages.Game.Context.Roleplay.Fight
{
    using Utils.IO;

    public class GameRolePlayPlayerFightFriendlyAnswerMessage : NetworkMessage
    {
        public const ushort ProtocolId = 5732;
        public override ushort MessageID => ProtocolId;
        public ushort FightId { get; set; }
        public bool Accept { get; set; }

        public GameRolePlayPlayerFightFriendlyAnswerMessage(ushort fightId, bool accept)
        {
            FightId = fightId;
            Accept = accept;
        }

        public GameRolePlayPlayerFightFriendlyAnswerMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUhShort(FightId);
            writer.WriteBoolean(Accept);
        }

        public override void Deserialize(IDataReader reader)
        {
            FightId = reader.ReadVarUhShort();
            Accept = reader.ReadBoolean();
        }

    }
}
