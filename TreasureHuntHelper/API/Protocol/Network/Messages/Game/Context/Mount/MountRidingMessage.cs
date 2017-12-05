namespace Cookie.API.Protocol.Network.Messages.Game.Context.Mount
{
    using Utils.IO;

    public class MountRidingMessage : NetworkMessage
    {
        public const ushort ProtocolId = 5967;
        public override ushort MessageID => ProtocolId;
        public bool IsRiding { get; set; }
        public bool IsAutopilot { get; set; }

        public MountRidingMessage(bool isRiding, bool isAutopilot)
        {
            IsRiding = isRiding;
            IsAutopilot = isAutopilot;
        }

        public MountRidingMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            var flag = new byte();
            flag = BooleanByteWrapper.SetFlag(0, flag, IsRiding);
            flag = BooleanByteWrapper.SetFlag(1, flag, IsAutopilot);
            writer.WriteByte(flag);
        }

        public override void Deserialize(IDataReader reader)
        {
            var flag = reader.ReadByte();
            IsRiding = BooleanByteWrapper.GetFlag(flag, 0);
            IsAutopilot = BooleanByteWrapper.GetFlag(flag, 1);
        }

    }
}
