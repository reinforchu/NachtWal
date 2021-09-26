using System;
using System.Text;
using System.Web;

namespace NachtWal
{
    /// <summary>
    /// Hide HTTP Error Page
    /// </summary>
   public class ErrorStatusHandler
    {
        public void CheckHTTPStatus()
        {
            int StatusCode = 100;
            StatusCode = HttpContext.Current.Response.StatusCode;
            if (StatusCode == 502)
            {
                try {
                    HTTPResponseRewrite("Bad Request", StatusCode.ToString() + " Error message suppression", "An error message with unsafe content has been detected.");
                } catch (Exception e) {
                    HTTPResponseRewrite("Critical stop error", "Module error (Audit Exception)", "This page couldn't be displayed due to a fatal error.");
                    Console.WriteLine(e.ToString()); // 後々エラーログ機能を実装して出力する
                    }
                }
        }

        private void HTTPResponseRewrite(string Title, string Head, string Message)
        {
            StringBuilder Body = new StringBuilder();
            Body.Append("<!DOCTYPE html>\n");
            Body.Append("<html>\n");
            Body.Append("\t<head>\n");
            Body.Append("\t\t<title>" + Title + "</title>\n");
            Body.Append("\t</head>\n");
            Body.Append("\t<body>\n");
            Body.Append("\t\t<h1>" + Head + "</h1>\n");
            Body.Append("\t\t<p>" + Message + "</p>\n");
            Body.Append("\t\t<hr>\n\t\t<address>NachtWal/" + AssemblyInformation.Version + " (" + AssemblyInformation.Release + ") Server at " + HttpContext.Current.Request.ServerVariables["SERVER_NAME"] + " Port " + HttpContext.Current.Request.ServerVariables["SERVER_PORT"] + "</address>\n");
            Body.Append("\t</body>\n");
            Body.Append("</html>");
            HttpContext.Current.Response.ContentType = "text/html";
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.Write(Body);
        }

    }
}