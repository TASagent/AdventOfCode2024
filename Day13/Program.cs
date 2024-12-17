using AoCTools;
using System.Text.RegularExpressions;

const string inputFile = @"../../../../input13.txt";

Console.WriteLine("Day 13 - Claw Contraption");
Console.WriteLine("Star 1");
Console.WriteLine();

//string[] lines = File.ReadAllLines(inputFile);
List<string> chunks = File.ReadAllText(inputFile).Split("\r\n\r\n").ToList();

List<ClawMachine> machines = chunks.Select(x=>new ClawMachine(x)).ToList();

long value = machines.Select(x=>x.GetCost1()).Sum();

Console.WriteLine($"The answer is: {value}");

Console.WriteLine();
Console.WriteLine("Star 2");
Console.WriteLine();

long value2 = machines.Select(x => x.GetCost2()).Sum(); ;

Console.WriteLine($"The answer is: {value2}");

Console.WriteLine();
Console.ReadKey();


class ClawMachine
{
    public LongPoint2D ButtonA { get; }
    public LongPoint2D ButtonB { get; }
    public LongPoint2D Prize { get; }

    public ClawMachine(string inputChunk)
    {
        string[] chunks = inputChunk.Split(new []{ '\n',  '+', ',', '=' });

        ButtonA = (int.Parse(chunks[1]), int.Parse(chunks[3]));
        ButtonB = (int.Parse(chunks[5]), int.Parse(chunks[7]));
        Prize = (int.Parse(chunks[9]), int.Parse(chunks[11]));
    }

    public long GetCost1()
    {
        long determinantDenom = ButtonA.x * ButtonB.y - ButtonA.y * ButtonB.x;

        long aPresses = (ButtonB.y * Prize.x - ButtonB.x * Prize.y) / determinantDenom;
        long bPresses = (-ButtonA.y * Prize.x + ButtonA.x * Prize.y) / determinantDenom;

        if (aPresses * ButtonA + bPresses * ButtonB != Prize)
        {
            return 0;
        }

        return 3 * aPresses + bPresses;
    }

    public long GetCost2()
    {
        LongPoint2D NewPrize = Prize + (10000000000000, 10000000000000);
        long determinantDenom = ButtonA.x * ButtonB.y - ButtonA.y * ButtonB.x;

        long aPresses = (ButtonB.y * NewPrize.x - ButtonB.x * NewPrize.y) / determinantDenom;
        long bPresses = (-ButtonA.y * NewPrize.x + ButtonA.x * NewPrize.y) / determinantDenom;

        if (aPresses * ButtonA + bPresses * ButtonB != NewPrize)
        {
            return 0;
        }

        return 3 * aPresses + bPresses;
    }
}
