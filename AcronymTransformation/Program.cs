using System;
using System.Text;

namespace AcronymTransformation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Console.WriteLine("Avoid this");
            Console.WriteLine($"Portable Network Graphics transform in: {Acronim.TransformInAcronym(" Portable           Network Graphics ")}");

            string s = "( Three Letter Acronyms ) Help generate some jargon by writing Three , Letter : Acronyms a program that converts a long name like Portable Network Graphics  to its acronym.";
            Console.WriteLine(Acronim.TransformAcronymFromPhrase2(s));

            Console.ReadLine();
        }
    }

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

        public static string TransformAcronymFromPhrase(string phrase)
        {
            StringBuilder newPhrase = new StringBuilder();

            int startIndex = 0, 
                endIndex = 0;
            bool insideAcronym = false,
                startWithUpperLetter = true;
            char[] separator = { ',', '.', ':', ';', '?', '!','(',')' };

            while (endIndex < phrase.Length)
            {
                if (!startWithUpperLetter)
                {
                    while (endIndex < phrase.Length && (phrase[endIndex] < 'A' || phrase[endIndex] > 'Z'))
                    {
                        newPhrase.Append(phrase[endIndex]);
                        endIndex++;
                    }

                    startIndex = endIndex;
                    endIndex++;
                    startWithUpperLetter = true;
                }
                else if (!insideAcronym)
                {
                    while (endIndex < phrase.Length && Char.IsLower(phrase[endIndex]))
                    {
                        endIndex++;
                    }
                    while (endIndex < phrase.Length && phrase[endIndex] == ' ')
                    {
                        endIndex++;
                    }
                    if (endIndex < phrase.Length && !Char.IsUpper(phrase[endIndex]))
                    {
                        startWithUpperLetter = false;
                        newPhrase.Append(phrase.Substring(startIndex, endIndex - startIndex + 1));
                        startIndex = endIndex;
                    } else
                    {
                        insideAcronym = true;
                    }
                }
                else // suntem in interiorul acronimului
                {
                    while (endIndex < phrase.Length-1 && insideAcronym)
                    {
                        if (Array.IndexOf(separator, phrase[endIndex]) >= 0)
                        {
                            insideAcronym = false;
                            endIndex--;
                            break;
                        }
                        if (phrase[endIndex] == ' ' && !Char.IsUpper(phrase[endIndex + 1]))
                        {
                            insideAcronym = false;
                            break;
                        }
                        endIndex++;
                    }
                    if (endIndex == phrase.Length - 2 && Char.IsLetter(phrase[endIndex + 1]))
                        endIndex++;

                    while (phrase[endIndex] == ' ')
                        endIndex--;

                    newPhrase.Append(TransformInAcronym(phrase.Substring(startIndex, endIndex - startIndex + 1)));
                    startIndex = endIndex;
                    startWithUpperLetter = false;
                    insideAcronym = false;
                }
                endIndex++;
            }
            return newPhrase.ToString();
        }

        public static string TransformAcronymFromPhrase2(string phrase)
        {
            StringBuilder newPhrase = new StringBuilder();

            int startIndex = 0,
                endIndex = 0;
            char[] separator = { ',', '.', ':', ';', '?', '!', '(', ')' };
            char[] upperLetter = new char[26]; // vom retine toate literele mari

            for (int i = 0; i < 26; i++)
                upperLetter[i] = (Char)((int)'A' + i);

            while (endIndex < phrase.Length)
            {
                int firstUpperLetter = phrase.IndexOfAny(upperLetter,startIndex);
                if (firstUpperLetter >= startIndex)
                {
                    newPhrase.Append(phrase.Substring(startIndex, firstUpperLetter - startIndex));
                    startIndex = firstUpperLetter;
                    endIndex = startIndex;

                    int firstSeparator = phrase.IndexOfAny(separator, startIndex);
                    if (firstSeparator < 0)
                        firstSeparator = phrase.Length;

                    do
                    {
                        int indexSpace = phrase.IndexOf(' ', endIndex);
                        if (indexSpace >= 0)
                            endIndex = indexSpace;
                        else
                            endIndex = phrase.Length;
                        while (endIndex < firstSeparator && phrase[endIndex] == ' ')
                            endIndex++;
                    } while (endIndex < firstSeparator && endIndex == phrase.IndexOfAny(upperLetter,endIndex));
                    endIndex--;

                    if (endIndex >= firstSeparator)
                        endIndex = firstSeparator - 1;

                    while (phrase[endIndex] == ' ')
                        endIndex--;

                    if (phrase.IndexOf(' ', startIndex) < endIndex)
                    {
                        newPhrase.Append(TransformInAcronym(phrase.Substring(startIndex, endIndex - startIndex + 1)));
                    } else
                    {
                        newPhrase.Append(phrase.Substring(startIndex, endIndex - startIndex + 1));
                    }

                    startIndex = endIndex + 1;
                }
                else
                {
                    newPhrase.Append(phrase.Substring(startIndex));
                    startIndex = phrase.Length;
                    endIndex = phrase.Length;
                }
            }
            return newPhrase.ToString();
        }
    }
}
