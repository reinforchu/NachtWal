namespace NachtWal
{
    /// <summary>
    /// Current Configuration
    /// </summary>
    public class CurrentConfiguration
    {
        public static readonly bool ProductBanner = true;
        public static readonly bool XSSAudit = true;
        public static readonly bool XSSAuditReflected = false;
        public static readonly string XSSPossibleChars = @"[<>"".;+\\'()]";
    }
}