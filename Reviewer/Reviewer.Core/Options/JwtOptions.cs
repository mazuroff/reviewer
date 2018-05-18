using System;
using System.Collections.Generic;
using System.Text;

namespace Reviewer.Core.Options
{
    public class JwtOptions
    {
        public string ValidIssuer { get; set; }

        public string ValidAudience { get; set; }

        public string Key { get; set; }

        public int Lifetime { get; set; }

        public int RefreshLifetime { get; set; }

        public int OneTimeCodeLifetime { get; set; }
    }
}
