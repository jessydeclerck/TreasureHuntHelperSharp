namespace Cookie.API.Protocol.Network.Types.Game.Achievement
{
    using Utils.IO;

    public class AchievementAchieved : NetworkType
    {
        public const ushort ProtocolId = 514;
        public override ushort TypeID => ProtocolId;
        public ushort ObjectId { get; set; }
        public ulong AchievedBy { get; set; }

        public AchievementAchieved(ushort objectId, ulong achievedBy)
        {
            ObjectId = objectId;
            AchievedBy = achievedBy;
        }

        public AchievementAchieved() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUhShort(ObjectId);
            writer.WriteVarUhLong(AchievedBy);
        }

        public override void Deserialize(IDataReader reader)
        {
            ObjectId = reader.ReadVarUhShort();
            AchievedBy = reader.ReadVarUhLong();
        }

    }
}
