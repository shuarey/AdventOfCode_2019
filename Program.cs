using System;

namespace AdventOfCode_2019
{
    class Program
    {
        
        static void Main(string[] args)
        {
            int input = 5;
            IntcodeComputer computer = new IntcodeComputer(IntcodeComputer.DaysOfChallenge.day5, input);
            Console.WriteLine($"First position: {computer.GetFirstPosition()}");
            Console.WriteLine($"Diagnostic code: {computer.GetDiagnosticCode()}");
        }

        //public int[] cellCompete(int[] states, int days)
        //{
        //    while (days > 1)
        //    {
        //        int[] newState = new int[8];
        //        for (int i = 0; i < states.Length; i++)
        //        {
        //            if (states[] )
        //            {

        //            }
        //        }
        //    }

        //}

        public void mutate(int[] states, int pos)
        {

        }
    }
}
