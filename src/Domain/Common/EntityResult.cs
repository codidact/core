using System.Collections.Generic;
using System.Linq;

namespace Codidact.Core.Domain.Common
{
    public class EntityResult<T>
    {
        public List<string> Errors { get; set; } = new List<string>();

        public bool Success { get; set; }

        public long Id { get; set; }

        public EntityResult()
        {

        }
        public EntityResult(bool success)
        {
            Success = success;
        }

        public EntityResult(long id)
        {
            Id = id;
            Success = true;
        }

        public EntityResult(params string[] error)
        {
            Errors = error.ToList();
        }
    }
}
