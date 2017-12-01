using System;
using Cookie.API.Protocol;
using Cookie.API.Messages;
using Cookie.API.Gamedata.D2o;
using Cookie.API.Gamedata.D2i;
using Cookie.API.Gamedata;
using TreasureHuntHelper;
using TreasureHuntHelper.mitm;
using System.Net;
using System.Threading;
using TreasureHuntHelper.Web;
using System.IO;
using System.Reflection;

namespace treasureHuntHelper
{
    class Program
    {

        
        static void Main(string[] args)
        {
            ProtocolTypeManager.Initialize();
            MessageReceiver.Initialize();
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"GamePath.txt");
            string gamePath = System.IO.File.ReadAllText(path);
            ObjectDataManager.Instance.AddReaders(gamePath + @"\app\data\common");
            FastD2IReader.Init((gamePath + @"\app\data\i18n\i18n_fr.d2i"));
            WebService.InitDofusHuntValues();
            new Capture();
            return;
        }

    }
}
