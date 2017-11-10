using System;
using Cookie.API.Protocol;
using Cookie.API.Messages;

namespace treasureHuntHelper
{
    class Program
    {

        
        static void Main(string[] args)
        {
            ProtocolTypeManager.Initialize();
            MessageReceiver.Initialize();
            new Capture();
            return;
        }

    }
}
