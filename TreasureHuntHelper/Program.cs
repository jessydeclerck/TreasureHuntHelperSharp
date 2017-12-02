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
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace treasureHuntHelper
{
    class Program
    {
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetWindowPos(
       IntPtr hWnd,
       IntPtr hWndInsertAfter,
       int x,
       int y,
       int cx,
       int cy,
       int uFlags);

        private const int HWND_TOPMOST = -1;
        private const int SWP_NOMOVE = 0x0002;
        private const int SWP_NOSIZE = 0x0001;

        static void Main(string[] args)
        {
            IntPtr hWnd = Process.GetCurrentProcess().MainWindowHandle;

            SetWindowPos(hWnd,
                new IntPtr(HWND_TOPMOST),
                0, 0, 0, 0,
                SWP_NOMOVE | SWP_NOSIZE);

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
