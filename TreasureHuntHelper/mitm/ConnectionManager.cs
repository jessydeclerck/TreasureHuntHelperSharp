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
using System.Net.Sockets;
using Cookie.API.Protocol.Network.Messages.Handshake;
using Cookie.API.Protocol.Network.Messages.Game.Approach;

namespace TreasureHuntHelper.mitm
{
    class ConnectionManager
    {
        private IPEndPoint _loginServerEP, _gameServerEP;
        private Server _loginListenerSocket, _gameListenerSocket;
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
            _loginServerEP = server; // on le garde pour le Client
            _gameServerEP = new IPEndPoint(IPAddress.Parse("213.248.126.79"), 5555);
            _loginListenerSocket = new Server(5555); // on initialise le Listener
            _gameListenerSocket = new Server(786);
            //GameServer = new Server();
            //LoginServer = new Server();
            PManager = new PackManager();
            Console.WriteLine("Connection Manager initiated");

            //idNames = new PackIdNames();
        }

        /// <summary>
        /// 
        /// </summary>
        public void start()
        {
            _loginListenerSocket.onClientConnected += loginClientConnect; // on s'abonne aux nouvelles connexions
            _gameListenerSocket.onClientConnected += gameClientConnect;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        public void loginClientConnect(Client client)
        {
            Console.WriteLine("Nouveau login client connecté sur " + client.IpAndPort);

            Client server = new Client(_loginServerEP); // on initialise le client

            client.associated = server;// client = jeu
            server.associated = client;

            client.onReception += forwardtoserver; // on créer les règles de redirection
            server.onReception += forwadtoclient;

            server.connect(); // on connect le client
        }

        public void gameClientConnect(Client client)
        {
            Console.WriteLine("Nouveau game client connecté sur " + client.IpAndPort);

            Client server = new Client(_gameServerEP);

            client.associated = server;
            server.associated = client;

            client.onReception += forwardtoserver;
            server.onReception += forwadtoclient;

            server.connect();
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
            if (idMsg == 42)
            {
                int lenType = header & 3;
                int length = getLenMsg(lenType, reader);
                //Console.WriteLine("size buffer : " + buffer.Length);
                //Console.WriteLine(content_hex);
                NetworkMessage message = MessageReceiver.BuildMessage(idMsg, reader);
                SelectedServerDataMessage Ssdm = (SelectedServerDataMessage)message;
                Console.WriteLine(Ssdm.ServerId + " " + Ssdm.Address + " " + Ssdm.Port + " " + Ssdm.CanCreateNewCharacter + " " + Ssdm.Ticket);
                Ssdm.Address = "127.0.0.1";
                Ssdm.Port = 786;
                Console.WriteLine(Ssdm.ServerId + " " + Ssdm.Address + " " + Ssdm.Port + " " + Ssdm.CanCreateNewCharacter + " " + Ssdm.Ticket);
                BigEndianWriter beWriter = new BigEndianWriter();
                //Ssdm.Serialize(beWriter);
                //NetworkMessage msg = (NetworkMessage)Ssdm;
                //msg.Pack(beWriter);
                Ssdm.Pack(beWriter);
                Console.WriteLine("--test--");
                PManager.ParsePacket(beWriter.Data);
                Console.WriteLine("envoi paquet 42 vers " + sender.associated.IpAndPort);
                sender.associated.send(beWriter.Data);
                ProtocolRequired protocolRequired = new ProtocolRequired(1692, 1692);
                beWriter = new BigEndianWriter();
                protocolRequired.Pack(beWriter);
                PManager.ParsePacket(beWriter.Data);
                sender.associated.send(beWriter.Data);
                HelloGameMessage helloMsg = new HelloGameMessage();
                beWriter = new BigEndianWriter();
                helloMsg.Pack(beWriter);
                PManager.ParsePacket(beWriter.Data);
                sender.associated.send(beWriter.Data);
                //new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp).Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 786));

                /*byte[] newBuffer = new byte[buffer.Length - (Ssdm.Address.Length - "127.0.0.1".Length)];
                SelectedServerDataMessage newSsdm = new SelectedServerDataMessage(Ssdm.ServerId, "127.0.0.1", 786, Ssdm.CanCreateNewCharacter, Ssdm.Ticket);
                BigEndianWriter writer = new BigEndianWriter(newBuffer);
                newSsdm.Serialize(writer);
                Console.WriteLine(newSsdm.ServerId + " " + newSsdm.Address + " " + newSsdm.Port + " " + newSsdm.CanCreateNewCharacter);
                PManager.ParsePacket(buffer);
                sender.associated.send(newBuffer);*/

            }
            else
            {
                PManager.ParsePacket(buffer);
                sender.associated.send(buffer);
            }
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
