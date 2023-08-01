
List<int> GetSortedElfCalorieList(string[] calorieArray)
{
    var list = new List<int>();

    int currentCalorieCount = 0;

    foreach (var calorie in calorieArray)
    {
        if (calorie == "")
        {
            list.Add(currentCalorieCount);
            currentCalorieCount = 0;
            continue;
        }

        currentCalorieCount += int.Parse(calorie);
    }

    list.Sort();
    return list;
}

int GetTotalCaloriesOfTopN(int topN, List<int> calorieList)
{
    if (topN > calorieList.Count)
    {
        throw new ArgumentOutOfRangeException($"The TopN value is out of range, Calorie List count is {calorieList.Count}");
    }

    int total = 0;
    int calorieListCount = calorieList.Count;

    for (int i = calorieListCount - 1; i > calorieListCount - topN - 1; i--)
    {
        total += calorieList[i];
    }


    return total;
}

var calorieData = File.ReadAllLines("input.txt");

var elfCalorieList = GetSortedElfCalorieList(calorieData);

Console.WriteLine(GetTotalCaloriesOfTopN(1, elfCalorieList));
Console.WriteLine(GetTotalCaloriesOfTopN(3, elfCalorieList));

// The version below uses functional programming to achieve the same result

int GetTotalCaloriesOfTopNFunctional(int topN)
{
    return File.ReadAllText("input.txt")
                   .Split(new[] { "\r\n\r\n", "\n\n" }, StringSplitOptions.RemoveEmptyEntries)
                   .Select(block => block.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries)
                                         .Select(int.Parse)
                                         .Sum())
                   .OrderByDescending(sum => sum)
                   .Take(topN)
                   .Sum();
}


Console.WriteLine(GetTotalCaloriesOfTopNFunctional(1));