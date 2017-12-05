namespace Cookie.API.Protocol.Network.Messages.Game.Character.Stats
{
    using Utils.IO;

    public class CharacterLevelUpMessage : NetworkMessage
    {
        public const ushort ProtocolId = 5670;
        public override ushort MessageID => ProtocolId;
        public ushort NewLevel { get; set; }

        public CharacterLevelUpMessage(ushort newLevel)
        {
            NewLevel = newLevel;
        }

        public CharacterLevelUpMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUhShort(NewLevel);
        }

        public override void Deserialize(IDataReader reader)
        {
            NewLevel = reader.ReadVarUhShort();
        }

    }
}
