using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace AdventOfCode_2019
{
    public class FuelCalculator
    {
        private string[] FuelLines;

        public FuelCalculator()
        {
            FuelLines = File.ReadAllLines(@"C:\Users\joshu\source\repos\AdventOfCode_2019\Resources\Fuel input.txt");
        }
        public int GetTotalFuel()
        {
            var totalFuel = 0;
            foreach (var line in FuelLines)
            {
                var initFuel = Calculate(Int32.Parse(line));
                totalFuel += initFuel;
                while (initFuel > 0)
                {
                    var nextFuel = Calculate(initFuel);
                    if (nextFuel > 0)
                    {
                        totalFuel += Calculate(initFuel);
                    }
                    initFuel = nextFuel;
                }
            }
            return totalFuel;
        }
        public int Calculate(double mass)
        {
            return (int)Math.Floor((mass / 3)) - 2;
        }
    }
}