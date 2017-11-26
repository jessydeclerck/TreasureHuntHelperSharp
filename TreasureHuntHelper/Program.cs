﻿using System;
using Cookie.API.Protocol;
using Cookie.API.Messages;
using Cookie.API.Gamedata.D2o;
using Cookie.API.Gamedata.D2i;
using Cookie.API.Gamedata;
using TreasureHuntHelper;
using TreasureHuntHelper.Injection;
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
            //Injection injection = new Injection();
            //Thread thread = new Thread(new ThreadStart(injection.ProcessDofus));
            //thread.Start();
            //Console.WriteLine("213.248.126.39");
            //ConnectionManager mitm = new ConnectionManager(new IPEndPoint(IPAddress.Parse("213.248.126.39"), 5555));
            //new Thread(new ThreadStart(mitm.start)).Start();
            //ConnectionManager game = new ConnectionManager(new IPEndPoint(IPAddress.Parse("213.248.126.79"), 786));
            //game.start();
            //new ConnectionManager(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 12345)).start();
            //Console.WriteLine(D2OParsing.GetPoiName(64));
            new Capture();
            //new Capture3();
            return;
        }

    }
}
