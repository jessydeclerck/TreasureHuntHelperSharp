namespace Cookie.API.Protocol.Network.Messages.Game.Context.Roleplay.Spell
{
    using Utils.IO;

    public class SpellVariantActivationMessage : NetworkMessage
    {
        public const ushort ProtocolId = 6705;
        public override ushort MessageID => ProtocolId;
        public ushort SpellId { get; set; }
        public bool Result { get; set; }

        public SpellVariantActivationMessage(ushort spellId, bool result)
        {
            SpellId = spellId;
            Result = result;
        }

        public SpellVariantActivationMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUhShort(SpellId);
            writer.WriteBoolean(Result);
        }

        public override void Deserialize(IDataReader reader)
        {
            SpellId = reader.ReadVarUhShort();
            Result = reader.ReadBoolean();
        }

    }
}
