namespace AdventOfCode_2019
{
    public class LineSegment
    {
        public Point Point_1 { get; private set; }
        public Point Point_2 { get; private set; }

        public LineSegment(Point point1, Point point2)
        {
            Point_1 = point1;
            Point_2 = point2;
        }
    }
}
