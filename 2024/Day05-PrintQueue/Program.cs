var updateData = File.ReadAllText("input.txt");
PartOne();
PartTwo();
return;


void PartOne()
{
    var pageInformationArray = updateData.Split("\n\n");

    var partsDictionary = BuildPartsDictionary(pageInformationArray[0]);

    var updatesArray = pageInformationArray[1].Split("\n");
    
    var updatesList = GetUpdates(updatesArray, partsDictionary, true);

    var sum = updatesList.Sum(updateList => updateList[updateList.Count / 2]);

    Console.WriteLine(sum);
}

void PartTwo()
{
    var pageInformationArray = updateData.Split("\n\n");

    var partsDictionary = BuildPartsDictionary(pageInformationArray[0]);

    var updatesArray = pageInformationArray[1].Split("\n");
    
    var invalidUpdatesList = GetUpdates(updatesArray, partsDictionary, false);
    
    var sum = 0;
    foreach (var list in invalidUpdatesList)
    {
        for (var j = 1; j < list.Count; j++)
        {
            var currentIndex = j;
            while (currentIndex > 0)
            {
                if (partsDictionary[list[currentIndex]].Contains(list[currentIndex - 1]))
                {
                    (list[currentIndex - 1], list[currentIndex]) = (list[currentIndex], list[currentIndex - 1]);
                }
                currentIndex--;
            }
        }

        sum += list[list.Count / 2];
    }
    
    Console.WriteLine(sum);
}

Dictionary<int, HashSet<int>> BuildPartsDictionary(string pageRulesData)
{
    var pageRules = pageRulesData.Split("\n");
    
    var partDictionary = new Dictionary<int, HashSet<int>>();
    foreach (var rule in pageRules)
    {
        var parts = rule.Split("|").Select(int.Parse).ToList();

        if (!partDictionary.ContainsKey(parts.First()))
        {
            var hashset = new HashSet<int> { parts[1] };
            partDictionary.Add(parts[0], hashset);

            if (!partDictionary.ContainsKey(parts[1]))
            {
                partDictionary.Add(parts[1], []);
            }
        }
        else
        {
            partDictionary[parts[0]].Add(parts[1]);
        }
    }
    
    return partDictionary;
}

List<List<int>> GetUpdates(string[] updatesArray, Dictionary<int, HashSet<int>> partsDictionary, bool AllValidUpdates)
{
    var updates = new List<List<int>>();
    foreach (var update in updatesArray)
    {
        var updateValues = update.Split(",").Select(int.Parse).ToList();
        var success = true;
        for (var i = 0; i < updateValues.Count() - 1; i++)
        {
            if (!partsDictionary.ContainsKey(updateValues[i]))
            {
                if (i < updateValues.Count() - 1)
                {
                    success = false;
                    break;
                }
                continue;
            }
        
            var hashSet = partsDictionary[updateValues[i]];
            if (!hashSet.Contains(updateValues[i + 1]))
            {
                success = false;
                break;
            }
        }

        if (AllValidUpdates && success)
        {
            updates.Add(updateValues);
        }
        else if(!AllValidUpdates && !success)
        {
            updates.Add(updateValues);
        }
    }
    
    return updates;
}