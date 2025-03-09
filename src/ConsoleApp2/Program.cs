using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp2
{
    internal class Program
    {
        /// <summary>
        /// Difference between two lists
        /// https://stackoverflow.com/questions/5636438/difference-between-two-lists
        /// </summary>
        /// <param name="args"></param>
        private static void Main(string[] args)
        {
            var sb = new StringBuilder();
            var list1 = new List<int> { 1, 2, 3, 4, 5 };
            var list2 = new List<int> { 3, 4, 5, 6, 7 };

            var list3 = list1.Except(list2); //list3 contains only 1, 2
            var list4 = list2.Except(list1); //list4 contains only 6, 7
            var differences = list3.Concat(list4).ToList(); //differences contains 1, 2, 6, 7
            var duplicates = list1.Intersect(list2).ToList(); //duplicates contains 3, 4, 5
            //====================================================================================================
            Console.WriteLine();
            Console.Write("list1 contains: ");
            sb.Length = 0;
            foreach (var item in list1)
            {
                sb.Append($",{item}");
            }
            Console.Write(sb.ToString().TrimStart(','));
            //====================================================================================================
            Console.WriteLine();
            Console.Write("list2 only: ");
            sb.Length = 0;
            foreach (var item in list2)
            {
                sb.Append($",{item}");
            }
            Console.Write(sb.ToString().TrimStart(','));
            //====================================================================================================
            Console.WriteLine();
            Console.Write("list3 contains only: ");
            sb.Length = 0;
            foreach (var item in list3)
            {
                sb.Append($",{item}");
            }
            Console.Write(sb.ToString().TrimStart(','));
            //====================================================================================================
            Console.WriteLine();
            Console.Write("list4 contains only: ");
            sb.Length = 0;
            foreach (var item in list4)
            {
                sb.Append($",{item}");
            }
            Console.Write(sb.ToString().TrimStart(','));
            //====================================================================================================
            Console.WriteLine();
            Console.Write("differences items: ");
            sb.Length = 0;
            foreach (var item in differences)
            {
                sb.Append($",{item}");
            }
            Console.Write(sb.ToString().TrimStart(','));
            //====================================================================================================
            Console.WriteLine();
            Console.Write("duplicates items: ");
            sb.Length = 0;
            foreach (var item in duplicates)
            {
                sb.Append($",{item}");
            }
            Console.Write(sb.ToString().TrimStart(','));
            //====================================================================================================
            Console.ReadKey();
        }
    }
}