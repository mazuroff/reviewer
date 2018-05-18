using System;
using System.Collections.Generic;
using System.Text;

namespace Reviewer.Core.Interfaces
{
    public interface IAuthenticatedUserProvider
    {
        bool IsAuthenticated { get; }

        string Name { get; }

        Guid UserId { get; }

        string Email { get; }
    }
}
