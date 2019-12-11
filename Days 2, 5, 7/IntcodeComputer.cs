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
        public char Mode1 { get; set; } = '0';
        public char Mode2 { get; set; } = '0';
        public int Value1 { get; set; }
        public int Value2 { get; set; }
        public int[] PhaseSettings { get; set; }

        private const string path = @"C:\Users\joshu\source\repos\AdventOfCode_2019\Resources\opCodes_";
        private const string phaseSettings = @"C:\Users\joshu\source\repos\AdventOfCode_2019\Resources\PhaseSettings.txt";

        public enum DaysOfChallenge { day2 = 2, day5 = 5, day7 = 7 }

        public IntcodeComputer(DaysOfChallenge day, int? input)
        {
            if (input != null) { Input = (int)input; }

            string path = BuildPath(day);
            string csv = File.ReadAllText(path);
            OpCodes = Array.ConvertAll(csv.Split(','), int.Parse);
            if (day == DaysOfChallenge.day7)
            {
                Amplify();
            }
            Compute();
        }

        private void Amplify()
        {
            var phaseFile = File.ReadAllLines(phaseSettings);
            foreach (var input in phaseFile)
            {
                
            }
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
                Mutate(OpCodes[i].ToString(), i, out jump);
                ResetModes();
            }
        }

        private void Mutate(string opCode, int currentPos, out int jumpCount)
        {
            int code = 0;

            SetModes(opCode);
            SetValues(currentPos);
            code = ParseCode(opCode);

            int positionToMutate = OpCodes[currentPos + 3];

            switch (code)
            {
                case 1:
                    OpCodes[positionToMutate] = Value1 + Value2;
                    jumpCount = 3;
                    break;
                case 2:
                    OpCodes[positionToMutate] = Value1 * Value2;
                    jumpCount = 3;
                    break;
                case 3:
                    positionToMutate = GetPosition(Mode1, currentPos + 1);
                    OpCodes[positionToMutate] = Input;
                    jumpCount = 1;
                    break;
                case 4:
                    diagnosticCodes.Add(Value1);
                    jumpCount = 1;
                    break;
                case 5:
                    jumpCount = Value1 != 0 ? Math.Abs(currentPos + 1 - Value2) : 2;
                    break;
                case 6:
                    jumpCount = Value1 == 0 ? Math.Abs(currentPos + 1 - Value2) : 2;
                    break;
                case 7:
                    OpCodes[positionToMutate] = Value1 < Value2 ? 1 : 0;
                    jumpCount = 3;
                    break;
                case 8:
                    OpCodes[positionToMutate] = Value1 == Value2 ? 1 : 0;
                    jumpCount = 3;
                    break;
                default:
                    jumpCount = 0;
                    break;
            }
        }

        private void SetValues(int currentPosition)
        {
            Value1 = SetValue(Mode1, OpCodes[++currentPosition]);
            Value2 = SetValue(Mode2, OpCodes[++currentPosition]);
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

        public int GetDiagnosticCode() => diagnosticCodes.Last();

        public void SetNounAndVerb(int noun, int verb) { OpCodes[1] = noun; OpCodes[2] = verb; }

        public int GetFirstPosition() => OpCodes[0];
    }
}
