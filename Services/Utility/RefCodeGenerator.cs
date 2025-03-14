﻿using System;
using System.Collections.Generic;
using System.Text;

using System.Linq;

namespace Services.Utility
{
    public static class RefCodeGenerator
    {
        private static Random random = new Random();

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
