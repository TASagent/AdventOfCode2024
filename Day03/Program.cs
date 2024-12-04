using System.Text.RegularExpressions;

const string inputFile = @"../../../../input03.txt";

Console.WriteLine("Day 03 - Mull It Over");
Console.WriteLine("Star 1");
Console.WriteLine();

//string[] lines = File.ReadAllLines(inputFile);
string line = File.ReadAllText(inputFile);

Regex scanner = new Regex(@"mul\((\d+),(\d+)\)");

long value = 0;

foreach (Match match in scanner.Matches(line))
{
    value += long.Parse(match.Groups[1].ValueSpan) * long.Parse(match.Groups[2].ValueSpan);
}

Console.WriteLine($"The answer is: {value}");

Console.WriteLine();
Console.WriteLine("Star 2");
Console.WriteLine();

Regex scanner2 = new Regex(@"(?:mul\((\d+),(\d+)\)|do\(\)|don't\(\))");

long value2 = 0;

bool enabled = true;

foreach (Match match in scanner2.Matches(line))
{
    switch (match.Value.Substring(0, 3))
    {
        case "mul":
            if (enabled)
            {
                value2 += long.Parse(match.Groups[1].ValueSpan) * long.Parse(match.Groups[2].ValueSpan);
            }
            break;

        case "do(":
            enabled = true;
            break;

        case "don":
            enabled = false;
            break;

        default: throw new Exception();
    }
}

Console.WriteLine($"The answer is: {value2}");


Console.WriteLine($"The Regex Attempt is: {AllRegexAllTheTime(line)}");


Console.WriteLine();
Console.ReadKey();


long AllRegexAllTheTime(string line)
{
    line = new string(line.Replace('\n', '.').Reverse().ToArray());
    // Uncertain why this is failing. It's the same as https://regex101.com/r/mS5bPi/5
    Regex allRegexScanner = new Regex(@"(?=.*?(?:(\)\(t'nod)|\)\(od|$))(?:\)(\d{1,3}),(\d{1,3})\(lum)(?(1)(?!))");

    return scanner2.Matches(line).Select(ConvertMatch).Sum();
}

long ConvertMatch(Match match)
{
    return long.Parse(new string(match.Groups[3].Value.Reverse().ToArray())) * long.Parse(new string(match.Groups[4].Value.Reverse().ToArray()));
}