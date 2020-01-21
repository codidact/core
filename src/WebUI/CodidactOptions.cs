namespace Codidact.WebUI
{
    public class CodidactOptions
    {
        /// <summary>Internally all requests are processed in the path schema. With this option enabled, incoming
        /// requests in the subdomain schema are rewritten to the path schema.<summary>
        public bool UseSubdomainSchema { get; set; } = false;

        public string Hostname { get; set; } = "codidact.com";

        public string CommunitySeparator { get; set; } = "community";
    }
}
