using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace AdventOfCode_2019
{
    public class CrossedWires
    {
        CrossedWireFunctions functions = new CrossedWireFunctions();

        private string[] Vectors_Wire1 { get; set; }
        private string[] Vectors_Wire2 { get; set; }
        private List<Point> Points_Wire1 { get; set; }
        private List<Point> Points_Wire2 { get; set; }
        private List<LineSegment> Segments_Wire1 { get; set; }
        private List<LineSegment> Segments_Wire2 { get; set; }

        public string vectorFile { get; set; } = @"C:\Users\joshu\source\repos\AdventOfCode_2019\Resources\WireVectors.txt";
        public CrossedWires()
        {
            SetVectorArrays(vectorFile);
            Points_Wire1 = functions.PlotPoints(Vectors_Wire1);
            Points_Wire2 = functions.PlotPoints(Vectors_Wire2);
            Segments_Wire1 = functions.SetSegments(Points_Wire1);
            Segments_Wire2 = functions.SetSegments(Points_Wire2);
        }

        public CrossedWires(string path)
        {
            vectorFile = path;
            SetVectorArrays(vectorFile);
            Points_Wire1 = functions.PlotPoints(Vectors_Wire1);
            Points_Wire2 = functions.PlotPoints(Vectors_Wire2);
            Segments_Wire1 = functions.SetSegments(Points_Wire1);
            Segments_Wire2 = functions.SetSegments(Points_Wire2);
        }

        public int GetClosestIntersection()
        {
            List<Point> intersections = new List<Point>();
            intersections = functions.GetIntersections(Segments_Wire1, Segments_Wire2);
            int closestIntersection = int.MaxValue;
            foreach (var intersection in intersections)
            {
                int currentIntersection = functions.GetManhattanDistance(intersection);
                if (functions.GetManhattanDistance(intersection) < closestIntersection)
                {
                    closestIntersection = currentIntersection;
                }
            }
            return closestIntersection;
        }

        private void SetVectorArrays(string input)
        {
            using (StreamReader reader = new StreamReader(input))
            {
                Vectors_Wire1 = reader.ReadLine().Split(',');
                Vectors_Wire2 = reader.ReadLine().Split(',');
            }
        }

        private List<(Point, int, int)> GetIntersectionsAndIndexes(List<LineSegment> segments_Wire1, List<LineSegment> segments_Wire2)
        {
            Point origin = new Point(0, 0);
            List<(Point, int, int)> tuples = new List<(Point, int, int)>();
            for (int i = 0; i < segments_Wire1.Count; i++)
            {
                for (int j = 1; j < segments_Wire2.Count; j++)
                {
                    Point point = functions.LineIntersection(segments_Wire1[i], segments_Wire2[j]);
                    if (point != null && point != origin)
                    {
                        (Point, int, int) intersectionAndIndex = (point, i, j);
                        tuples.Add(intersectionAndIndex);
                    }
                }
            }
            return tuples;
        }

        public int GetFewestSteps()
        {
            List<(Point, int, int)> tuples = new List<(Point, int, int)>();
            tuples = GetIntersectionsAndIndexes(Segments_Wire1, Segments_Wire2);
            var steps = int.MaxValue;
            foreach (var item in tuples)
            {
                int itemSteps = 0;

                Point intersection = item.Item1;

                var segment1 = item.Item2;
                Point wire1LastPoint = functions.GetLastPointBeforeIntersection(Segments_Wire1[segment1], Segments_Wire2[segment1 - 1]);
                var segment2 = item.Item3;
                Point wire2LastPoint = functions.GetLastPointBeforeIntersection(Segments_Wire2[segment2], Segments_Wire2[segment2 - 1]);

                itemSteps += functions.GetSteps(Vectors_Wire1, segment1);
                itemSteps += functions.GetManhattanDistance(intersection, wire1LastPoint);
                itemSteps += functions.GetSteps(Vectors_Wire2, segment2);
                itemSteps += functions.GetManhattanDistance(intersection, wire2LastPoint);

                steps = itemSteps < steps ? itemSteps : steps;
            }
            return steps;
        }

    }
}
