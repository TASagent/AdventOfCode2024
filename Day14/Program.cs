using AoCTools;

const string inputFile = @"../../../../input14.txt";

Console.WriteLine("Day 14 - Restroom Redoubt");
Console.WriteLine("Star 1");
Console.WriteLine();

string[] lines = File.ReadAllLines(inputFile);
//string line = File.ReadAllText(inputFile);

List<Robot> robots = lines.Select(x => new Robot(x)).ToList();

IEnumerable<int> bins = robots.Select(x => x.GetPositionAfter(100)).Select(GetBin).ToList();

long value = bins.LongCount(x => x == 0) * bins.LongCount(x => x == 1) * bins.LongCount(x => x == 2) * bins.LongCount(x => x == 3);

Console.WriteLine($"The answer is: {value}");

Console.WriteLine();
Console.WriteLine("Star 2");
Console.WriteLine();

int steps = 0;

bool found = false;
const int foundThreshold = 300;


while (!found)
{
    steps++;

    HashSet<Point2D> finalPositions = robots.Select(x => x.GetPositionAfter(steps)).ToHashSet();

    int adjacentCount = 0;

    foreach (Point2D position in finalPositions)
    {
        foreach(Point2D adjPos in position.GetAdjacent())
        {
            if (finalPositions.Contains(adjPos))
            {
                adjacentCount++;
            }
        }

        if (adjacentCount >= foundThreshold)
        {
            found = true;
            break;
        }
    }
}


int value2 = steps;

Console.WriteLine($"The answer is: {value2}");

Console.WriteLine();
Console.ReadKey();

int GetBin(Point2D pos)
{
    if (pos.x == 50 || pos.y == 51)
    {
        return 4;
    }

    int value = 0;
    if (pos.x > 50)
    {
        value += 1;
    }

    if (pos.y > 51)
    {
        value += 2;
    }

    return value;
}

class Robot
{
    public static Point2D Max => (101, 103);
    //public static Point2D Max => (11, 7);

    Point2D Position { get; }
    Point2D Velocity { get; }

    public Robot(string line)
    {
        string[] splitLine = line.Split(new[] { ',', '=', ' ' });

        Position = new Point2D(int.Parse(splitLine[1]), int.Parse(splitLine[2]));
        Velocity = new Point2D(int.Parse(splitLine[4]), int.Parse(splitLine[5]));
    }

    public Point2D GetPositionAfter(int time)
    {
        Point2D newPosition = Position + time * Velocity;

        return ((newPosition % Max) + Max) % Max;
    }
}