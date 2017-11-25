﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Diagnostics;

namespace TreasureHuntHelper.Injection
{
    class Injection
    {
        private bool stopThread = false;
        public Dictionary<int, bool> DofusMods { get; set; }

        public Injection()
        {
            DofusMods = new Dictionary<int, bool>();
        }
        public string ApplicationPath
        {
            get { return Environment.CurrentDirectory; }
        }

        public DllInjectionResult Inject(int dofusModId)
        {
            try
            {
                if (!(File.Exists(ApplicationPath + "\\No.Ankama.dll")))
                {
                    throw new ArgumentException("Le fichier No.Ankama.dll ne se trouve pas dans le dossier ! Veuillez vérifier cela avec votre Antivirus.");
                }
                DllInjector dllInjector = new DllInjector();
                return dllInjector.Inject(Convert.ToUInt32(dofusModId), ApplicationPath + "\\No.Ankama.dll");
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.ToString());
            }
            return DllInjectionResult.InjectionFailed;
        }

        public void ProcessDofus()
        {
            stopThread = false;
            Console.WriteLine("MITM activé.");
            while (!stopThread)
            {
                List<Process> processDofusMod;
                try
                {
                    processDofusMod = Process.GetProcessesByName("Dofus").ToList();
                    if (processDofusMod.Count != 0)
                    {
                        foreach (Process dofusMod in processDofusMod)
                        {
                            if (!(DofusMods.ContainsKey(dofusMod.Id)))
                            {
                                Console.WriteLine("Nouveau processus Dofus trouvé avec l'id = " + dofusMod.Id);
                                switch (Inject(dofusMod.Id))
                                {
                                    case DllInjectionResult.DllNotFound:
                                        DofusMods.Add(dofusMod.Id, false);
                                        throw new ArgumentException("Dll not found.");

                                    case DllInjectionResult.GameProcessNotFound:
                                        DofusMods.Add(dofusMod.Id, false);
                                        throw new ArgumentException("Process " + dofusMod.Id + " not found");

                                    case DllInjectionResult.InjectionFailed:
                                        DofusMods.Add(dofusMod.Id, false);
                                        throw new ArgumentException("Injection Failed (Process " + dofusMod.Id + ")");

                                    case DllInjectionResult.Success:
                                        Console.WriteLine("Processus Dofus à l'id = " + dofusMod.Id + " a bien été patché !");
                                        DofusMods.Add(dofusMod.Id, true);
                                        break;
                                };
                            }
                        }
                    }
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        public void StopThread()
        {
            stopThread = true;
            Console.WriteLine("MITM desactivé.");
        }
    }
}
