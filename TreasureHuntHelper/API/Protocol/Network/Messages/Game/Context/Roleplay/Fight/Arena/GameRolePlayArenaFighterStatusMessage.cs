namespace Cookie.API.Protocol.Network.Messages.Game.Context.Roleplay.Fight.Arena
{
    using Utils.IO;

    public class GameRolePlayArenaFighterStatusMessage : NetworkMessage
    {
        public const ushort ProtocolId = 6281;
        public override ushort MessageID => ProtocolId;
        public ushort FightId { get; set; }
        public int PlayerId { get; set; }
        public bool Accepted { get; set; }

        public GameRolePlayArenaFighterStatusMessage(ushort fightId, int playerId, bool accepted)
        {
            FightId = fightId;
            PlayerId = playerId;
            Accepted = accepted;
        }

        public GameRolePlayArenaFighterStatusMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUhShort(FightId);
            writer.WriteInt(PlayerId);
            writer.WriteBoolean(Accepted);
        }

        public override void Deserialize(IDataReader reader)
        {
            FightId = reader.ReadVarUhShort();
            PlayerId = reader.ReadInt();
            Accepted = reader.ReadBoolean();
        }

    }
}
