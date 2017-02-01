<%@ WebHandler Language="C#" Class="NachtWal.XSSAttackDemoSite" %>

using System;
using System.Text;
using System.Web;

namespace NachtWal
{
    /// <summary>
    /// NachtWal: XSS Attack Demo Site
    /// </summary>
    public class XSSAttackDemoSite : IHttpHandler
    {
        private string id = String.Empty;
        private string pass = String.Empty;

        public void ProcessRequest(HttpContext context)
        {
            StringBuilder HtmlBody = new StringBuilder();
            HtmlBody.Append("<!DOCTYPE html>");
            HtmlBody.Append("<html>");
            HtmlBody.Append("<head>\n");
            HtmlBody.Append("<meta charset=\"utf-8\">");
            HtmlBody.Append("<meta name=\"viewport\" content=\"width=device-width,initial-scale=1\">");
            HtmlBody.Append("<title>XSS Attack Demo Site</title>");
            HtmlBody.Append("<script>function Hello(title, body) { alert(title + body); }</script>");
            HtmlBody.Append("<script>var msg = \'\'; if (msg.length > 1) Hello(\'Message: \', \'\');</script>");
            HtmlBody.Append("</head>");
            HtmlBody.Append("<body>");
            HtmlBody.Append("<h1>XSS Attack Demo Site</h1>");
            HtmlBody.Append("<hr>");
            HtmlBody.Append("<h2>QueryString (GET)</h2>");
            HtmlBody.Append("<form action=\"./\" method=\"get\">");
            HtmlBody.Append("<i>Sanitizing</i>:&nbsp;");
            HtmlBody.Append("<input type=\"radio\" name=\"sanitizing\" value=\"no\" checked>No");
            HtmlBody.Append("<input type=\"radio\" name=\"sanitizing\" value=\"yes\">Yes<br>");
            HtmlBody.Append("<input type=\"text\" name=\"id\" value=\"&quot;&gt;&lt;script&gt;alert(0)&lt;/script&gt;\" size=\"25\">");
            HtmlBody.Append("&nbsp;&nbsp;<input type=\"submit\" value=\"Attack\"></form>");
            HtmlBody.Append("<h3>Reflected area</h3>");
            HtmlBody.Append("<div name=\"getp\"><p></p></div>");
            HtmlBody.Append("<div name=\"geti\"><input type=\"text\" value=\"\" size=\"25\"></div>");
            HtmlBody.Append("<br><hr>");
            HtmlBody.Append("<h2>Body (POST)</h2>");
            HtmlBody.Append("<form action=\"./\" method=\"post\">");
            HtmlBody.Append("<i>Sanitizing</i>:&nbsp;");
            HtmlBody.Append("<input type=\"radio\" name=\"sanitizing\" value=\"no\" checked>No");
            HtmlBody.Append("<input type=\"radio\" name=\"sanitizing\" value=\"yes\">Yes<br>");
            HtmlBody.Append("<input type=\"text\" name=\"pass\" value=\"&quot;&gt;&lt;script&gt;alert(1)&lt;/script&gt;\" size=\"25\">");
            HtmlBody.Append("&nbsp;&nbsp;<input type=\"submit\" value=\"Attack\"></form>");
            HtmlBody.Append("<h3>Reflected area</h3>");
            HtmlBody.Append("<div name=\"postp\"><p></p></div>");
            HtmlBody.Append("<div name=\"posti\"><input type=\"text\" value=\"\" size=\"25\"></div>");
            HtmlBody.Append("</body>");
            HtmlBody.Append("</html>");
            if (context.Request.QueryString["sanitizing"] == "yes" || context.Request.Form["sanitizing"] == "yes")
            {
                id = HttpUtility.HtmlEncode(context.Request.QueryString["id"]);
                pass = HttpUtility.HtmlEncode(context.Request.Form["pass"]);
            } else
            {
                id = context.Request.QueryString["id"];
                pass = context.Request.Form["pass"];
            }
                if (context.Request.HttpMethod == "GET" && !String.IsNullOrEmpty(context.Request.QueryString["id"]))
            {
                HtmlBody.Replace("<div name=\"getp\"><p></p></div>", "<div name=\"getp\"><p>" + id + "</p></div>");
                HtmlBody.Replace("<div name=\"geti\"><input type=\"text\" value=\"\" size=\"25\"></div>", "<div name=\"geti\"><input type=\"text\" value=\"" + id + "\" size=\"25\"></div>");
                HtmlBody.Replace("<script>var msg = \'\'; if (msg.length > 1) Hello(\'Message: \', \'\');</script>", "<script>var msg = \'" + id + "\'; if (msg.length > 1) Hello(\'Message: \', \'" + id + "\');</script>");
            }
            else if (context.Request.HttpMethod == "POST" && !String.IsNullOrEmpty(context.Request.Form["pass"]))
            {
                HtmlBody.Replace("<div name=\"postp\"><p></p></div>", "<div name=\"postp\"><p>" + pass + "</p></div>");
                HtmlBody.Replace("<div name=\"posti\"><input type=\"text\" value=\"\" size=\"25\"></div>", "<div name=\"posti\"><input type=\"text\" value=\"" + pass + "\" size=\"25\"></div>");
                HtmlBody.Replace("<script>var msg = \'\'; if (msg.length > 1) Hello(\'Message: \', \'\');</script>", "<script>var msg = \'" + pass + "\'; if (msg.length > 1) Hello(\'Message: \', \'" + pass + "\');</script>");
            }
            context.Response.ContentType = "text/html";
            context.Response.Write(HtmlBody);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}