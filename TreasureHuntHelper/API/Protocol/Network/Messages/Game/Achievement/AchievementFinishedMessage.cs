namespace Cookie.API.Protocol.Network.Messages.Game.Achievement
{
    using Types.Game.Achievement;
    using Utils.IO;

    public class AchievementFinishedMessage : NetworkMessage
    {
        public const ushort ProtocolId = 6208;
        public override ushort MessageID => ProtocolId;
        public AchievementAchievedRewardable Achievement { get; set; }

        public AchievementFinishedMessage(AchievementAchievedRewardable achievement)
        {
            Achievement = achievement;
        }

        public AchievementFinishedMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            Achievement.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            Achievement = new AchievementAchievedRewardable();
            Achievement.Deserialize(reader);
        }

    }
}
