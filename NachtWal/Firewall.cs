using System;
using System.Web;

namespace NachtWal
{
    /// <summary>
    /// NachtWal: Das Anwendungssystem für automatische Verteidigung “NachtWal” lässt an.
    /// </summary>
    public class Firewall : IHttpModule
    {
        private HeaderAudtor HeaderAudit;
        private XSSAuditor XSSAudit;
        public static HttpApplication App;
        public static string HttpResponseBody;

        public void Init(HttpApplication context)
        {
            HeaderAudit = new HeaderAudtor();
            XSSAudit = new XSSAuditor();
            App = context;
            HttpResponseBody = String.Empty;
            context.BeginRequest += new EventHandler(OnBeginRequest);
            context.PreRequestHandlerExecute += new EventHandler(OnPreRequestHandlerExecute);
            context.PreSendRequestContent += new EventHandler(OnPreSendRequestContent);
            context.PreSendRequestHeaders += new EventHandler(OnPreSendRequestHeaders);
            context.EndRequest += new EventHandler(OnEndRequest);
        }

        private void OnBeginRequest(object sender, EventArgs e)
        {
            // string time = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
        }

        private void OnPreRequestHandlerExecute(object sender, EventArgs e)
        {
            App.Response.Filter = new StreamCapture(App.Response.Filter, App.Response.ContentEncoding);
        }

        private void OnPreSendRequestContent(object sender, EventArgs e)
        {
            StreamCapture filter = App.Response.Filter as StreamCapture;
            HttpResponseBody = filter.StreamContent;
            XSSAudit.CheckXSS();
        }

        private void OnPreSendRequestHeaders(object sender, EventArgs e)
        {
            // Eins.SetCSP(); // XMLの設定ファイルから読み込む実装が必要
            HeaderAudit.SetContentTypeOptions();
            HeaderAudit.SetFrameOptions();
            if (HttpContext.Current.Request.ServerVariables["HTTPS"] == "on")
            {
                HeaderAudit.SetHSTS();
                // Set-Cookie secure attr audit
            }
            HeaderAudit.SetSecureContents();
            HeaderAudit.SetXSSProtection();
            HeaderAudit.RemoveServer();
            HeaderAudit.RemoveAspNetVersion();
            HeaderAudit.RemoveProwerdBy();
            HeaderAudit.SetVersionInfo();
        }

        private void OnEndRequest(object sender, EventArgs e)
        {
            // Debug
            // App.Response.Write(HttpRuntime.AppDomainAppPath + "\n");
            // App.Response.Write(HttpRuntime.AppDomainAppVirtualPath + "\n");
            // App.Response.Write(HttpRuntime.BinDirectory + "\n");
        }

        public void Dispose()
        {
            HttpResponseBody = String.Empty;
        }
    }
}