using Reviewer.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reviewer.Core.Interfaces
{
    public interface IRegistrationService
    {
        RegisteredUserModel Register(UserRegistrationModel model);
    }
}
