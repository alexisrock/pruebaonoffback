using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service
{
    public static class TokenMethods
    {

        internal static string DecodeBase64Password(this string password)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(password);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

    }
}
