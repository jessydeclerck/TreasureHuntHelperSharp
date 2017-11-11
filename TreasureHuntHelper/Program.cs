using System;
using Cookie.API.Protocol;
using Cookie.API.Messages;
using Cookie.API.Gamedata.D2o;

namespace treasureHuntHelper
{
    class Program
    {

        
        static void Main(string[] args)
        {
            ProtocolTypeManager.Initialize();
            MessageReceiver.Initialize();
            ObjectDataManager.Instance.AddReaders(@"C:\Users\Pepito\Desktop\Ankama\Dofus\app\data\common");
            new Capture();
            return;
        }

    }
}
