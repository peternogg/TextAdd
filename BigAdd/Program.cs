using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace BigAdd
{
    static class Program
    {
        static void Main()
        {
            long StdResult, AddResult;

            Stopwatch stp = new Stopwatch();
            float Time = 0f;
            UInt64 Counter = 0;

            long LCount, RCount;

            Console.Write("Enter left count: ");
            LCount = long.Parse(Console.ReadLine());

            Console.Write("Enter right count: ");
            RCount = long.Parse(Console.ReadLine());

            UInt64 Total = (UInt64)(LCount * RCount);


            bool Failures = false;
            List<long[]> FailureCases = new List<long[]>();

            stp.Start();

            for (long i = 1; i <= LCount; i++)
            {
                for (long j = 1; j <= RCount; j++)
                {
                    StdResult = i + j;
                    AddResult = int.Parse(Add(i.ToString(), j.ToString()));

                    Time += stp.ElapsedMilliseconds;

                    if (AddResult != StdResult)
                    {
                        FailureCases.Add(new long[]{i, j, StdResult, AddResult});
                        Failures = true;
                    }
                    Counter++;

                    Console.Clear();
                    Console.WriteLine("{0} out of {1} tests finished", Counter, Total);
                }
            }

            stp.Stop();

            if (Failures)
            {
                Console.WriteLine("Failures at");
                foreach (long[] Failure in FailureCases)
                {
                    Console.WriteLine("i: {0}, j: {1}, Std: {2}, Add: {3}", Failure[0], Failure[1], Failure[2], Failure[3]);
                }
            }
            Console.WriteLine("{0} failures; took {1} seconds", FailureCases.Count, stp.Elapsed.TotalSeconds);
            Console.ReadLine();


            //Console.WriteLine("Please enter the first number: ");
            //Number1 = Console.ReadLine();

            //Console.WriteLine("Please enter the second number: ");
            //Number2 = Console.ReadLine();

            //Console.WriteLine(Add(n1: Number1, n2: Number2));
            //Console.ReadLine();
        }

        static string Add(string n1, string n2)
        {
            string Result = "";

            // Pads both strings to equal lenghth with 0s
            PadStrings(ref n1, ref n2);

            // Column Elements (stack longer number on shorter number)
            int TopNum, BottomNum;
            // If Top + Bottom > 10, this is the 10s / 10 for adding to the next column
            int AdditionOverflow = 0;
            // Temporary result value for processing before finalizing
            int TempResult;

            // n1.Length is arbitrary as both strings have the same length
            for (int i = n1.Length - 1; i >= 0; i--)
            {
                // Convert the column elements to integers
                TopNum = int.Parse(n1[i].ToString());
                BottomNum = int.Parse(n2[i].ToString());

                // Add all column elements together
                TempResult = TopNum + BottomNum + AdditionOverflow;

                //Check if the intermediate result flows into the next column
                if (TempResult >= 10)
                {
                    // Get the 10s digit of the overflow
                    AdditionOverflow = (int)Math.Floor(TempResult / 10f);
                    // Subtract off that digit (times 10) from the result to get a number < 10
                    TempResult -= AdditionOverflow * 10;
                }
                else AdditionOverflow = 0;
                
                // Take the digit we just made and append to it Result
                // so that the new digit is the most significant digit (the digit with 
                // the greatest 'value')
                Result = TempResult.ToString() + Result;
            }

            return Result;

            #region Old Code
            /*
             * // Output string
            string Result = "";

            // Indexes only to the limit of the shorter string so that 
            // no out-of-bounds errors occur
            int ShortestLength = Math.Min(n1.Length, n2.Length);
            
            // Clone the shorter of the two strings so that it can be directly compared
            string ShorterString = (string)((n1.Length >= n2.Length) ? n1.Clone() : n2.Clone());

            // Math variables
            int TopDigit = 0, BottomDigit = 0; // Upper and lower digits in addition
            int TempResult = 0; // Temporary Variable for manipulation before finalizing in Result
            int AddNext = 0; // The amount to carry to the next column

            int Overlap = ShorterString.Length - ((ShorterString == n1) ? n2.Length : n1.Length); // Will need this in a moment
            for (int i = ShortestLength - 1; i >= 0; i--)
            {
                TopDigit = byte.Parse(n1[i].ToString());
                BottomDigit = byte.Parse(n2[i].ToString());

                TempResult = TopDigit + BottomDigit + AddNext;
                if (TempResult > 10)
                {
                    // Extract the number to add to the next column
                    AddNext = (int)Math.Floor(TempResult / 10f);
                    // Will make something like 4.4, floored is 4, converted to int
                }
                else AddNext = 0;
                // Remove any extra leftovers

                // Equal to Result = Result + TempResult.ToString();
                // non-mutable strings :B
                Result += TempResult.ToString();
            }

            Result += ReverseString(ShorterString.Substring(Overlap));
            Result = ReverseString(Result);

            return Result;*/
#endregion Old Code
        }

        static void PadStrings(ref string String1, ref string String2)
        {
            int MaxLen = (String1.Length > String2.Length) ? String1.Length : String2.Length;

            String1 = String1.PadLeft(MaxLen + 1, '0');
            String2 = String2.PadLeft(MaxLen + 1, '0');
        }
    }
}
