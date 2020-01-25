using System;
using System.Collections.Generic;
using System.Text;

namespace Codidact.Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        public string GetUserId();
        public long GetMemberId();
    }
}
