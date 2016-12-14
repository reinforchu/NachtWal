using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace NachtWal
{
    /// <summary>
    /// Anti XSS module
    /// </summary>
    public class XSSAuditor
    {

        public void CheckXSS()
        {
            CheckXSSQueryString();
            CheckXSSBody();
        }

        private void CheckXSSQueryString()
        {
            foreach (string key in Firewall.App.Request.QueryString.Keys)
            {
                if (!String.IsNullOrEmpty(Firewall.App.Request.QueryString[key]) || Firewall.App.Request.QueryString[key].Length > 4)
                {
                    if (Regex.IsMatch(Firewall.App.Request.QueryString[key], @"[<>"";''()]"))
                    {
                        if (!String.IsNullOrEmpty(Firewall.HttpResponseBody) && Regex.IsMatch(Firewall.HttpResponseBody, Regex.Escape(HttpUtility.UrlDecode(Firewall.App.Request.QueryString[key]))))
                        {
                            XSSProtection();
                        }
                    }
                }
            }
        }

        private void CheckXSSBody()
        {
            foreach (string key in Firewall.App.Request.Form.Keys)
            {
                if (!String.IsNullOrEmpty(Firewall.App.Request.Form[key]) || Firewall.App.Request.Form[key].Length > 5)
                {
                    if (Regex.IsMatch(Firewall.App.Request.Form[key], @"[<>"";''()]"))
                    {                        
                        if (!String.IsNullOrEmpty(Firewall.HttpResponseBody) && Regex.IsMatch(Firewall.HttpResponseBody, Regex.Escape(HttpUtility.UrlDecode(Firewall.App.Request.Form[key]))))
                        {
                            XSSProtection();
                        }
                    }
                }
            }
        }

        private void XSSProtection()
        {
            StringBuilder XSSDetectedHtml = new StringBuilder();
            XSSDetectedHtml.Append("<!DOCTYPE html>\n");
            XSSDetectedHtml.Append("<html lang=\"en\">\n");
            XSSDetectedHtml.Append("\t<head>\n");
            XSSDetectedHtml.Append("\t\t<meta charset=\"utf-8\" />\n");
            XSSDetectedHtml.Append("\t\t<meta name=\"viewport\" content=\"width=device-width,initial-scale=1\" />\n");
            XSSDetectedHtml.Append("\t\t<title>NachtWal</title>\n");
            XSSDetectedHtml.Append("\t</head>\n");
            XSSDetectedHtml.Append("\t<body>\n");
            XSSDetectedHtml.Append("\t\t<h1>XSS Attack Detected</h1>\n");
            XSSDetectedHtml.Append("\t\t<p>This page couldn't be displayed due to security problem.</p>\n");
            XSSDetectedHtml.Append("\t\t<hr>\n\t\t\t<p><i>NachtWal: Das Anwendungssystem für automatische Verteidigung</i></p>\n");
            XSSDetectedHtml.Append("\t</body>\n");
            XSSDetectedHtml.Append("</html>");
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.Write(XSSDetectedHtml);
        }
    }
}