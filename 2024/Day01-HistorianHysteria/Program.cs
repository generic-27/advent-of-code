var locationIdsData = File.ReadAllLines("input.txt");

void PartOne()
{
    var leftList = new List<int>();
    var rightList = new List<int>();

    foreach (var line in locationIdsData)
    {
        var values = line.Split("  ");
        leftList.Add(int.Parse(values[0]));
        rightList.Add(int.Parse(values[1]));
    }
    
    leftList.Sort();
    rightList.Sort();

    long total = 0;
    for (int i = 0; i < leftList.Count; i++)
    {
        var difference = Math.Abs(leftList[i] - rightList[i]);
        total += difference;
    }
    
    Console.WriteLine(total);
}

void PartTwo()
{
    var leftList = new List<int>();
    var rightListDictionary = new Dictionary<int, long>();

    foreach (var line in locationIdsData)
    {
        var values = line.Split("  ");
        leftList.Add(int.Parse(values[0]));

        if (rightListDictionary.ContainsKey(int.Parse(values[1])))
        {
            rightListDictionary[int.Parse(values[1])]++;
        }
        else
        {
            rightListDictionary.Add(int.Parse(values[1]), 1);
        }
    }

    long total = 0;
    foreach (var value in leftList)
    {
        if (rightListDictionary.ContainsKey(value))
        {
            var similarityScore = value * rightListDictionary[value];
            total += similarityScore;
        }
    }
    
    Console.WriteLine(total);
}

PartOne();
PartTwo();