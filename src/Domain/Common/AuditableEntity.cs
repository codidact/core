namespace Codidact.Domain.Common
{
    /// <summary>
    /// Class that all of the DAL entities inherit from
    /// </summary>
    public class AuditableEntity
    {
        /// <summary>
        /// Auto Incremented Identification number
        /// </summary>
        public string Id { get; set; }
    }
}
