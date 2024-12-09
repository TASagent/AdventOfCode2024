using AoCTools;

const string inputFile = @"../../../../input08.txt";

Console.WriteLine("Day 08 - Resonant Collinearity");
Console.WriteLine("Star 1");
Console.WriteLine();

string[] lines = File.ReadAllLines(inputFile);

Dictionary<Point2D, char> map = lines.ToDictionaryGrid();

Point2D max = (lines[0].Length, lines.Length);

IEnumerable<char> targets = map.Values.Distinct().Where(x => x != '.').ToList();

int totalCount = 0;

HashSet<Point2D> antinodePoints = new HashSet<Point2D>();

foreach (char target in targets)
{
    foreach (Point2D initialPoint in map.Where(x => x.Value == target).Select(x => x.Key))
    {
        foreach (Point2D finalPoint in map.Where(x => x.Value == target && x.Key != initialPoint).Select(x => x.Key))
        {
            Point2D delta = finalPoint - initialPoint;
            Point2D newPoint = initialPoint + 2 * delta;
            if (newPoint.x < max.x && newPoint.y < max.y && newPoint.x >= 0 && newPoint.y >= 0)
            {
                antinodePoints.Add(newPoint);
            }
        }
    }
}

Console.WriteLine($"The answer is: {antinodePoints.Count}");

Console.WriteLine();
Console.WriteLine("Star 2");
Console.WriteLine();

antinodePoints.Clear();

foreach (char target in targets)
{
    HashSet<Point2D> targetPoints = map.Where(x => x.Value == target).Select(x => x.Key).ToHashSet();

    foreach (Point2D initialPoint in map.Where(x => x.Value == target).Select(x => x.Key))
    {
        foreach (Point2D finalPoint in map.Where(x => x.Value == target && x.Key != initialPoint).Select(x => x.Key))
        {
            Point2D delta = finalPoint - initialPoint;
            Point2D newPoint = initialPoint + delta;

            while (newPoint.x < max.x && newPoint.y < max.y && newPoint.x >= 0 && newPoint.y >= 0)
            {
                antinodePoints.Add(newPoint);
                newPoint = newPoint + delta;
            }
        }
    }
}

Console.WriteLine($"The answer is: {antinodePoints.Count}");

Console.WriteLine();
Console.ReadKey();
