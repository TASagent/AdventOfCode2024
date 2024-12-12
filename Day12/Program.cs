using AoCTools;
using System.Linq;

const string inputFile = @"../../../../input12.txt";

Console.WriteLine("Day 12 - Garden Groups");
Console.WriteLine("Star 1");
Console.WriteLine();

string[] lines = File.ReadAllLines(inputFile);
//string line = File.ReadAllText(inputFile);

Dictionary<Point2D, char> map = lines.ToDictionaryGrid();

List<char> regions = map.Values.Distinct().ToList();

long value = regions.Select(x => CalculatePrice(x, map)).Sum();

Console.WriteLine($"The answer is: {value}");

Console.WriteLine();
Console.WriteLine("Star 2");
Console.WriteLine();

long value2 = regions.Select(x => CalculatePrice2(x, map)).Sum();

Console.WriteLine($"The answer is: {value2}");

Console.WriteLine();
Console.ReadKey();

long CalculatePrice(char region, Dictionary<Point2D, char> map)
{
    HashSet<Point2D> regionPointsRemaining = map.Where(x => x.Value == region).Select(x=>x.Key).ToHashSet();

    Queue<Point2D> currentRegion = new Queue<Point2D>();

    long perimeter = 0;
    long area = 0;
    long cumulativePrice = 0;
    while (regionPointsRemaining.Count > 0)
    {
        Point2D seedPlot = regionPointsRemaining.First();
        currentRegion.Enqueue(seedPlot);
        regionPointsRemaining.Remove(seedPlot);

        while (currentRegion.Count > 0)
        {
            Point2D currentPlot = currentRegion.Dequeue();

            area++;

            foreach (Point2D adjacent in currentPlot.GetAdjacent())
            {
                if (map.ContainsKey(adjacent) && map[adjacent] == region)
                {
                    if (regionPointsRemaining.Contains(adjacent))
                    {
                        currentRegion.Enqueue(adjacent);
                        regionPointsRemaining.Remove(adjacent);
                    }
                }
                else
                {
                    perimeter++;
                }
            }
        }

        cumulativePrice += area * perimeter;
        area = 0;
        perimeter = 0;
    }

    return cumulativePrice;
}

long CalculatePrice2(char region, Dictionary<Point2D, char> map)
{
    HashSet<Point2D> regionPointsRemaining = map.Where(x => x.Value == region).Select(x => x.Key).ToHashSet();

    Queue<Point2D> currentRegion = new Queue<Point2D>();
    HashSet<(Point2D pos,Point2D dir)> fenceLocations = new HashSet<(Point2D pos, Point2D dir)>();

    long perimeter = 0;
    long area = 0;
    long cumulativePrice = 0;
    while (regionPointsRemaining.Count > 0)
    {
        Point2D seedPlot = regionPointsRemaining.First();
        currentRegion.Enqueue(seedPlot);
        regionPointsRemaining.Remove(seedPlot);

        while (currentRegion.Count > 0)
        {
            Point2D currentPlot = currentRegion.Dequeue();

            area++;

            foreach (Point2D adjacent in currentPlot.GetAdjacent())
            {
                if (map.ContainsKey(adjacent) && map[adjacent] == region)
                {
                    if (regionPointsRemaining.Contains(adjacent))
                    {
                        currentRegion.Enqueue(adjacent);
                        regionPointsRemaining.Remove(adjacent);
                    }
                }
                else
                {
                    Point2D dir = adjacent - currentPlot;
                    perimeter++;
                    fenceLocations.Add((adjacent, dir));

                    if (fenceLocations.Contains((adjacent + dir.Rotate(true), dir)))
                    {
                        perimeter--;
                    }

                    if (fenceLocations.Contains((adjacent - dir.Rotate(true), dir)))
                    {
                        perimeter--;
                    }
                }
            }
        }

        cumulativePrice += area * perimeter;
        area = 0;
        perimeter = 0;
        fenceLocations.Clear();
    }

    return cumulativePrice;
}
