using AoCTools;

const string inputFile = @"../../../../input04.txt";

Console.WriteLine("Day 04 - Ceres Search");
Console.WriteLine("Star 1");
Console.WriteLine();

string[] lines = File.ReadAllLines(inputFile);
//string line = File.ReadAllText(inputFile);

Dictionary<Point2D, char> grid = lines.ToDictionaryGrid();

int count = 0;

for (int y = 0; y < lines.Length; y++)
{
    for (int x = 0; x < lines[0].Length; x++)
    {
        if (grid[(x, y)] == 'X')
        {
            Point2D centerPoint = (x, y);
            //Search!

            foreach (var direction in Point2D.Zero.GetFullAdjacent())
            {
                if (grid.ContainsKey(centerPoint + direction) && grid[centerPoint + direction] == 'M' &&
                    grid.ContainsKey(centerPoint + 2 * direction) && grid[centerPoint + 2 * direction] == 'A' &&
                    grid.ContainsKey(centerPoint + 3 * direction) && grid[centerPoint + 3 * direction] == 'S')
                {
                    count++;
                }
            }
        }
    }
}

Console.WriteLine($"The answer is: {count}");

Console.WriteLine();
Console.WriteLine("Star 2");
Console.WriteLine();

int count2 = 0;

for (int y = 1; y < lines.Length - 1; y++)
{
    for (int x = 1; x < lines[0].Length - 1; x++)
    {
        if (grid[(x, y)] == 'A')
        {
            Point2D centerPoint = (x, y);
            //Search!

            //Pluses don't count, duh!
            //if (ValidateCoords(grid, centerPoint, (1, 0)) && ValidateCoords(grid, centerPoint, (0, 1)))
            //{
            //    count2++;
            //    Console.WriteLine(centerPoint);
            //}

            if (ValidateCoords(grid, centerPoint, (1, 1)) && ValidateCoords(grid, centerPoint, (1, -1)))
            {
                count2++;
                Console.WriteLine(centerPoint);
            }
        }
    }
}

Console.WriteLine($"The answer is: {count2}");

Console.WriteLine();
Console.ReadKey();

bool ValidateCoords(Dictionary<Point2D, char> grid, Point2D center, Point2D delta)
{
    char charA = grid.SafeGet(center + delta);
    char charB = grid.SafeGet(center - delta);

    return (charA == 'M' || charA == 'S') && (charB == 'M' || charB == 'S') && charA != charB;
}

static class DictExt
{
    public static char SafeGet(this Dictionary<Point2D, char> grid, Point2D coordinate)
    {
        if (!grid.ContainsKey(coordinate))
        {
            return '\0';
        }

        return grid[coordinate];
    }
}