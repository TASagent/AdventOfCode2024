using AoCTools;

const string inputFile = @"../../../../input14.txt";

Console.WriteLine("Day 14 - Restroom Redoubt");
Console.WriteLine("Star 1");
Console.WriteLine();

string[] lines = File.ReadAllLines(inputFile);
//string line = File.ReadAllText(inputFile);

int value = 0;

Console.WriteLine($"The answer is: {value}");

Console.WriteLine();
Console.WriteLine("Star 2");
Console.WriteLine();

int value2 = 0;

Console.WriteLine($"The answer is: {value2}");

Console.WriteLine();
Console.ReadKey();


class Robot
{
    Point2D Position { get; }
    Point2D Velocity { get; }

    public Robot(string line)
    {
        string[] splitLine = line.Split(new[] { ',', '=', ' ' });

        Position = new Point2D(int.Parse(splitLine[1]),  int.Parse(splitLine[2]));
        Velocity = new Point2D(int.Parse(splitLine[4]),  int.Parse(splitLine[5]));
    }

    Point2D GetPositionAfter(int time)
    {

    }
}