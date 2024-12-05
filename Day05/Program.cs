const string inputFile = @"../../../../input05.txt";

Console.WriteLine("Day 05 - Print Queue");
Console.WriteLine("Star 1");
Console.WriteLine();

string[] sections = File.ReadAllText(inputFile).Split("\r\n\r\n");

string[] constraints = sections[0].Split("\r\n");
string[] orders = sections[1].Split("\r\n");

Dictionary<int, List<int>> constraintDict = new Dictionary<int, List<int>>();

foreach (string constraint in constraints)
{
    var splitString = constraint.Split('|');
    int dependentPage = int.Parse(splitString[1]);
    int requiredPage = int.Parse(splitString[0]);

    if (!constraintDict.ContainsKey(dependentPage))
    {
        constraintDict.Add(dependentPage, new List<int>());
    }

    constraintDict[dependentPage].Add(requiredPage);
}

IEnumerable<List<int>> parsedOrders = orders.Select(x => x.Split(',').Select(int.Parse).ToList());


int value = parsedOrders.Where(x => IsCorrect(x, constraintDict)).Select(GetMiddle).Sum();

Console.WriteLine($"The answer is: {value}");

Console.WriteLine();
Console.WriteLine("Star 2");
Console.WriteLine();


var wrongOrders = parsedOrders.Where(x => !IsCorrect(x, constraintDict)).Select(x => Reorder(x, constraintDict)).Select(GetMiddle).Sum();

Console.WriteLine($"The answer is: {wrongOrders}");

Console.WriteLine();
Console.ReadKey();

//Judge if a page ordering is consistent with rules
bool IsCorrect(List<int> order, Dictionary<int, List<int>> constraintDict)
{
    List<int> handledPages = new List<int>();

    for (int i = 0; i < order.Count; i++)
    {
        if (constraintDict.ContainsKey(order[i]))
        {
            foreach (int page in constraintDict[order[i]])
            {
                if (!handledPages.Contains(page) && order.Contains(page))
                {
                    return false;
                }
            }
        }

        handledPages.Add(order[i]);
    }

    return true;
}

List<int> Reorder(List<int> order, Dictionary<int, List<int>> constraintDict)
{
    List<int> returnList = new List<int>(order);

    foreach (var kvp in constraintDict)
    {
        foreach (int value in kvp.Value)
        {
            if (returnList.Contains(kvp.Key) && returnList.Contains(value))
            {
                int keyIndex = returnList.IndexOf(kvp.Key);
                int valueIndex = returnList.IndexOf(value);

                if (keyIndex > valueIndex)
                {
                    returnList.RemoveAt(keyIndex);
                    returnList.Insert(valueIndex, kvp.Key);
                }
            }
        }
    }

    return returnList;
}


int GetMiddle(List<int> order)
{
    return order[order.Count / 2];
}