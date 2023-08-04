var cratesInformation = File.ReadAllText("input.txt");

const int CRATE_ORDER_DISTANCE = 4;
var cratesList = new List<Stack<char>>();


void BuildCratesList(string cratesArrangement)
{
    cratesList = new List<Stack<char>>();

    var cratesOrder = cratesArrangement.Split("\r\n");

    for (int i = cratesOrder.Length - 2; i >= 0; i--)
    {
        int crateIndex = 0;

        for (int j = 1; j < cratesOrder[i].Length; j += CRATE_ORDER_DISTANCE)
        {
            if (cratesList.Count() <= crateIndex)
            {
                cratesList.Add(new Stack<char>());
            }

            if (cratesOrder[i][j] != ' ')
            {
                cratesList[crateIndex].Push(cratesOrder[i][j]);
            }

            crateIndex++;
        }
    }
}

void RunPartOne()
{
    var cratesData = cratesInformation.Split("\r\n\r\n");

    var cratesArrangement = cratesData[0];
    var cratesCommands = cratesData[1];

    BuildCratesList(cratesArrangement);

    var commandArray = cratesCommands.Split("\n");

    foreach (var command in commandArray)
    {
        var commandParts = command.Split(" ");

        var quantityToMove = int.Parse(commandParts[1]);
        var moveFrom = int.Parse(commandParts[3]) - 1;
        var moveTo = int.Parse(commandParts[5]) - 1;

        for (int i = 0; i < quantityToMove; i++)
        {
            var value = cratesList[moveFrom].Peek();
            cratesList[moveFrom].Pop();
            cratesList[moveTo].Push(value);
        }
    }

    for (int i = 0; i < cratesList.Count(); i++)
    {
        if (cratesList[i].Count() > 0)
        {
            Console.Write(cratesList[i].Peek());
        }
    }
}

void RunPartTwo()
{
    var cratesData = cratesInformation.Split("\r\n\r\n");

    var cratesArrangement = cratesData[0];
    var cratesCommands = cratesData[1];

    BuildCratesList(cratesArrangement);

    var commandArray = cratesCommands.Split("\n");

    foreach (var command in commandArray)
    {
        var commandParts = command.Split(" ");

        var quantityToMove = int.Parse(commandParts[1]);
        var moveFrom = int.Parse(commandParts[3]) - 1;
        var moveTo = int.Parse(commandParts[5]) - 1;


        var stack = new Stack<char>();

        for (int i = 0; i < quantityToMove; i++)
        {
            stack.Push(cratesList[moveFrom].Pop());
        }

        while (stack.Count() > 0)
        {
            cratesList[moveTo].Push(stack.Pop());
        }
    }

    for (int i = 0; i < cratesList.Count(); i++)
    {
        if (cratesList[i].Count() > 0)
        {
            Console.Write(cratesList[i].Peek());
        }
    }
}

RunPartOne();
Console.WriteLine();
RunPartTwo();
