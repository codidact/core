namespace Codidact.Core.WebApp.Models
{
    public class IdentityOptions
    {
        public string Authority { get; set; }
        public bool RequireHttpsMetadata { get; set; }
        public string ClientId { get; set; }
        public string ResponseType { get; set; }
        public string ResponseMode { get; set; }
        public string CallbackPath { get; set; }
        public string SignedOutCallbackPath { get; set; }
    }
}
