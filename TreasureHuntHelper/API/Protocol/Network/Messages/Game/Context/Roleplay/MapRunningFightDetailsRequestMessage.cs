namespace Cookie.API.Protocol.Network.Messages.Game.Context.Roleplay
{
    using Utils.IO;

    public class MapRunningFightDetailsRequestMessage : NetworkMessage
    {
        public const ushort ProtocolId = 5750;
        public override ushort MessageID => ProtocolId;
        public ushort FightId { get; set; }

        public MapRunningFightDetailsRequestMessage(ushort fightId)
        {
            FightId = fightId;
        }

        public MapRunningFightDetailsRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUhShort(FightId);
        }

        public override void Deserialize(IDataReader reader)
        {
            FightId = reader.ReadVarUhShort();
        }

    }
}
