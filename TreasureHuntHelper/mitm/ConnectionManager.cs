using Cookie.API.Protocol;
using Cookie.API.Utils.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Cookie.API.Protocol.Network.Messages.Connection;
using Cookie.API.Messages;

namespace TreasureHuntHelper.mitm
{
    class ConnectionManager
    {
        private IPEndPoint _serverEP;
        private Server _listenerSocket;
        private PackManager PManager;

        //private PackIdNames idNames;


        public delegate void clientActionEventHandler(Client client);
        public event clientActionEventHandler ClientConnected;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="server"></param>
        /// <param name="_Log"></param>
        /// <param name="_Main"></param>
        public ConnectionManager(IPEndPoint server)
        {
            _serverEP = server; // on le garde pour le Client
            _listenerSocket = new Server(); // on initialise le Listener
            PManager = new PackManager();
            Console.WriteLine("Connection Manager initiated");

            //idNames = new PackIdNames();
        }

        /// <summary>
        /// 
        /// </summary>
        public void start()
        {
            _listenerSocket.onClientConnected += clientconnect; // on s'abonne aux nouvelles connexions
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        public void clientconnect(Client client)
        {
            Console.WriteLine("Nouveau client connecté sur " + client.IpAndPort);

            Client server = new Client(_serverEP); // on initialise le client

            client.associated = server;
            server.associated = client;

            client.onReception += forwardtoserver; // on créer les règles de redirection
            server.onReception += forwadtoclient;

            server.connect(); // on connect le client
        }

        private void forwadtoclient(Client sender, byte[] buffer)
        {
            //PManager = new PackManager(ClientForm);
            string content_hex = string.Empty;
            int huit_bytes = 0;
            foreach (byte b in buffer)
            {
                content_hex += b.ToString("X2") + " ";
                huit_bytes++;
            }
            //ClientForm.AddItem(true, PManager.packet_id, idNames.GetClasseName(PManager.packet_id), PManager.packet_lenght, PManager.packet_content);
            BigEndianReader reader = new BigEndianReader(buffer);
            int header = reader.ReadShort();
            ushort idMsg = (ushort)(header >> 2);// getIdMsg(reader);
            //Console.WriteLine(idMsg);
            /*if(idMsg == 42) {
                int lenType = header & 3;
                int length = getLenMsg(lenType, reader);
                Console.WriteLine("size buffer : " + buffer.Length);
                Console.WriteLine(content_hex);
                NetworkMessage message = MessageReceiver.BuildMessage(idMsg, reader);
                SelectedServerDataMessage Ssdm = (SelectedServerDataMessage)message;
                Console.WriteLine(Ssdm.ServerId + " " + Ssdm.Address + " " + Ssdm.Port + " " + Ssdm.CanCreateNewCharacter);
                byte[] newBuffer = new byte[buffer.Length - (Ssdm.Address.Length - "127.0.0.1".Length)];
                SelectedServerDataMessage newSsdm = new SelectedServerDataMessage(Ssdm.ServerId, "127.0.0.1", 786, Ssdm.CanCreateNewCharacter, Ssdm.Ticket);
                BigEndianWriter writer = new BigEndianWriter(newBuffer);
                newSsdm.Serialize(writer);
                Console.WriteLine("newBuffer size : " + newBuffer.Length);
                sender.associated.send(newBuffer);

            }else*/
                sender.associated.send(buffer);
        }


        private int getLenMsg(int lenType, IDataReader reader)
        {
            int length = 0;
            int tmpLenType = lenType;
            while (tmpLenType-- > 0)
                try { length = (length << 8) + reader.ReadByte(); } catch (Exception e) { }

            return length;
        }
        private void forwardtoserver(Client sender, byte[] buffer)
        {
            string content_hex = string.Empty;
            int huit_bytes = 0;
            foreach (byte b in buffer)
            {
                content_hex += b.ToString("X2") + " ";
                huit_bytes++;
            }
            PManager.ParsePacket(buffer);
            //ClientForm.AddItem(false, PManager.packet_id, idNames.GetClasseName(PManager.packet_id), PManager.packet_lenght, PManager.packet_content);
            //ClientForm.WriteLogClient(content_hex + "\tASCII = " + Encoding.ASCII.GetString(buffer));
            sender.associated.send(buffer);
        }
    }
}
