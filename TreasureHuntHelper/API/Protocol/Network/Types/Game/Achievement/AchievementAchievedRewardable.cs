namespace Cookie.API.Protocol.Network.Types.Game.Achievement
{
    using Utils.IO;

    public class AchievementAchievedRewardable : AchievementAchieved
    {
        public new const ushort ProtocolId = 515;
        public override ushort TypeID => ProtocolId;
        public ushort Finishedlevel { get; set; }

        public AchievementAchievedRewardable(ushort finishedlevel)
        {
            Finishedlevel = finishedlevel;
        }

        public AchievementAchievedRewardable() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarUhShort(Finishedlevel);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Finishedlevel = reader.ReadVarUhShort();
        }

    }
}
