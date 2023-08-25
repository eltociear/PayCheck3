﻿using PayCheckServerLib.Helpers;

namespace PayCheckServerLib
{
    public class ServerManager
    {
        static GSTATICServer.GSServer STATICServer;
        static PD3UDPServer UDPServer;
        public static void Start()
        {
            if (ConfigHelper.ServerConfig.Hosting.Server)
                PC3Server.Start("127.0.0.1", 443);
            if (ConfigHelper.ServerConfig.Hosting.Gstatic)
            {
                STATICServer = new GSTATICServer.GSServer("127.0.0.1", 80);
                STATICServer.Start();
            }
            if (ConfigHelper.ServerConfig.Hosting.Udp)
            {
                UDPServer = new PD3UDPServer("127.0.0.1", 6969);
                UDPServer.Start();
            }
        }

        public static void Stop()
        {
            if (ConfigHelper.ServerConfig.Hosting.Server)
                PC3Server.Stop();
            if (ConfigHelper.ServerConfig.Hosting.Gstatic)
            {
                STATICServer.Stop();
            }
            if (ConfigHelper.ServerConfig.Hosting.Udp)
            {
                UDPServer.Stop();
            }
        }
    }
}
