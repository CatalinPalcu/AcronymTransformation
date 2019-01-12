using System;
using System.Collections.Generic;
using System.Text;

namespace AcronymTransformation
{
    public static class Acronim
    {
        public static string TransformInAcronym(string s)
        {
            string acr = "";
            s = s.Trim();
            while (s.IndexOf("  ") >= 0)
                s = s.Replace("  ", " ");

            string[] words = s.Split(' ');
            foreach (string word in words)
                acr += word[0];

            return acr;
        }
    }
}
