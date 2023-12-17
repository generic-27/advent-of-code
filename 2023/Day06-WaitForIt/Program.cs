var racesInformation = File.ReadAllText("input.txt");

void PartOne()
{
    var timeandDistanceInfo = racesInformation.Split("\r\n");

    var timeList = timeandDistanceInfo[0].Split(":")[1]
                                         .Trim()
                                         .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                                         .Select(c => long.Parse(c))
                                         .ToList();

    var distanceList = timeandDistanceInfo[1].Split(":")[1]
                                             .Trim()
                                             .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                                             .Select(c => long.Parse(c))
                                             .ToList();



    var numberOfWaysList = GetNumberOfWaysList(timeList, distanceList);

    var total = numberOfWaysList.Aggregate((v1, v2) => v1 * v2);

    Console.WriteLine(total);
}

void PartTwo()
{
    var timeandDistanceInfo = racesInformation.Split("\r\n");

    var time = long.Parse(timeandDistanceInfo[0].Split(":")[1]
                                         .Trim()
                                         .Replace(" ", ""));

    var distance = long.Parse(timeandDistanceInfo[1].Split(":")[1]
                                         .Trim()
                                         .Replace(" ", ""));

    var numberOfWaysList = GetNumberOfWaysList(new List<long> { time }, new List<long> { distance });

    Console.WriteLine(numberOfWaysList[0]);
}

List<long> GetNumberOfWaysList(List<long> timeList, List<long> distanceList)
{
    var numberOfWaysList = new List<long>();

    for (int i = 0; i < distanceList.Count; i++)
    {
        var time = timeList[i];
        var distanceRecord = distanceList[i];

        int numberOfWays = 0;
        for (int j = 1; j < time; j++)
        {
            var remainingTime = time - j;

            var distancePossible = remainingTime * j;
            if (distancePossible > distanceRecord)
            {
                numberOfWays++;
            }
        }

        numberOfWaysList.Add(numberOfWays);
    }

    return numberOfWaysList;
}

PartOne();
PartTwo();