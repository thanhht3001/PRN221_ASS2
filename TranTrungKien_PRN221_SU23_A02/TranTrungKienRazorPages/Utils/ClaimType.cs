namespace TranTrungKienRazorPages.Utils
{
    public class ClaimType
    {
        internal const string ClaimTypeNamespace = "http://schemas.microsoft.com/ws/2008/06/identity/claims";

        public const string AccountId = ClaimTypeNamespace + "/accountid";
        public const string Role = ClaimTypeNamespace + "/role";
    }
}
