using Cookie.API.Messages;
using Cookie.API.Protocol;
using Cookie.API.Protocol.Network.Messages.Connection;
using Cookie.API.Utils.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using treasureHuntHelper;

namespace TreasureHuntHelper.mitm
{
    class PackManager
    {
        #region Getter Setter

        private short _packet_id;

        public virtual short packet_id
        {
            get { return _packet_id; }
            set { _packet_id = value; }
        }

        private Int32 _packet_lenght;

        public virtual Int32 packet_lenght
        {
            get { return _packet_lenght; }
            set { _packet_lenght = value; }
        }

        private byte[] _packet_content;

        public virtual byte[] packet_content
        {
            get { return _packet_content; }
            set { _packet_content = value; }
        }
        #endregion

        public PackManager()
        {
        }
        private bool fragmented = false;

        private void appendNextPacket()
        {

        }

        private byte[] tmpPacket;

        private byte[] appendNextPacket(byte[] dataToAppend)
        {
            Console.WriteLine("appending next packet...");
            byte[] data = tmpPacket;
            Console.WriteLine("taille packetToAppend : " + dataToAppend.Length);
            byte[] result = new byte[data.Length + dataToAppend.Length];
            System.Buffer.BlockCopy(data, 0, result, 0, data.Length);
            System.Buffer.BlockCopy(dataToAppend, 0, result, data.Length, dataToAppend.Length);
            return result;
        }

        public void ParsePacket(byte[] PacketToParse)
        {
            int initLength = PacketToParse.Length;
            byte[] initPacket = PacketToParse.ToArray();
            int index = 0;
            short id_and_packet_lenght_type, packet_lenght_type = 0;
            _packet_lenght = 0;
            if (fragmented && PacketToParse.Length != 0)
            {
                PacketToParse = appendNextPacket(initPacket);
                Console.WriteLine("total packet : " + PacketToParse.Length);
                initLength = PacketToParse.Length;
                fragmented = false;
            }
            try
            {
                // Lecture jusqu'à la fin de byte[] data
                bool isConstructed;
                while (isConstructed = (index != PacketToParse.Length) && PacketToParse.Length != 0)
                {
                    // Décodage du header
                    id_and_packet_lenght_type = (short)(PacketToParse[index] * 256 + PacketToParse[index + 1]); // Selection des 2 premiers octets du paquet
                    _packet_id = (short)(id_and_packet_lenght_type >> 2); // Récupérer l'ID du paquet
                    packet_lenght_type = (short)(id_and_packet_lenght_type & 3); // Récupérer la taille de la taille de la longueur du paquet

                    index += 2; // On se déplace 2 octets plus loin

                    if (packet_lenght_type < 0 || packet_lenght_type > 3)
                        throw new Exception("Malformated Message Header, invalid bytes number to read message length (inferior to 0 or superior to 3)");
                    // Récupération de la taille du paquet
                    switch (packet_lenght_type)
                    {
                        case 0:
                            _packet_lenght = 0;
                            break;
                        case 1:
                            _packet_lenght = PacketToParse[index];
                            break;
                        case 2:
                            _packet_lenght = PacketToParse[index] * 256 + PacketToParse[index + 1];
                            break;
                        case 3:
                            _packet_lenght = PacketToParse[index] * 65536 + PacketToParse[index + 1] * 256 + PacketToParse[index + 2];
                            break;
                    }
                    if(initLength - 1 - 32 < _packet_lenght && _packet_id == 226 && isConstructed)
                    {
                        fragmented = true;
                        tmpPacket = initPacket;
                        Console.WriteLine("packet length type : " + packet_lenght_type);
                        Console.WriteLine("packet length : " + _packet_lenght);
                        Console.WriteLine("packet content length : " + _packet_content.Length);
                        Console.WriteLine("Init length : " + initLength);
                        //Console.WriteLine("packet before appending : " + tmpPacket.Length);
                        return;
                    }
                    _packet_content = new byte[(int)_packet_lenght];
                    Array.Copy(PacketToParse, index + packet_lenght_type, _packet_content, 0, _packet_lenght);
                    index += _packet_lenght + packet_lenght_type;
                    if (ToCatch.MESSAGES.Contains(_packet_id))
                    {
                        Console.WriteLine("packet length type : " + packet_lenght_type);
                        Console.WriteLine("packet length : " + _packet_lenght);
                        Console.WriteLine("packet content length : " + _packet_content.Length);

                        try
                        {
                            NetworkMessage message = MessageReceiver.BuildMessage((ushort)_packet_id, new BigEndianReader(_packet_content));
                            MessageHandler.handleMessage(message);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }

                }
            }
            catch (Exception e)
            {
                /*if(_packet_id == 226) { 
                    Console.WriteLine("packet length type : " + packet_lenght_type);
                    Console.WriteLine("packet length : " + _packet_lenght);
                    Console.WriteLine("packet content length : " + _packet_content.Length);
                    Console.WriteLine("Init length : " + initLength);
                }*/
                //Console.WriteLine(e.ToString());
            }
        }
    }
}