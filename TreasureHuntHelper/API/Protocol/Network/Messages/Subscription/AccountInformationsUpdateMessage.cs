namespace Cookie.API.Protocol.Network.Messages.Subscription
{
    using Utils.IO;

    public class AccountInformationsUpdateMessage : NetworkMessage
    {
        public const ushort ProtocolId = 6740;
        public override ushort MessageID => ProtocolId;
        public double SubscriptionEndDate { get; set; }
        public double UnlimitedRestatEndDate { get; set; }

        public AccountInformationsUpdateMessage(double subscriptionEndDate, double unlimitedRestatEndDate)
        {
            SubscriptionEndDate = subscriptionEndDate;
            UnlimitedRestatEndDate = unlimitedRestatEndDate;
        }

        public AccountInformationsUpdateMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteDouble(SubscriptionEndDate);
            writer.WriteDouble(UnlimitedRestatEndDate);
        }

        public override void Deserialize(IDataReader reader)
        {
            SubscriptionEndDate = reader.ReadDouble();
            UnlimitedRestatEndDate = reader.ReadDouble();
        }

    }
}
