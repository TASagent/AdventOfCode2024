using System.Reflection.Metadata.Ecma335;

const string inputFile = @"../../../../input02.txt";

Console.WriteLine("Day 02 - Red-Nosed Reports");
Console.WriteLine("Star 1");
Console.WriteLine();

string[] lines = File.ReadAllLines(inputFile);

int value = lines.Select(IsSafe).Count(x=>x);

Console.WriteLine($"The answer is: {value}");

Console.WriteLine();
Console.WriteLine("Star 2");
Console.WriteLine();

int value2 = lines.Select(IsSafe2).Count(x => x); ;

Console.WriteLine($"The answer is: {value2}");

Console.WriteLine();
Console.ReadKey();

bool IsSafe(string report)
{
    List<int> results = report.Split(' ').Select(int.Parse).ToList();

    bool decreasing = results[1] < results[0];

    int minDiff = decreasing ? -3 : 1;
    int maxDiff = decreasing ? -1 : 3;


    for (int i = 0; i < results.Count - 1; i++)
    {
        if (results[i + 1] < results[i] + minDiff || results[i + 1] > results[i] + maxDiff)
        {
            return false;
        }
    }

    return true;
}

bool IsSafe2(string report)
{
    List<int> results = report.Split(' ').Select(int.Parse).ToList();

    for (int j = 0; j < results.Count; j++)
    {
        int tempRemoved = results[j];
        results.RemoveAt(j);

        bool decreasing = results[1] < results[0];

        int minDiff = decreasing ? -3 : 1;
        int maxDiff = decreasing ? -1 : 3;

        bool defectFound = false;

        for (int i = 0; i < results.Count - 1; i++)
        {
            if (results[i + 1] < results[i] + minDiff || results[i + 1] > results[i] + maxDiff)
            {
                defectFound = true;
                break;
            }
        }

        if (!defectFound) return true;

        results.Insert(j, tempRemoved);
    }

    return false;
}
