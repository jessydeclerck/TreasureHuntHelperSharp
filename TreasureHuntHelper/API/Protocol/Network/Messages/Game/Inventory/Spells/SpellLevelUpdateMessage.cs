namespace Cookie.API.Protocol.Network.Messages.Game.Inventory.Spells
{
    using Types.Game.Data.Items;
    using Utils.IO;

    public class SpellLevelUpdateMessage : NetworkMessage
    {
        public const ushort ProtocolId = 6743;
        public override ushort MessageID => ProtocolId;
        public SpellItem Spell { get; set; }

        public SpellLevelUpdateMessage(SpellItem spell)
        {
            Spell = spell;
        }

        public SpellLevelUpdateMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            Spell.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            Spell = new SpellItem();
            Spell.Deserialize(reader);
        }

    }
}
