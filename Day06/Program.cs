using AoCTools;

const string inputFile = @"../../../../input06.txt";

Console.WriteLine("Day 06 - Guard Gallivant");
Console.WriteLine("Star 1");
Console.WriteLine();

string[] lines = File.ReadAllLines(inputFile);
//string line = File.ReadAllText(inputFile);

Dictionary<Point2D, char> map = lines.ToDictionaryGrid();

Point2D intialGuardPosition = (0, 0);
Point2D initialGuardFacing = (0, -1);

//Cleanup
foreach (var kvp in map.ToArray())
{
    if (kvp.Value == '.')
    {
        map.Remove(kvp.Key);
    }
    else if (kvp.Value == '^')
    {
        intialGuardPosition = kvp.Key;
        map.Remove(kvp.Key);
    }
}

HashSet<(Point2D pos, Point2D facing)> visited = new HashSet<(Point2D pos, Point2D facing)>();


RunGuardExperiment(
    obstructions: map,
    visited: visited,
    initialGuardPosition: intialGuardPosition,
    initialGuardFacing: initialGuardFacing);

int value = visited.Select(x => x.pos).Distinct().Count();

Console.WriteLine($"The answer is: {value}");

Console.WriteLine();
Console.WriteLine("Star 2");
Console.WriteLine();

HashSet<Point2D> visitedLocations = new HashSet<Point2D>(visited.Select(x => x.pos));

int validPositions = 0;

foreach (Point2D testLocation in visitedLocations)
{
    visited.Clear();
    map.Add(testLocation, '#');

    if (RunGuardExperiment(
        obstructions: map,
        visited: visited,
        initialGuardPosition: intialGuardPosition,
        initialGuardFacing: initialGuardFacing))
    {
        validPositions++;
    }

    map.Remove(testLocation);
}

Console.WriteLine($"The answer is: {validPositions}");

Console.WriteLine();
Console.ReadKey();

bool RunGuardExperiment(
    Dictionary<Point2D, char> obstructions,
    HashSet<(Point2D pos, Point2D facing)> visited,
    Point2D initialGuardPosition,
    Point2D initialGuardFacing)
{
    Point2D guardPosition = intialGuardPosition;
    Point2D guardFacing = initialGuardFacing;

    while (visited.Add((guardPosition, guardFacing)))
    {
        Point2D newPos = guardPosition + guardFacing;
        if (map.ContainsKey(newPos))
        {
            guardFacing = guardFacing.Rotate(false);
            continue;
        }

        guardPosition = newPos;

        if (guardPosition.x < 0 || guardPosition.y < 0 || guardPosition.x >= lines[0].Length || guardPosition.y >= lines.Length)
        {
            return false;
        }
    }

    return true;
}