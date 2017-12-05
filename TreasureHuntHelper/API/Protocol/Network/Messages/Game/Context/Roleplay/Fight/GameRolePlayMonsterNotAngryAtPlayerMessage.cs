namespace Cookie.API.Protocol.Network.Messages.Game.Context.Roleplay.Fight
{
    using Utils.IO;

    public class GameRolePlayMonsterNotAngryAtPlayerMessage : NetworkMessage
    {
        public const ushort ProtocolId = 6742;
        public override ushort MessageID => ProtocolId;
        public ulong PlayerId { get; set; }
        public double MonsterGroupId { get; set; }

        public GameRolePlayMonsterNotAngryAtPlayerMessage(ulong playerId, double monsterGroupId)
        {
            PlayerId = playerId;
            MonsterGroupId = monsterGroupId;
        }

        public GameRolePlayMonsterNotAngryAtPlayerMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUhLong(PlayerId);
            writer.WriteDouble(MonsterGroupId);
        }

        public override void Deserialize(IDataReader reader)
        {
            PlayerId = reader.ReadVarUhLong();
            MonsterGroupId = reader.ReadDouble();
        }

    }
}
