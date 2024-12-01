const string inputFile = @"../../../../input01.txt";

Console.WriteLine("Day 01 - Historian Hysteria");
Console.WriteLine("Star 1");
Console.WriteLine();

string[] lines = File.ReadAllLines(inputFile);
//string line = File.ReadAllText(inputFile);

List<long> firstList = new List<long>();
List<long> secondList = new List<long>();



foreach (string line in lines)
{
    string[] splitLine = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

    firstList.Add(long.Parse(splitLine[0]));
    secondList.Add(long.Parse(splitLine[1]));
}

firstList.Sort();
secondList.Sort();


List<long> differenceList = firstList.Zip(secondList).Select(x => Math.Abs(x.First - x.Second)).ToList();


Console.WriteLine($"The answer is: {differenceList.Sum()}");

Console.WriteLine();
Console.WriteLine("Star 2");
Console.WriteLine();

Dictionary<long, long> secondListCount = new Dictionary<long, long>();


foreach(long value in secondList)
{
    if (!secondListCount.ContainsKey(value))
    {
        secondListCount.Add(value, 1);
    }
    else
    {
        secondListCount[value]++;
    }
}

Console.WriteLine($"The answer is: {firstList.Where(secondListCount.ContainsKey).Select(x=> x * secondListCount[x]).Sum()}");

Console.WriteLine();
Console.ReadKey();
