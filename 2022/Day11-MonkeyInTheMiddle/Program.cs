using Day11_MonkeyInTheMiddle;

List<MonkeyBusiness> BuildMonkeyBusinessList()
{
    var monkeyBusinessData = File.ReadAllText("input.txt");

    var monkeysInformation = monkeyBusinessData.Split("\r\n\r\n");

    var monkeyBusinessList = new List<MonkeyBusiness>();

    foreach (var monkeyBusinessInfo in monkeysInformation)
    {
        var monkeyBusinessArray = monkeyBusinessInfo.Split("\n").Select(s => s.Trim()).ToArray();

        var startingItemsArray = monkeyBusinessArray[1].Split(":")[1]
                                                           .Trim()
                                                           .Split(",")
                                                           .Select(num => long.Parse(num.Trim()))
                                                           .ToArray();

        var operation = monkeyBusinessArray[2].Split(" = ")[1];

        var divisibleBy = int.Parse(monkeyBusinessArray[3].Split("divisible by")[1].Trim());

        var ifTrueThrowTo = int.Parse(monkeyBusinessArray[4].Split("throw to monkey")[1].Trim());
        var ifFalseThrowTo = int.Parse(monkeyBusinessArray[5].Split("throw to monkey")[1].Trim());

        var monkeyBusiness = new MonkeyBusiness
        {
            StartingItems = new Queue<long>(startingItemsArray),
            Operation = operation,
            DivisibleBy = divisibleBy,
            ThrowObjectTo = new Dictionary<bool, int>
            {
                { true, ifTrueThrowTo },
                { false, ifFalseThrowTo }
            }
        };

        monkeyBusinessList.Add(monkeyBusiness);
    }

    return monkeyBusinessList;
}

long PerformOperation(string operation, long itemWorryLevel, long number)
{
    var operationArray = operation.Split(" ");

    long value = 0;

    switch (operationArray[1])
    {
        case "*":
            value = itemWorryLevel * number;
            break;
        case "/":
            value = itemWorryLevel / number;
            break;
        case "+":
            value = itemWorryLevel + number;
            break;
        case "-":
            value = itemWorryLevel - number;
            break;
    }

    return value;
}

Dictionary<int, long> BuildMonkeyInspectionDictionary(List<MonkeyBusiness> monkeyBusinessList, int rounds, bool reduceUsingModulus)
{
    var monkeyInspectionDictionary = new Dictionary<int, long>();

    char[] delimiters = new char[] { '*', '-', '+', '/' };

    for (int i = 0; i < monkeyBusinessList.Count(); i++)
    {
        monkeyInspectionDictionary.Add(i, 0);
    }

    var mod = 1;

    foreach (var monkeyBusinessInfo in monkeyBusinessList)
    {
        mod *= monkeyBusinessInfo.DivisibleBy;
    }


    for (int i = 0; i < rounds; i++)
    {
        int index = 0;

        foreach (var monkeyBusiness in monkeyBusinessList)
        {
            while (monkeyBusiness.StartingItems.Count() > 0)
            {
                var startingItem = monkeyBusiness.StartingItems.Dequeue();

                var numberString = monkeyBusiness.Operation.Split(delimiters)[1].Trim();

                long number = 0;
                if (numberString == "old")
                {
                    number = startingItem;
                }
                else
                {
                    number = long.Parse(numberString);
                }

                var worryLevel = PerformOperation(monkeyBusiness.Operation, startingItem, number);

                var monkeyBoredWorryLevel = reduceUsingModulus ? worryLevel % mod : worryLevel / 3;

                var isDivisible = monkeyBoredWorryLevel % monkeyBusiness.DivisibleBy == 0;

                var monkeyToThrowTo = monkeyBusiness.ThrowObjectTo[isDivisible];
                monkeyBusinessList[monkeyToThrowTo].StartingItems.Enqueue(monkeyBoredWorryLevel);

                monkeyInspectionDictionary[index]++;
            }

            index++;
        }
    }

    return monkeyInspectionDictionary;
}

void RunPartOne()
{
    var monkeyBusinessList = BuildMonkeyBusinessList();


    var monkeyInspectionDictionary = BuildMonkeyInspectionDictionary(monkeyBusinessList, 20, false);


    var activeMonkeys = monkeyInspectionDictionary.Values.ToList();
    activeMonkeys.Sort();

    Console.WriteLine(activeMonkeys[activeMonkeys.Count() - 1] * activeMonkeys[activeMonkeys.Count() - 2]);
}

RunPartOne();

void RunPartTwo()
{
    var monkeyBusinessList = BuildMonkeyBusinessList();

    var monkeyInspectionDictionary = BuildMonkeyInspectionDictionary(monkeyBusinessList, 10000, true);


    var activeMonkeys = monkeyInspectionDictionary.Values.ToList();
    activeMonkeys.Sort();

    Console.WriteLine(activeMonkeys[activeMonkeys.Count() - 1] * activeMonkeys[activeMonkeys.Count() - 2]);
}

RunPartTwo();