using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Options;
using Reviewer.Core.Interfaces;
using Reviewer.Core.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;

namespace Reviewer.Core.Infrastructure
{
    public class AspNetCryptoContext : ICryptoContext
    {
        private readonly CryptoOptions _options;

        public AspNetCryptoContext(IOptions<CryptoOptions> options)
        {
            _options = options.Value;

            if (_options == null) { throw new ArgumentNullException(); }
            if (_options.SaltSizeBits < 512 || _options.SaltSizeBits % 8 != 0)
            {
                throw new ArgumentException($"Salt size cannot be less than {_options.SaltSizeBits} bits and has to be dividable by 8");
            }
            if (_options.DerivedKeySizeBits < 512 || _options.DerivedKeySizeBits % 8 != 0)
            {
                throw new ArgumentException($"Derived key size cannot be less than {_options.DerivedKeySizeBits} bits and has to be dividable by 8");
            }
        }

        public string GenerateSaltAsBase64() => Convert.ToBase64String(GenerateSalt());

        public byte[] GenerateSalt()
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                byte[] salt = new byte[_options.SaltSizeBits];
                rng.GetBytes(salt);
                return salt;
            }
        }
        
        public bool ArePasswordsEqual(string password, string encodedPassword, string salt_base64)
        {
            return ArePasswordsEqual(password, encodedPassword, Convert.FromBase64String(salt_base64));
        }

        public bool ArePasswordsEqual(string password, string encodedPassword, byte[] salt)
        {
            byte[] storedKey = Convert.FromBase64String(encodedPassword);

            byte[] derivedKey = DeriveKey(password, salt);

            return storedKey.SequenceEqual(derivedKey);
        }

        public byte[] DeriveKey(string password, string salt_base64)
        {
            return DeriveKey(password, Convert.FromBase64String(salt_base64));
        }

        public byte[] DeriveKey(string password, byte[] salt)
        {
            byte[] key = KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: _options.DerivedKeyIterations,
                numBytesRequested: _options.DerivedKeySizeBits
                );
            return key;
        }
    }
}
