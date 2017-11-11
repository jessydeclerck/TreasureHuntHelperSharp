using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using PcapDotNet.Packets;
using PcapDotNet.Packets.IpV4;
using PcapDotNet.Packets.Transport;
using PcapDotNet.Core;
using System.IO;
using Cookie.API.Utils.IO;
using Cookie.API.Protocol;
using Cookie.API.Messages;

namespace treasureHuntHelper
{
    class PacketProcesser
    {
        private static JsonManager jsonManager = new JsonManager();

        private static List<ushort> messagesToCatch = new List<ushort>(new ushort[] { 226, 6491, 6509, 6484, 6485, 6483, 6510,6507,6508,6487,6499,6486,6489,6488,6498});

        private PacketProcesser()
        {
        }

        public static void printMessage(Packet packet)
        {

            /*Packet packetToRead = Packet.ParsePacket(packet.LinkLayerType, packet.Data);
            //IpPacket ipPacket = (IpPacket)packetToRead.Extract(typeof(IpPacket));
            TcpPacket tcpPacket = (TcpPacket)packetToRead.Extract(typeof(TcpPacket));
            //var ip = ipPacket.SourceAddress;
            //Console.WriteLine("Source address : " + ip);*/
            IpV4Datagram ip = packet.Ethernet.IpV4;
            TcpDatagram  tcpDatagram = ip.Tcp;
            Datagram tcpPayload = tcpDatagram.Payload;
            if (null != tcpPayload)
            {
                int payloadLength = tcpPayload.Length;
                byte[] rx_payload = new byte[payloadLength];
                using (MemoryStream ms = tcpPayload.ToMemoryStream())
                {
                    ms.Read(rx_payload, 0, payloadLength);
                }
                
                IDataReader reader = new BigEndianReader(rx_payload);
                int header = reader.ReadShort();
                ushort idMsg = (ushort) (header >> 2);// getIdMsg(reader);

                //byte[] dataMsg = reader.ReadBytes(length);

                if (messagesToCatch.Contains(idMsg))
                {

                Console.WriteLine("id message : " + (ushort)idMsg );
                    try
                    {
                        int lenType = header & 3;// getLenMsg(reader);
                        int length = getLenMsg(lenType, reader);
                        NetworkMessage message = MessageReceiver.BuildMessage(idMsg, reader);



                        MessageHandler.handleMessage(message);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }

            }
        }

        private static int getLenMsg(int lenType, IDataReader reader)
        {
            int length = 0;
            int tmpLenType = lenType;
            while (tmpLenType-- > 0)
                try { length = (length << 8) + reader.ReadByte(); } catch(Exception e) { }

            return length;
        }

    }
}
