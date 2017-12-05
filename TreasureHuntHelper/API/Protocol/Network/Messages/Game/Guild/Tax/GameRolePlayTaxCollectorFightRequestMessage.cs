namespace Cookie.API.Protocol.Network.Messages.Game.Guild.Tax
{
    using Utils.IO;

    public class GameRolePlayTaxCollectorFightRequestMessage : NetworkMessage
    {
        public const ushort ProtocolId = 5954;
        public override ushort MessageID => ProtocolId;

        public GameRolePlayTaxCollectorFightRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
        }

        public override void Deserialize(IDataReader reader)
        {
        }

    }
}
