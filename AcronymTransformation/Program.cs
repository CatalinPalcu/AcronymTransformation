using System;
using System.Text;

namespace AcronymTransformation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine($"Portable Network Graphics transform in: {Acronim.TransformInAcronym(" Portable Network Graphics ")}");

            Console.ReadLine();
        }
    }
}
