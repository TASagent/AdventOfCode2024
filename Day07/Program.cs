const string inputFile = @"../../../../input07.txt";

Console.WriteLine("Day 07 - Bridge Repair");
Console.WriteLine("Star 1");
Console.WriteLine();

string[] lines = File.ReadAllLines(inputFile);
//string line = File.ReadAllText(inputFile);

List<TrainLine> trainLines = lines.Select(x=> new TrainLine(x)).ToList();


long value = trainLines.Where(x => x.IsValid()).Sum(x => x.TestValue);

Console.WriteLine($"The answer is: {value}");

Console.WriteLine();
Console.WriteLine("Star 2");
Console.WriteLine();

long value2 = trainLines.Where(x => x.IsValid2()).Sum(x => x.TestValue);

Console.WriteLine($"The answer is: {value2}");

Console.WriteLine();
Console.ReadKey();

class TrainLine
{
    public long TestValue { get; }
    public List<long> Values { get; }
    public TrainLine(string line)
    {
        string[] splitLine = line.Split(new[] { ':', ' ' }, StringSplitOptions.RemoveEmptyEntries);

        TestValue = long.Parse(splitLine[0]);
        Values = splitLine.Skip(1).Select(long.Parse).ToList();
    }

    public bool IsValid()
    {
        return CheckValues(1, Values[0]);
    }

    public bool IsValid2()
    {
        return CheckValues2(1, Values[0]);
    }

    public bool CheckValues(int index, long total)
    {
        if (index == Values.Count)
        {
            return total == TestValue;
        }

        if (CheckValues(index + 1, total + Values[index]))
            return true;

        if (CheckValues(index + 1, total * Values[index]))
            return true;

        return false;
    }

    public bool CheckValues2(int index, long total)
    {
        if (index == Values.Count)
        {
            return total == TestValue;
        }

        if (CheckValues2(index + 1, total + Values[index]))
            return true;

        if (CheckValues2(index + 1, total * Values[index]))
            return true;

        if (CheckValues2(index + 1, long.Parse($"{total}{Values[index]}")))
            return true;

        return false;
    }
}