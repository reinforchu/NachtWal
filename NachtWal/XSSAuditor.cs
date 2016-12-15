using System;
using System.Collections.Specialized;
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
        private HttpApplication App;
        private string HttpResponseBody;

        /// <summary>
        /// Constructor
        /// Initialize
        /// </summary>
        public XSSAuditor()
        {
            HttpResponseBody = String.Empty;
        }

        public void CheckXSS(HttpApplication Application, string ResponseBody)
        {
            App = Application;
            HttpResponseBody = ResponseBody;
            CheckXSSParamValue(this.App.Request.QueryString); // QueryString value
            CheckXSSParamValue(this.App.Request.Form); // Body value
        }

        private void CheckXSSParamValue(NameValueCollection Param)
        {
            foreach (string key in Param.Keys)
            {
                if (!String.IsNullOrEmpty(Param[key]) && Param[key].Length > 4)
                {
                    if (Regex.IsMatch(Param[key], @"[<>""=;\\:''()]"))
                    {
                        if (!String.IsNullOrEmpty(this.HttpResponseBody) && Regex.IsMatch(this.HttpResponseBody, Regex.Escape(HttpUtility.UrlDecode(Param[key]))))
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
            XSSDetectedHtml.Append("<html>\n");
            XSSDetectedHtml.Append("\t<head>\n");
            XSSDetectedHtml.Append("\t\t<title>XSS Attack Blocked</title>\n");
            XSSDetectedHtml.Append("\t</head>\n");
            XSSDetectedHtml.Append("\t<body>\n");
            XSSDetectedHtml.Append("\t\t<h1>XSS Attack Detected (Possible)</h1>\n");
            XSSDetectedHtml.Append("\t\t<p>This page couldn't be displayed due to security problem.</p>\n");
            XSSDetectedHtml.Append("\t\t<hr>\n\t\t\t<address>NachtWal: Das Anwendungssystem für automatische Verteidigung</address>\n");
            XSSDetectedHtml.Append("\t</body>\n");
            XSSDetectedHtml.Append("</html>");
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.Write(XSSDetectedHtml);
        }
    }
}