namespace Cookie.API.Protocol.Network.Messages.Game.Achievement
{
    using Types.Game.Achievement;
    using System.Collections.Generic;
    using Utils.IO;

    public class AchievementListMessage : NetworkMessage
    {
        public const ushort ProtocolId = 6205;
        public override ushort MessageID => ProtocolId;
        public List<AchievementAchieved> FinishedAchievements { get; set; }

        public AchievementListMessage(List<AchievementAchieved> finishedAchievements)
        {
            FinishedAchievements = finishedAchievements;
        }

        public AchievementListMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)FinishedAchievements.Count);
            for (var finishedAchievementsIndex = 0; finishedAchievementsIndex < FinishedAchievements.Count; finishedAchievementsIndex++)
            {
                var objectToSend = FinishedAchievements[finishedAchievementsIndex];
                writer.WriteUShort(objectToSend.TypeID);
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var finishedAchievementsCount = reader.ReadUShort();
            FinishedAchievements = new List<AchievementAchieved>();
            for (var finishedAchievementsIndex = 0; finishedAchievementsIndex < finishedAchievementsCount; finishedAchievementsIndex++)
            {
                var objectToAdd = ProtocolTypeManager.GetInstance<AchievementAchieved>(reader.ReadUShort());
                objectToAdd.Deserialize(reader);
                FinishedAchievements.Add(objectToAdd);
            }
        }

    }
}
