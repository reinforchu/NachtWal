using System;
using System.Text.RegularExpressions;

namespace NachtWal
{
    partial class XSSAuditor
    {

        private void XSSReflected(string chars)
        {
            if (!String.IsNullOrEmpty(chars) && chars.Length > 4) // Key
            {
                if (Regex.IsMatch(chars, CurrentConfiguration.XSSPossibleChars))
                {
                    try
                    {
                        if (!String.IsNullOrEmpty(this.HttpResponseBody) && Regex.IsMatch(this.HttpResponseBody, Regex.Escape(chars)))
                        {
                            ResponseRewrite("XSS Attack Blocked", "XSS Attack Detected (Possible)", "This page couldn't be displayed due to security problem.");
                        }
                    }
                    catch (Exception e)
                    {
                        ResponseRewrite("Critical stop error", "Module error (Audit Exception)", "This page couldn't be displayed due to a fatal error.");
                        Console.WriteLine(e.ToString()); // 後々エラーログ機能を実装して出力する
                    }
                }
            }
        }
    }
}