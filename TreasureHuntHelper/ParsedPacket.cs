using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreasureHuntHelper
{
    class ParsedPacket
    {
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

        public ParsedPacket(short packetId, Int32 packetLength, byte[] packetContent)
        {
            this.packet_id = packetId;
            this.packet_lenght = packetLength;
            this._packet_content = packetContent;
        }

        public ParsedPacket()
        {

        }

    }
}
