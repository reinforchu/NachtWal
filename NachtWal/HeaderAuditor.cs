using System.Web;

namespace NachtWal
{
    /// <summary>
    /// Vulnerability mitigation secure settings
    /// </summary>
    public class HeaderAuditor
    {

        public void SetProductBanner()
        {
            AddHeader("X-Security-By", AssemblyInformation.Name + "/" + AssemblyInformation.Version);
        }

        public void SetCSP()
        {
            AddHeader("Content-Security-Policy", "default-src 'self'");
        }

        public void SetContentTypeOptions()
        {
            AddHeader("X-Content-Type-Options", "nosniff");
        }

        public void SetFrameOptions()
        {
            AddHeader("X-Frame-Options", "DENY");
        }

        public void SetHSTS()
        {
            AddHeader("Strict-Transport-Security", "max-age=15768000");
        }

        public void SetSecureContents()
        {
            AddHeader("Cache-Control", "no-cache, no-store");
            AddHeader("Expires", "0");
            AddHeader("Pragma", "no-cache");
        }

        public void SetXSSProtection()
        {
            AddHeader("X-XSS-Protection", "1; mode=block");
        }

        public void RemoveServer()
        {
            RemoveHeader("Server");
        }

        public void RemoveEtag()
        {
            RemoveHeader("Etag");
        }

        public void RemoveAspNetVersion()
        {
            RemoveHeader("X-AspNet-Version");
            RemoveHeader("X-AspNetMvc-Version");
        }

        public void RemovePoweredBy()
        {
            RemoveHeader("X-Powered-By");
            // Issue #1 https://github.com/reinforchu/NachtWal/issues/1
        }

        private void AddHeader(string field, string value)
        {
            HttpContext.Current.Response.Headers.Add(field, value);
        }

        private void RemoveHeader(string field)
        {
            HttpContext.Current.Response.Headers.Remove(field);
        }
    }
}