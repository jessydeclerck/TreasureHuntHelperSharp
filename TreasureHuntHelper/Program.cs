using System;
using Cookie.API.Protocol;
using Cookie.API.Messages;
using Cookie.API.Gamedata.D2o;
using Cookie.API.Gamedata.D2i;
using Cookie.API.Gamedata;

namespace treasureHuntHelper
{
    class Program
    {

        
        static void Main(string[] args)
        {
            ProtocolTypeManager.Initialize();
            MessageReceiver.Initialize();
            ObjectDataManager.Instance.AddReaders(@"C:\Users\Pepito\Desktop\Ankama\Dofus\app\data\common");
            FastD2IReader.Init(@"C:\Users\Pepito\Desktop\Ankama\Dofus\app\data\i18n\i18n_fr.d2i");
            Console.WriteLine(D2OParsing.GetPoiName(64));
            new Capture();
            return;
        }

    }
}
