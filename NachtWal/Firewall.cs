using System;
using System.Web;

namespace NachtWal
{
    /// <summary>
    /// NachtWal: Das Anwendungssystem für automatische Verteidigung “NachtWal” lässt an.
    /// Reinforce Web Security
    /// </summary>
    public class Firewall : IHttpModule
    {
        private HeaderAuditor HeaderAudit;
        private XSSAuditor XSSAudit;
        private ErrorStatusHandler ErrorPage;
        private HttpApplication App;
        private string HttpResponseBody;

        public void Init(HttpApplication context)
        {
            HeaderAudit = new HeaderAuditor();
            XSSAudit = new XSSAuditor();
            ErrorPage = new ErrorStatusHandler();
            App = context;
            HttpResponseBody = String.Empty;
            context.BeginRequest += new EventHandler(OnBeginRequest);
            context.PreRequestHandlerExecute += new EventHandler(OnPreRequestHandlerExecute);
            context.PreSendRequestContent += new EventHandler(OnPreSendRequestContent);
            context.PreSendRequestHeaders += new EventHandler(OnPreSendRequestHeaders);
        }

        private void OnBeginRequest(object sender, EventArgs e)
        {
            string AuditTime = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
        }

        private void OnPreRequestHandlerExecute(object sender, EventArgs e)
        {
            App.Response.Filter = new StreamCapture(App.Response.Filter, App.Response.ContentEncoding);
        }

        private void OnPreSendRequestContent(object sender, EventArgs e)
        {
            if (HttpContext.Current.Response.StatusCode != 502 && HttpContext.Current.Response.StatusCode != 404) // 暫定対応
            {
                StreamCapture filter = App.Response.Filter as StreamCapture;
                HttpResponseBody = filter.StreamContent;
                XSSAudit.CheckXSS(App, HttpResponseBody);
            }
        }

        private void OnPreSendRequestHeaders(object sender, EventArgs e)
        {
            ErrorPage.CheckHTTPStatus(); // 必要がない場合はコメントアウトしてください
            // HeaderAudit.SetCSP(); // XMLの設定ファイルから読み込む実装が必要
            HeaderAudit.SetContentTypeOptions();
            HeaderAudit.SetFrameOptions();
            if (HttpContext.Current.Request.ServerVariables["HTTPS"] == "on")
            {
                HeaderAudit.SetHSTS();
            }
            HeaderAudit.SetSecureContents();
            HeaderAudit.SetXSSProtection();
            HeaderAudit.RemoveServer();
            HeaderAudit.RemoveEtag();
            HeaderAudit.RemoveAspNetVersion();
            HeaderAudit.RemovePoweredBy();
            if (CurrentConfiguration.ProductBanner) HeaderAudit.SetProductBanner();
        }

        public void Dispose()
        {
            HttpResponseBody = String.Empty;
        }
    }
}