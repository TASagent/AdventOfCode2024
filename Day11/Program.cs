const string inputFile = @"../../../../input11.txt";

Console.WriteLine("Day 11 - Plutonian Pebbles");
Console.WriteLine("Star 1");
Console.WriteLine();

string line = File.ReadAllText(inputFile);

List<long> values = line.Split(' ').Select(long.Parse).ToList();

IEnumerable<long> progressiveValues = values;

for (int repetitions = 0; repetitions < 25; repetitions++)
{
    progressiveValues = progressiveValues.SelectMany(Propagate);
}


long value = progressiveValues.LongCount();

Console.WriteLine($"The answer is: {value}");

Console.WriteLine();
Console.WriteLine("Star 2");
Console.WriteLine();

Dictionary<(long, long), long> countMap = new Dictionary<(long, long), long>();
long value2 = values.Sum(x => GetCount(x, 75, countMap));

Console.WriteLine($"The answer is: {value2}");

Console.WriteLine();
Console.ReadKey();


IEnumerable<long> Propagate(long value)
{
    if (value == 0)
    {
        yield return 1;
    }
    else if (value.ToString().Length % 2 == 0)
    {
        string stringifiedValue = value.ToString();
        int splitValue = stringifiedValue.Length / 2;

        yield return long.Parse(stringifiedValue[0..splitValue]);
        yield return long.Parse(stringifiedValue[(splitValue)..]);
    }
    else
    {
        yield return (value * 2024);
    }
}

long GetCount(long value, int remaining, Dictionary<(long, long), long> countMap)
{
    if (remaining == 0)
    {
        return 1;
    }

    if (!countMap.ContainsKey((value, remaining)))
    {
        IEnumerable<long> newValues = Propagate(value);
        long newCount = newValues.Sum(x => GetCount(x, remaining - 1, countMap));
        countMap[(value, remaining)] = newCount;
    }

    return countMap[(value, remaining)];
}
