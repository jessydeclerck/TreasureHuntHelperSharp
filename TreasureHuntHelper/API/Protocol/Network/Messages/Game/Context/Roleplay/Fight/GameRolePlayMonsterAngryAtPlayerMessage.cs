namespace Cookie.API.Protocol.Network.Messages.Game.Context.Roleplay.Fight
{
    using Utils.IO;

    public class GameRolePlayMonsterAngryAtPlayerMessage : NetworkMessage
    {
        public const ushort ProtocolId = 6741;
        public override ushort MessageID => ProtocolId;
        public ulong PlayerId { get; set; }
        public double MonsterGroupId { get; set; }
        public double AngryStartTime { get; set; }
        public double AttackTime { get; set; }

        public GameRolePlayMonsterAngryAtPlayerMessage(ulong playerId, double monsterGroupId, double angryStartTime, double attackTime)
        {
            PlayerId = playerId;
            MonsterGroupId = monsterGroupId;
            AngryStartTime = angryStartTime;
            AttackTime = attackTime;
        }

        public GameRolePlayMonsterAngryAtPlayerMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUhLong(PlayerId);
            writer.WriteDouble(MonsterGroupId);
            writer.WriteDouble(AngryStartTime);
            writer.WriteDouble(AttackTime);
        }

        public override void Deserialize(IDataReader reader)
        {
            PlayerId = reader.ReadVarUhLong();
            MonsterGroupId = reader.ReadDouble();
            AngryStartTime = reader.ReadDouble();
            AttackTime = reader.ReadDouble();
        }

    }
}
