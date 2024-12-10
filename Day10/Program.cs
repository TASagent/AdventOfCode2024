using AoCTools;

const string inputFile = @"../../../../input10.txt";

Console.WriteLine("Day 10 - Hoof It");
Console.WriteLine("Star 1");
Console.WriteLine();

string[] lines = File.ReadAllLines(inputFile);

Dictionary<Point2D, char> map = lines.ToDictionaryGrid();

List<Point2D> startingPoints = map.Where(x => x.Value == '0').Select(x => x.Key).ToList();

int value = startingPoints.Sum(x => GetValue(x, map));

Console.WriteLine($"The answer is: {value}");

Console.WriteLine();
Console.WriteLine("Star 2");
Console.WriteLine();

int value2 = startingPoints.Sum(x => BuildRating(x, map)); ;

Console.WriteLine($"The answer is: {value2}");

Console.WriteLine();
Console.ReadKey();

int GetValue(Point2D startingPoint, Dictionary<Point2D, char> map)
{
    HashSet<Point2D> handledPoints = new HashSet<Point2D>();
    Queue<Point2D> pendingPoints = new Queue<Point2D>();
    pendingPoints.Enqueue(startingPoint);

    while (pendingPoints.Count > 0)
    {
        Point2D currentPoint = pendingPoints.Dequeue();
        char currentChar = map[currentPoint];

        if (currentChar == '9')
        {
            handledPoints.Add(currentPoint);
            continue;
        }

        char nextChar = (char)(currentChar + 1);

        foreach (Point2D adjacentPoint in currentPoint.GetAdjacent())
        {
            if (!map.ContainsKey(adjacentPoint)) continue;
            if (handledPoints.Contains(adjacentPoint)) continue;
            if (map[adjacentPoint] == nextChar)
            {
                pendingPoints.Enqueue(adjacentPoint);
            }
        }

        handledPoints.Add(currentPoint);
    }

    int score = handledPoints.Count(x => map[x] == '9');

    return score;
}

int BuildRating(Point2D currentPoint, Dictionary<Point2D, char> map)
{
    char currentChar = map[currentPoint];

    if (currentChar == '9')
    {
        return 1;
    }

    char nextChar = (char)(currentChar + 1);

    int branches = 0;

    foreach (Point2D adjacentPoint in currentPoint.GetAdjacent())
    {
        if (!map.ContainsKey(adjacentPoint)) continue;
        if (map[adjacentPoint] == nextChar)
        {
            branches += BuildRating(adjacentPoint, map);
        }
    }

    return branches;
}
