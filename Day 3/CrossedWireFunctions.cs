using System;
using System.Collections.Generic;

namespace AdventOfCode_2019
{
    public class CrossedWireFunctions
    {
        public List<Point> GetIntersections(List<LineSegment> segments_Wire1, List<LineSegment> segments_Wire2)
        {
            List<Point> intersections = new List<Point>();
            for (int i = 0; i < segments_Wire1.Count; i++)
            {
                for (int j = 0; j < segments_Wire2.Count; j++)
                {
                    Point point = LineIntersection(segments_Wire1[i], segments_Wire2[j]);
                    if (point != null)
                    {
                        intersections.Add(point);
                    }
                }
            }
            return intersections;
        }

        public Point? LineIntersection(LineSegment line1, LineSegment line2)
        {
            Point intersection = null;
            int line1Slope = line1.Point_1.X == line1.Point_2.X ? 1 : 0;
            int line2Slope = line2.Point_1.X == line2.Point_2.X ? 1 : 0;

            if (line1Slope != line2Slope)
            {
                if (line1Slope == 1)
                {
                    intersection = new Point(line1.Point_1.X, line2.Point_1.Y);
                }
                else
                {
                    intersection = new Point(line2.Point_1.X, line1.Point_1.Y);
                }
                if (!doIntersect(line1.Point_1, line1.Point_2, line2.Point_1, line2.Point_2))
                {
                    intersection = null;
                }
            }
            return intersection;
        }

        private Boolean doIntersect(Point p1, Point q1, Point p2, Point q2)
        {
            // Find the four orientations needed for general and 
            // special cases 
            int o1 = orientation(p1, q1, p2);
            int o2 = orientation(p1, q1, q2);
            int o3 = orientation(p2, q2, p1);
            int o4 = orientation(p2, q2, q1);

            // General case 
            if (o1 != o2 && o3 != o4)
                return true;

            // Special Cases 
            // p1, q1 and p2 are colinear and p2 lies on segment p1q1 
            if (o1 == 0 && onSegment(p1, p2, q1)) return true;

            // p1, q1 and q2 are colinear and q2 lies on segment p1q1 
            if (o2 == 0 && onSegment(p1, q2, q1)) return true;

            // p2, q2 and p1 are colinear and p1 lies on segment p2q2 
            if (o3 == 0 && onSegment(p2, p1, q2)) return true;

            // p2, q2 and q1 are colinear and q1 lies on segment p2q2 
            if (o4 == 0 && onSegment(p2, q1, q2)) return true;

            return false; // Doesn't fall in any of the above cases 
        }

        private int orientation(Point p, Point q, Point r)
        {

            int val = (q.Y - p.Y) * (r.X - q.X) -
                    (q.X - p.X) * (r.Y - q.Y);

            if (val == 0) return 0;

            return (val > 0) ? 1 : 2;
        }

        private Boolean onSegment(Point p, Point q, Point r)
        {
            if (q.X <= Math.Max(p.X, r.X) && q.X >= Math.Min(p.X, r.X) &&
                q.Y <= Math.Max(p.Y, r.Y) && q.Y >= Math.Min(p.Y, r.Y))
                return true;

            return false;
        }

        public int GetManhattanDistance(Point point)
        {
            return Math.Abs(point.X) + Math.Abs(point.Y);
        }

        public int GetManhattanDistance(Point point1, Point point2)
        {
            return point1.X == point2.X ? Math.Abs(point1.Y - point2.Y) : Math.Abs(point1.X - point2.X);
        }

        public List<Point> PlotPoints(string[] vectorArray)
        {
            Point origin = new Point(0, 0);
            List<Point> coordinateList = new List<Point>();
            coordinateList.Add(origin);
            foreach (var item in vectorArray)
            {
                char direction = item[0];
                int distance = Int32.Parse(item.Substring(1));
                GetNewPosition(origin, direction, distance, out origin);
                coordinateList.Add(origin);
            }
            return coordinateList;
        }

        public int GetSteps(string[] vectorArray, int index)
        {
            int steps = 0;
            for (int i = 0; i < index; i++)
            {
                steps += Int32.Parse(vectorArray[i].Substring(1));
            }
            return steps;
        }

        public List<LineSegment> SetSegments(List<Point> points)
        {
            List<LineSegment> segments = new List<LineSegment>();
            for (int i = 0; i < points.Count - 1; i++)
            {
                LineSegment segment = new LineSegment(points[i], points[i + 1]);
                segments.Add(segment);
            }
            return segments;
        }

        public Point GetLastPointBeforeIntersection(LineSegment segment, LineSegment priorSegment)
        {
            return segment.Point_1 == priorSegment.Point_1 ? segment.Point_2 : segment.Point_1;
        }

        public void GetNewPosition(Point currentPosition, char direction, int distance, out Point origin)
        {
            int currentX = currentPosition.X;
            int currentY = currentPosition.Y;
            int newX = 0;
            int newY = 0;
            switch (direction)
            {
                case 'R':
                    newX = currentX + distance;
                    origin = new Point(newX, currentY);
                    break;
                case 'L':
                    newX = currentX - distance;
                    origin = new Point(newX, currentY);
                    break;
                case 'U':
                    newY = currentY + distance;
                    origin = new Point(currentX, newY);
                    break;
                case 'D':
                    newY = currentY - distance;
                    origin = new Point(currentX, newY);
                    break;
                default:
                    origin = new Point(currentX, currentY);
                    break;
            }
        }
    }
}
