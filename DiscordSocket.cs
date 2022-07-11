using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebSocketSharp;
using System.Threading.Tasks;
using System.Threading;

namespace Discord_Spammer
{
    class DiscordSocket
    {
        private static bool connected;

        public void OnlineToken(string token)
        {
            try
            {
                if (!connected)
                {
                    var ws = new WebSocket("wss://gateway.discord.gg/?encoding=json&v=9");
                    ws.SslConfiguration.EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12;
                    ws.Origin = "https://discord.com";
                    ws.EnableRedirection = false;
                    ws.EmitOnPing = false;
                    ws.Connect();
                    ws.Send("{\"op\":2,\"d\":{\"token\":\"" + token + "\",\"capabilities\":125,\"properties\":{\"os\":\"Windows\",\"browser\":\"Firefox\",\"device\":\"\",\"system_locale\":\"it-IT\",\"browser_user_agent\":\"Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:89.0) Gecko/20100101 Firefox/89.0\",\"browser_version\":\"89.0\",\"os_version\":\"10\",\"referrer\":\"\",\"referring_domain\":\"\",\"referrer_current\":\"\",\"referring_domain_current\":\"\",\"release_channel\":\"stable\",\"client_build_number\":" + "9999" + ",\"client_event_source\":null},\"presence\":{\"status\":\"online\",\"since\":0,\"activities\":[],\"afk\":false},\"compress\":false,\"client_state\":{\"guild_hashes\":{},\"highest_last_message_id\":\"0\",\"read_state_version\":0,\"user_guild_settings_version\":-1}}}");
                    connected = true;
                }
            }
            catch
            {

            }

        }
    }
}
