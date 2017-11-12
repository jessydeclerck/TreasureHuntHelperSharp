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
using TreasureHuntHelper;

namespace treasureHuntHelper
{
    class PacketProcesser
    {


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

                

                if (ToCatch.MESSAGES.Contains(idMsg))
                {
                    IDataReader datacpy = new BigEndianReader(rx_payload);
                    int headercpy = datacpy.ReadShort();
                    ushort idMsgcpy = (ushort)(headercpy >> 2);// getIdMsg(reader);
                    int lentypecpy = (int)(headercpy & 3);
                    //byte[] dataMsg = datacpy.ReadBytes(getLenMsg(lentypecpy, datacpy));
                    
                    Console.WriteLine("id message : " + (ushort)idMsg + " len : " + getLenMsg(lentypecpy, datacpy) + " lenrestant : "+ (rx_payload.Length - 1 - 32) + "\n");
                    //Console.WriteLine(BitConverter.ToString(dataMsg).Replace("-", string.Empty));
                    Console.WriteLine(BitConverter.ToString(rx_payload).Replace("-", string.Empty));
                    try
                    {
                        int lenType = header & 3;
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
