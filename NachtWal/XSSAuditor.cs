using System;
using System.Collections.Specialized;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace NachtWal
{
    /// <summary>
    /// Anti-XSS Attack
    /// </summary>
    public class XSSAuditor
    {
        public static readonly string XSSPossibleChars = @"[<>"".;\\''()]";
        private HttpApplication App;
        private string HttpResponseBody = String.Empty;

        public void CheckXSS(HttpApplication Application, string ResponseBody)
        {
            App = Application;
            HttpResponseBody = ResponseBody;
            CheckXSSParamKey(this.App.Request.QueryString); // QueryString key
            CheckXSSParamValue(this.App.Request.QueryString); // QueryString value
            CheckXSSParamKey(this.App.Request.Form); // Body key
            CheckXSSParamValue(this.App.Request.Form); // Body value
        }

        private void CheckXSSParamKey(NameValueCollection Param)
        {
            foreach (string key in Param.Keys)
            {
                if (!String.IsNullOrEmpty(key) && key.Length > 4)
                {
                    if (Regex.IsMatch(key, XSSPossibleChars))
                    {
                        try
                        {
                            if (!String.IsNullOrEmpty(this.HttpResponseBody) && Regex.IsMatch(this.HttpResponseBody, Regex.Escape(key)))
                            {
                                XSSProtection();
                            }
                        }
                        catch (Exception e)
                        {
                            XSSAuditRegxException();
                            Console.WriteLine(e.ToString()); // 後々エラーログ機能を実装して出力する
                        }
                    }
                }
            }
        }

        private void CheckXSSParamValue(NameValueCollection Param)
        {
            foreach (string key in Param.Keys)
            {
                if (!String.IsNullOrEmpty(Param[key]) && Param[key].Length > 4)
                {
                    if (Regex.IsMatch(Param[key], XSSPossibleChars))
                    {
                        try
                        {
                            if (!String.IsNullOrEmpty(this.HttpResponseBody) && Regex.IsMatch(this.HttpResponseBody, Regex.Escape(Param[key])))
                            {
                                XSSProtection();
                            }
                        }
                        catch (Exception e)
                        {
                            XSSAuditRegxException();
                            Console.WriteLine(e.ToString()); // 後々エラーログ機能を実装して出力する
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
            XSSDetectedHtml.Append("\t\t<hr>\n\t\t<address>NachtWal: Das Anwendungssystem für automatische Verteidigung</address>\n");
            XSSDetectedHtml.Append("\t</body>\n");
            XSSDetectedHtml.Append("</html>");
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.Write(XSSDetectedHtml);
        }

        private void XSSAuditRegxException()
        {
            StringBuilder XSSAuditRegxExceptionHtml = new StringBuilder();
            XSSAuditRegxExceptionHtml.Append("<!DOCTYPE html>\n");
            XSSAuditRegxExceptionHtml.Append("<html>\n");
            XSSAuditRegxExceptionHtml.Append("\t<head>\n");
            XSSAuditRegxExceptionHtml.Append("\t\t<title>Server error</title>\n");
            XSSAuditRegxExceptionHtml.Append("\t</head>\n");
            XSSAuditRegxExceptionHtml.Append("\t<body>\n");
            XSSAuditRegxExceptionHtml.Append("\t\t<h1>Module error (Exception)</h1>\n");
            XSSAuditRegxExceptionHtml.Append("\t\t<p>This page couldn't be displayed due to a fatal error.</p>\n");
            XSSAuditRegxExceptionHtml.Append("\t\t<hr>\n\t\t<address>NachtWal: Das Anwendungssystem für automatische Verteidigung</address>\n");
            XSSAuditRegxExceptionHtml.Append("\t</body>\n");
            XSSAuditRegxExceptionHtml.Append("</html>");
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.Write(XSSAuditRegxExceptionHtml);
        }
    }
}