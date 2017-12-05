namespace Cookie.API.Protocol.Network.Messages.Game.Actions.Fight
{
    using Messages.Game.Actions;
    using Utils.IO;

    public class GameActionFightTriggerGlyphTrapMessage : AbstractGameActionMessage
    {
        public new const ushort ProtocolId = 5741;
        public override ushort MessageID => ProtocolId;
        public short MarkId { get; set; }
        public ushort MarkImpactCell { get; set; }
        public double TriggeringCharacterId { get; set; }
        public ushort TriggeredSpellId { get; set; }

        public GameActionFightTriggerGlyphTrapMessage(short markId, ushort markImpactCell, double triggeringCharacterId, ushort triggeredSpellId)
        {
            MarkId = markId;
            MarkImpactCell = markImpactCell;
            TriggeringCharacterId = triggeringCharacterId;
            TriggeredSpellId = triggeredSpellId;
        }

        public GameActionFightTriggerGlyphTrapMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort(MarkId);
            writer.WriteVarUhShort(MarkImpactCell);
            writer.WriteDouble(TriggeringCharacterId);
            writer.WriteVarUhShort(TriggeredSpellId);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            MarkId = reader.ReadShort();
            MarkImpactCell = reader.ReadVarUhShort();
            TriggeringCharacterId = reader.ReadDouble();
            TriggeredSpellId = reader.ReadVarUhShort();
        }

    }
}
