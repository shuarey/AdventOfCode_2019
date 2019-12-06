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
        public IntcodeComputer()
        {
            string csv = File.ReadAllText(@"C:\Users\joshu\source\repos\AdventOfCode_2019\Resources\opCodes.txt");
            string[] opCodeStrings = csv.Split(',');
            OpCodes = Array.ConvertAll(opCodeStrings, int.Parse);
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
            for (int i = 0; i < opCodes.Length; i += 4)
            {
                if (opCodes[i] == 99)
                {
                    return;
                }
                Mutate(opCodes[i], i);
            }
        }

        private void Mutate(int opCode, int currentPos)
        {
            var pos1 = OpCodes[currentPos + 1];
            var pos2 = OpCodes[currentPos + 2];
            var pos3 = OpCodes[currentPos + 3];
            switch (opCode)
            {
                case 1:
                    OpCodes[pos3] = OpCodes[pos1] + OpCodes[pos2];
                    break;
                case 2:
                    OpCodes[pos3] = OpCodes[pos1] * OpCodes[pos2];
                    break;
                default:
                    break;
            }
        }
    }
}
