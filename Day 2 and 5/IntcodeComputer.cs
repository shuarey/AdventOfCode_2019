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
        public int Input { get; set; }
        public List<int> diagnosticCodes = new List<int>();
        public int LastCode { get; set; }
        public char Mode1 { get; set; } = '0';
        public char Mode2 { get; set; } = '0';
        public int Value1 { get; set; }
        public int Value2 { get; set; }

        private const string path = @"C:\Users\joshu\source\repos\AdventOfCode_2019\Resources\opCodes_";

        public enum DaysOfChallenge { day2 = 2, day5 = 5 }

        public IntcodeComputer(DaysOfChallenge day, int input)
        {
            Input = input;
            string path = BuildPath(day);
            string csv = File.ReadAllText(path);
            OpCodes = Array.ConvertAll(csv.Split(','), int.Parse);
            Compute();
        }

        private string BuildPath(DaysOfChallenge day)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(path);
            sb.Append(day.ToString());
            sb.Append(".txt");
            return sb.ToString();
        }

        private void Compute()
        {
            int jump = 0;
            for (int i = 0; i < OpCodes.Length; i += jump + 1)
            {
                if (ShouldTerminate(OpCodes[i].ToString())) { break; }
                int newPosition = 0;
                bool needsToJump = ShouldJump(OpCodes[i].ToString(), i, out newPosition);
                if (needsToJump)
                {
                    i = newPosition;
                    ResetModes();
                    jump = -1;
                    continue;
                }
                Mutate(OpCodes[i].ToString(), i, out jump);
                ResetModes();
            }
        }

        private bool ShouldJump(string opCode, int currentPosition, out int position)
        {
            int code = 0;
            SetModes(opCode);
            SetValues(currentPosition);
            position = currentPosition;
            code = ParseCode(opCode);
            if (code == 5 && Value1 != 0)
            {
                position = Value2;
                return true;
            }
            if (code == 6 && Value1 == 0)
            {
                position = Value2;
                return true;
            }
            return false;
        }

        private void Mutate(string opCode, int currentPos, out int paramCount)
        {
            int code = 0;

            SetModes(opCode);
            SetValues(currentPos);
            code = ParseCode(opCode);

            var positionToMutate = OpCodes[currentPos + 3];

            switch (code)
            {
                case 1:
                    OpCodes[positionToMutate] = Value1 + Value2;
                    paramCount = 3;
                    break;
                case 2:
                    OpCodes[positionToMutate] = Value1 * Value2;
                    paramCount = 3;
                    break;
                case 3:
                    positionToMutate = GetPosition(Mode1, currentPos + 1);
                    OpCodes[positionToMutate] = Input;
                    paramCount = 1;
                    break;
                case 4:
                    positionToMutate = GetPosition(Mode1, currentPos + 1);
                    diagnosticCodes.Add(OpCodes[Value1]);
                    paramCount = 1;
                    break;
                case 7:
                    OpCodes[positionToMutate] = Value1 < Value2 ? 1 : 0;
                    paramCount = 3;
                    break;
                case 8:
                    OpCodes[positionToMutate] = Value1 == Value2 ? 1 : 0;
                    paramCount = 3;
                    break;
                default:
                    paramCount = 0;
                    break;
            }
        }

        private void SetValues(int currentPosition)
        {
            Value1 = SetValue(Mode1, OpCodes[currentPosition + 1]);
            Value2 = SetValue(Mode2, OpCodes[currentPosition + 2]);
        }

        private int SetValue(char mode, int param) => mode == '0' ? OpCodes[param] : param;

        private void SetModes(string opCode)
        {
            if (opCode.Length == 3)
            {
                Mode1 = opCode[0];
            }
            else if (opCode.Length == 4)
            {
                Mode1 = opCode[1];
                Mode2 = opCode[0];
            }
        }

        private void ResetModes() { Mode1 = '0'; Mode2 = '0'; }

        private int GetPosition(char mode, int currentPosition) => mode == '0' ? 
            OpCodes[currentPosition] : currentPosition;

        private int ParseCode(string opCode) => opCode.Length > 1 ?
            Int32.Parse(opCode.Substring(opCode.Length - 2)) : Int32.Parse(opCode);

        private bool ShouldTerminate(string opCode) => ParseCode(opCode) == 99 ? true : false;

        public int GetFirstPosition() => OpCodes[0];

        public int GetDiagnosticCode() => diagnosticCodes.Last();

        public void SetNounAndVerb(int noun, int verb) { OpCodes[1] = noun; OpCodes[2] = verb; }
    }
}
