using Codidact.Core.Domain.Common;

namespace Codidact.Core.Domain.Entities
{
    /// <summary>
    /// Levels of trust for each member
    /// </summary>
    public class TrustLevel : AuditableEntity
    {
        /// <summary>
        /// Auto Incremented Identification number
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Name of the trust level
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// A short explaination of this trust level and its meaning
        /// </summary>
        public string Explanation { get; set; }
    }
}
