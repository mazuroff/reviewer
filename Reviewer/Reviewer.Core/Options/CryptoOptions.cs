using System;
using System.Collections.Generic;
using System.Text;

namespace Reviewer.Core.Options
{
    public sealed class CryptoOptions
    {
        public int DerivedKeyIterations { get; set; }

        public int DerivedKeySizeBits { get; set; }

        public int SaltSizeBits { get; set; }

        public int CodeLength { get; set; }
    }
}
