var registerInstructions = File.ReadLines("input.txt");

void RunPartOne()
{
    var xValue = 1;
    var currentCycle = 0;

    var cycleArray = new int[]
    {
        20, 60, 100, 140, 180, 220
    };

    var signalStrengthList = new List<int>();

    foreach (var registerInstruction in registerInstructions)
    {
        var instructionArray = registerInstruction.Split(" ");

        switch (instructionArray[0])
        {
            case "noop":
                currentCycle++;
                if (cycleArray.Contains(currentCycle))
                {
                    signalStrengthList.Add(xValue * currentCycle);
                }
                break;
            case "addx":
                for (int i = 0; i < 2; i++)
                {
                    currentCycle++;
                    if (cycleArray.Contains(currentCycle))
                    {
                        signalStrengthList.Add(xValue * currentCycle);
                    }
                }

                xValue += int.Parse(instructionArray[1]);
                break;
        }
    }

    Console.WriteLine(signalStrengthList.Sum());
}

RunPartOne();

void RunPartTwo()
{
    var registerX = 1;
    var spritePosition = registerX - 1;
    var currentCycle = 0;

    var crtScreenArray = new string[240];

    foreach (var registerInstruction in registerInstructions)
    {
        var instructionArray = registerInstruction.Split(" ");


        var cyclePosition = currentCycle % 40;

        switch (instructionArray[0])
        {
            case "noop":
                if (cyclePosition >= spritePosition && cyclePosition <= spritePosition + 2)
                {
                    crtScreenArray[currentCycle] = "#";
                }
                else
                {
                    crtScreenArray[currentCycle] = ".";
                }
                currentCycle++;
                break;
            case "addx":
                for (int i = 0; i < 2; i++)
                {
                    if (cyclePosition >= spritePosition && cyclePosition <= spritePosition + 2)
                    {
                        crtScreenArray[currentCycle] = "#";
                    }
                    else
                    {
                        crtScreenArray[currentCycle] = ".";
                    }
                    currentCycle++;
                    cyclePosition = currentCycle % 40;
                }

                registerX += int.Parse(instructionArray[1]);
                spritePosition = registerX - 1;
                break;
        }
    }

    for (int i = 0; i < crtScreenArray.Length; i += 40)
    {
        for (int j = i; j < i + 40; j++)
        {
            Console.Write(crtScreenArray[j]);
        }
        Console.WriteLine();
    }
}

RunPartTwo();