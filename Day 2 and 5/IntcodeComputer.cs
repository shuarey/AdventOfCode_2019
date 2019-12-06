using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace AdventOfCode_2019
{
    public class IntcodeComputer
    {
        private int[] OpCodes { get; set; }
        private const string path = @"C:\Users\joshu\source\repos\AdventOfCode_2019\Resources\opCodes_day";

        public enum DaysOfChallenge { day2 = 2, day5 = 5 }

        public IntcodeComputer(DaysOfChallenge day)
        {
            string path = BuildPath(day);
            string csv = File.ReadAllText(path);
            string[] opCodeStrings = csv.Split(',');
            OpCodes = Array.ConvertAll(opCodeStrings, int.Parse);
        }

        private string BuildPath(DaysOfChallenge day)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(path);
            sb.Append(day.ToString());
            sb.Append(".txt");
            return sb.ToString();
        }

        public int GetFirstPosition()
        {
            Compute(OpCodes);
            return OpCodes[0];
        }

        public void SetNounAndVerb(int noun, int verb)
        {
            OpCodes[1] = noun;
            OpCodes[2] = verb;
        }

        private void Compute(int[] opCodes)
        {
            int jump = 0;
            for (int i = 0; i < opCodes.Length; i += jump + 1)
            {
                if (opCodes[i] == 99)
                {
                    return;
                }
                Mutate(opCodes[i], i, out jump);
            }
        }

        private void Mutate(int opCode, int currentPos, out int paramCount)
        {
            var pos1 = OpCodes[currentPos + 1];
            var pos2 = OpCodes[currentPos + 2];
            var pos3 = OpCodes[currentPos + 3];
            switch (opCode)
            {
                case 1:
                    OpCodes[pos3] = OpCodes[pos1] + OpCodes[pos2];
                    paramCount = 3;
                    break;
                case 2:
                    OpCodes[pos3] = OpCodes[pos1] * OpCodes[pos2];
                    paramCount = 3;
                    break;
                default:
                    paramCount = 0;
                    break;
            }
        }
    }
}
