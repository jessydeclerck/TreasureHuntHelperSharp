namespace Cookie.API.Protocol.Network.Messages.Game.Context.Roleplay.TreasureHunt
{
    using Utils.IO;

    public class TreasureHuntRequestMessage : NetworkMessage
    {
        public const ushort ProtocolId = 6488;
        public override ushort MessageID => ProtocolId;
        public ushort QuestLevel { get; set; }
        public byte QuestType { get; set; }

        public TreasureHuntRequestMessage(ushort questLevel, byte questType)
        {
            QuestLevel = questLevel;
            QuestType = questType;
        }

        public TreasureHuntRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUhShort(QuestLevel);
            writer.WriteByte(QuestType);
        }

        public override void Deserialize(IDataReader reader)
        {
            QuestLevel = reader.ReadVarUhShort();
            QuestType = reader.ReadByte();
        }

    }
}
