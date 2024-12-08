var levelsLines = File.ReadAllLines("input.txt");

void PartOne()
{
    var safeCount = 0;
    foreach (var level in levelsLines)
    {
        var levelList = level.Split(" ").Select(int.Parse).ToList();
        
        safeCount += IsReportSafe(levelList) ? 1 : 0;
    }
    
    Console.WriteLine(safeCount);
}

void PartTwo()
{
    var safeCount = 0;

    foreach (var level in levelsLines)
    {
        var levelList = level.Split(" ").Select(int.Parse).ToList();
         
        var isReportSafe = IsReportSafe(levelList);

        if (!isReportSafe)
        {
            var reportsWithBadLevelRemoved = GetReportWithBadLevelRemoved(levelList);

            foreach (var report in reportsWithBadLevelRemoved)
            {
                isReportSafe = IsReportSafe(report);

                if (isReportSafe)
                {
                    safeCount++;
                    break;
                }
            }
        }
        else
        {
            safeCount++;
        }
    }
    
    Console.WriteLine(safeCount);
}

bool IsReportSafe(List<int> reportList)
{
    var difference = reportList[1] - reportList[0];

    var isIncreasing = false;
    switch (difference)
    {
        case > 0:
            isIncreasing = true;
            break;
        case < 0:
            isIncreasing = false;
            break;
        default:
            return false;
    }
    
    for (var i = 1; i < reportList.Count; i++)
    {
        difference = reportList[i] - reportList[i - 1];
        if (isIncreasing)
        {
            if (difference is > 3 or <= 0)
            {
                return false;
            }
        }
        else
        {
            if (difference is < -3 or >= 0)
            {
                return false;
            }
        }
    }

    return true;
}

List<List<int>> GetReportWithBadLevelRemoved(List<int> reportList)
{
    var isIncreasing = false;
    var difference = reportList[1] - reportList[0];
    
    switch (difference)
    {
        case > 0:
            isIncreasing = true;
            break;
        case < 0:
            isIncreasing = false;
            break;
        default:
            return [reportList.Slice(1, reportList.Count - 1).ToList()];
    }

    var foundBadLevel = false;
    var returnList = new List<List<int>>();
    for (var i = 1; i < reportList.Count; i++)
    {
        difference = reportList[i] - reportList[i - 1];
        if (isIncreasing)
        {
            if (difference is > 3 or <= 0)
            {
                foundBadLevel = true;
            }
        }
        else
        {
            if (difference is < -3 or >= 0)
            {
                foundBadLevel = true;
            }
        }

        if (foundBadLevel)
        {
            var firstList = reportList.Slice(0, i).ToList();
            var secondList = reportList.Slice(i + 1, reportList.Count - i - 1).ToList();
                
            returnList.Add(firstList.Concat(secondList).ToList());
                
            firstList = reportList.Slice(0, i - 1).ToList();
            secondList = reportList.Slice(i, reportList.Count - i).ToList();
                
            returnList.Add(firstList.Concat(secondList).ToList());
        }
    }
    
    return returnList;
}

PartOne();
PartTwo();