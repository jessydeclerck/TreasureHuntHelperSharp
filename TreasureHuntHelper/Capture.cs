using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PcapDotNet.Core;
using PcapDotNet.Packets;
using System.Threading;

namespace treasureHuntHelper
{
    class Capture
    {
      public Capture()
        {

                 // Retrieve the device list from the local machine
                IList<LivePacketDevice> allDevices = LivePacketDevice.AllLocalMachine;

                if (allDevices.Count == 0)
                {
                    Console.WriteLine("No interfaces found! Make sure WinPcap is installed.");
                    return;
                }

                // Print the list
                for (int i = 0; i != allDevices.Count; ++i)
                {
                    LivePacketDevice device = allDevices[i];
                    Console.Write((i + 1) + ". " + device.Name);
                    if (device.Description != null)
                        Console.WriteLine(" (" + device.Description + ")");
                    else
                        Console.WriteLine(" (No description available)");
                }

                int deviceIndex = 0;
                do
                {
                    Console.WriteLine("Enter the interface number (1-" + allDevices.Count + "):");
                    string deviceIndexString = Console.ReadLine();
                    if (!int.TryParse(deviceIndexString, out deviceIndex) ||
                        deviceIndex < 1 || deviceIndex > allDevices.Count)
                    {
                        deviceIndex = 0;
                    }
                } while (deviceIndex == 0);

                // Take the selected adapter
                PacketDevice selectedDevice = allDevices[deviceIndex - 1];

                // Open the device
                using (PacketCommunicator communicator =
                    selectedDevice.Open(2000,                                  // portion of the packet to capture
                                        PacketDeviceOpenAttributes.Promiscuous|PacketDeviceOpenAttributes.NoCaptureLocal|PacketDeviceOpenAttributes.NoCaptureRemote, // promiscuous mode
                                        500))                                  // read timeout
                {
                    Console.WriteLine("Listening on " + selectedDevice.Description + "...");

                // start the capture
                    communicator.SetFilter("net 213.248.126.0 mask 255.255.255.0");
                    packetProcesser = new PacketProcesser();
                    Thread thread = new Thread(new ThreadStart(packetProcesser.process));
                    thread.Start();
                //communicator.ReceivePackets(0, PacketHandler);
                // Retrieve the packets
                Packet packet;

                do
                {
                    PacketCommunicatorReceiveResult result = communicator.ReceivePacket(out packet);
                    switch (result)
                    {
                        case PacketCommunicatorReceiveResult.Timeout:
                            // Timeout elapsed
                            continue;
                        case PacketCommunicatorReceiveResult.Ok:
                            packetProcesser.addPacket(packet);
                            break;
                        default:
                            throw new InvalidOperationException("The result " + result + " shoudl never be reached here");
                    }
                } while (true);
            }
            }

        private PacketProcesser packetProcesser;

        // Callback function invoked by Pcap.Net for every incoming packet
        private void PacketHandler(Packet packet)
            {
            //Console.WriteLine(packet.Timestamp.ToString("yyyy-MM-dd hh:mm:ss.fff") + " length:" + packet.Length);
            packetProcesser.addPacket(packet);
            }
        
    }
}
