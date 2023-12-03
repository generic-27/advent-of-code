using System.Text.RegularExpressions;

var calibrationValues = File.ReadAllLines("input.txt");

List<List<string>> BuildNumberStringList(string[] regexMatchArray)
{
    var list = new List<List<string>>();

    foreach (var calibrationValue in calibrationValues)
    {
        var sortedDictionary = new SortedDictionary<int, string>();

        foreach (var regExp in regexMatchArray)
        {
            var matches = Regex.Matches(calibrationValue, regExp);

            foreach (var match in matches.ToList())
            {
                sortedDictionary.Add(match.Index, match.Value);
            }
        }

        list.Add(sortedDictionary.Values.ToList());
    }

    return list;
}

void PartOne()
{
    var regexMatchArray = new string[]
    {
        "1", "2", "3", "4", "5", "6", "7", "8", "9"
    };

    var numberStringsList = BuildNumberStringList(regexMatchArray);

    int total = 0;

    foreach (var numberStringList in numberStringsList)
    {
        var value = 0;
        if (numberStringList.Count == 1)
        {
            value = int.Parse(numberStringList[0] + "" + numberStringList[0]);
        }
        else
        {
            var count = numberStringList.Count;

            value = int.Parse(numberStringList[0] + "" + numberStringList[count - 1]);
        }

        total += value;
    }

    Console.WriteLine(total);
}

PartOne();


void PartTwo()
{
    var regexMatchArray = new string[]
    {
        "one", "two", "three", "four", "five", "six", "seven", "eight", "nine",
        "1", "2", "3", "4", "5", "6", "7", "8", "9"
    };

    var textToNumberDictionary = new Dictionary<string, int>
    {
        {"one", 1},
        {"two", 2},
        {"three", 3},
        {"four", 4},
        {"five", 5},
        {"six", 6},
        {"seven", 7},
        {"eight", 8},
        {"nine", 9}
    };

    int oneAscii = '1';
    int nineAscii = '9';

    var numberStringsList = BuildNumberStringList(regexMatchArray);

    int total = 0;

    foreach (var numberStringList in numberStringsList)
    {
        var numberList = new List<int>();

        foreach (var numberString in numberStringList)
        {
            if (numberString[0] >= oneAscii && numberString[0] <= nineAscii)
            {
                numberList.Add(int.Parse(numberString));
            }
            else
            {
                var number = textToNumberDictionary[numberString];

                numberList.Add(number);
            }
        }

        int finalResult = 0;

        if (numberList.Count == 1)
        {
            finalResult = int.Parse(numberList[0] + "" + numberList[0]);
        }
        else
        {
            int count = numberList.Count;

            finalResult = int.Parse(numberList[0] + "" + numberList[count - 1]);
        }

        total += finalResult;
    }

    Console.WriteLine(total);
}

PartTwo();