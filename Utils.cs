using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leaf.xNet;
using System.Security.Authentication;

namespace Discord_Spammer
{
    class Utils
    {
        public static Random random = new Random(Guid.NewGuid().GetHashCode());
        public static string RandomCookie(int a)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, a)
              .Select(s => s[random.Next(s.Length)]).ToArray()); // Return random char string
        }
        public void headers(HttpRequest r, string auth)
        {
            r.IgnoreProtocolErrors = true;
            r.KeepTemporaryHeadersOnRedirect = false;
            r.ClearAllHeaders();
            r.EnableMiddleHeaders = false;
            r.AllowEmptyHeaderValues = false;
            r.IgnoreInvalidCookie = true;
            r.AddHeader("Accept", "*/*");
            r.AddHeader("Pragma", "no-cache");
            r.AddHeader("Cache-Control", "no-cache");
            r.AddHeader("Accept-Language", "en-us");
            r.AddHeader("Host", "discord.com");
            r.AddHeader("User-Agent", "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_13_6) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/13.1.2 Safari/605.1.15");
            r.AddHeader("Referer", "https://discord.com/channels/@me");
            r.AddHeader("Accept-Encoding", "br, gzip, deflate");
            r.AddHeader("Connection", "keep-alive");
            r.AddHeader("Origin", "https://discord.com");
            r.AddHeader("Authorization", auth);
            r.AddHeader("Cookie", $"__dcfduid={RandomCookie(43)}; __sdcfduid={RandomCookie(32)}; locale=en-US");
            r.AddHeader("X-Debug-Options", "bugReporterEnabled");
            r.AddHeader("X-Discord-Locale", "en-US");
            r.AddHeader("X-Super-Properties", "eyJvcyI6ICJNYWMgT1MgWCIsICJicm93c2VyIjogIlNhZmFyaSIsICJkZXZpY2UiOiAiIiwgInN5c3RlbV9sb2NhbGUiOiAiZW4tdXMiLCAiYnJvd3Nlcl91c2VyX2FnZW50IjogIk1vemlsbGEvNS4wIChNYWNpbnRvc2g7IEludGVsIE1hYyBPUyBYIDEwXzEzXzYpIEFwcGxlV2ViS2l0LzYwNS4xLjE1IChLSFRNTCwgbGlrZSBHZWNrbykgVmVyc2lvbi8xMy4xLjIgU2FmYXJpLzYwNS4xLjE1IiwgImJyb3dzZXJfdmVyc2lvbiI6ICIxMy4xLjIiLCAib3NfdmVyc2lvbiI6ICIxMC4xMy42IiwgInJlZmVycmVyIjogIiIsICJyZWZlcnJpbmdfZG9tYWluIjogIiIsICJyZWZlcnJlcl9jdXJyZW50IjogIiIsICJyZWZlcnJpbmdfZG9tYWluX2N1cnJlbnQiOiAiIiwgInJlbGVhc2VfY2hhbm5lbCI6ICJzdGFibGUiLCAiY2xpZW50X2J1aWxkX251bWJlciI6IDEzNTQwMiwgImNsaWVudF9ldmVudF9zb3VyY2UiOiBudWxsfQ==");
            r.SslProtocols = SslProtocols.Tls12;
        } 
        public string RandomNonce(int a)
        {
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, a)
              .Select(s => s[random.Next(s.Length)]).ToArray()); // Return random char string
        }
    }
}
