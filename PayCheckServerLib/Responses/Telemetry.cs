﻿using NetCoreServer;
using System.Linq.Expressions;

namespace PayCheckServerLib.Responses
{
    public class Telemetry
    {
        [HTTP("POST", "/game-telemetry/v1/protected/events")]
        public static bool ProtectedEvents(HttpRequest request, PC3Server.PC3Session session)
        {
            if (!Directory.Exists("Telemetry")) { Directory.CreateDirectory("Telemetry"); }
            try {
                File.WriteAllText("Telemetry/" + DateTime.Now.ToString("s").Replace(":", "-") + ".json", request.Body);
            } catch(IOException e) {
                Debugger.PrintError(String.Format("Exception occurred while writing telemetry: {0}", e.ToString()));
            }
            session.SendResponse(session.Response.MakeOkResponse());
            return true;
        }
    }
}