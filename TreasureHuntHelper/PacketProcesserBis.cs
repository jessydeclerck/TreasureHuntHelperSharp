using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.IO;
using Cookie.API.Utils.IO;
using Cookie.API.Protocol;
using Cookie.API.Messages;
using SharpPcap;
using TreasureHuntHelper;
using System.Collections.Concurrent;
using PacketDotNet;

namespace treasureHuntHelper
{
    class PacketProcesserBis
    {

        private BlockingCollection<Packet> packets;
        //private Queue<Packet> packets;

        public PacketProcesserBis()
        {
            packets = new BlockingCollection<Packet>();
            //this.process();
        }

        public void addPacket(RawCapture packet)
        {
            packets.Add(Packet.ParsePacket(packet.LinkLayerType, packet.Data));
        }

        private byte[] getData(Packet packet)
        {

            //IpPacket ipPacket = (IpPacket)packetToRead.Extract(typeof(IpPacket));
            TcpPacket tcpPacket = (TcpPacket)packet.Extract(typeof(TcpPacket));
            //var ip = ipPacket.SourceAddress;
            //Console.WriteLine("Source address : " + ip);
            return tcpPacket.PayloadData;

        }

        private int getLenMsg(int lenType, IDataReader reader)
        {
            int length = 0;
            int tmpLenType = lenType;
            while (tmpLenType-- > 0)
                try { length = (length << 8) + reader.ReadByte(); } catch (Exception e) { }

            return length;
        }

        public void process()
        {
            bool isStarted = true;
            MemoryStream ms = new MemoryStream();
            bool fragmented = false;
            while (isStarted)
            {

                Console.WriteLine("test");
                Packet packet = packets.Take();
                var watch = System.Diagnostics.Stopwatch.StartNew();
                byte[] data = getData(packet);

                IDataReader reader = new BigEndianReader(data);
                int header = reader.ReadShort();
                ushort idMsg = (ushort)(header >> 2);// getIdMsg(reader);
                //Console.WriteLine(idMsg);
                if (ToCatch.MESSAGES.Contains((short)idMsg))
                {
                    Console.WriteLine("packet received");
                    try
                    {
                        int lenType = header & 3;
                        int length = getLenMsg(lenType, reader);
                        while (length > data.Length)
                        {
                            fragmented = true;
                            Console.WriteLine("taille data message : " + length + " taille data paquet : " + data.Length);
                            data = appendNextPacket(data);
                        }
                        if (fragmented)
                        {
                            //on réinitialise le reader
                            reader = new BigEndianReader(data);
                            reader.ReadShort();
                            getLenMsg(lenType, reader);
                            fragmented = false;
                        }
                        Console.WriteLine("taille data to build : " + (data.Length - 1 - 32));
                        //Console.WriteLine(BitConverter.ToString(data).Replace("-", string.Empty));
                        
                        NetworkMessage message = MessageReceiver.BuildMessage(idMsg, reader);
                        MessageHandler.handleMessage(message);
                        watch.Stop();
                        Console.WriteLine("time elapsed while building packet : " + watch.ElapsedMilliseconds + " ms");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
        }

        private byte[] appendNextPacket(byte[] data)
        {
            Console.WriteLine("appending next packet...");
            Packet packetToAppend = packets.Take();
            byte[] dataToAppend = getData(packetToAppend);
            Console.WriteLine("taille packetToAppend : " + dataToAppend.Length);
            byte[] result = new byte[data.Length + dataToAppend.Length];
            System.Buffer.BlockCopy(data, 0, result, 0, data.Length);
            System.Buffer.BlockCopy(dataToAppend, 0, result, data.Length, dataToAppend.Length);
            return result;
        }

        }


    /*IDataReader datacpy = new BigEndianReader(data);
    int headercpy = datacpy.ReadShort();
    ushort idMsgcpy = (ushort)(headercpy >> 2);// getIdMsg(reader);
    int lentypecpy = (int)(headercpy & 3);*/
    //byte[] dataMsg = datacpy.ReadBytes(getLenMsg(lentypecpy, datacpy));

    //Console.WriteLine("id message : " + (ushort)idMsg + " len : " + getLenMsg(lentypecpy, datacpy) + " lenrestant : " + (data.Length - 1 - 32) + "\n");
    //Console.WriteLine(BitConverter.ToString(dataMsg).Replace("-", string.Empty));

}
