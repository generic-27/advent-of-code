var rucksacks = File.ReadAllLines("input.txt");

const int LOWERCASE_A_ASCII = (int)'a';
const int LOWERCASE_Z_ASCII = (int)'z';
const int UPPERCASE_A_ASCII = (int)'A';
const int LOWERCASE_A_PRIORITY = 1;
const int UPPERCASE_A_PRIORITY = 27;

int GetPriorityByComparingRucksacks(string[] rucksacks)
{
    var rucksackHashSet = new HashSet<char>();

    foreach (var character in rucksacks[0])
    {
        rucksackHashSet.Add(character);
    }

    int priority = 0;

    for (int i = 1; i < rucksacks.Length; i++)
    {
        foreach (var character in rucksacks[i])
        {
            if (rucksackHashSet.Contains(character))
            {
                hashSet.Add(character);
            }
        }

        rucksackHashSet.Clear();
        rucksackHashSet = hashSet;
    }

    var characterAscii = rucksackHashSet.First();

    if (characterAscii >= LOWERCASE_A_ASCII && characterAscii <= LOWERCASE_Z_ASCII)
    {
        var differenceFromStart = characterAscii - LOWERCASE_A_ASCII;

        priority = differenceFromStart + LOWERCASE_A_PRIORITY;
    }
    else
    {
        var differenceFromStart = characterAscii - UPPERCASE_A_ASCII;
        priority = differenceFromStart + UPPERCASE_A_PRIORITY;
    }

    return priority;
}

void RunPartOne()
{
    int totalPriority = 0;

    foreach (var rucksack in rucksacks)
    {
        var rucksackLength = rucksack.Length;
        var firstCompartment = rucksack.Substring(0, rucksackLength / 2);
        var secondCompartment = rucksack.Substring(rucksackLength / 2);

        string[] rucksackArray = new string[]
        {
            firstCompartment,
            secondCompartment
        };

        var priority = GetPriorityByComparingRucksacks(rucksackArray);

        totalPriority += priority;
    }

    Console.WriteLine(totalPriority);
}

void RunPartTwo()
{
    int totalPriority = 0;

    for (int i = 0; i < rucksacks.Length; i += 3)
    {
        string[] rucksacksArray = new string[]
        {
            rucksacks[i],
            rucksacks[i + 1],
            rucksacks[i + 2],
        };

        var priority = GetPriorityByComparingRucksacks(rucksacksArray);

        totalPriority += priority;
    }

    Console.WriteLine(totalPriority);
}

RunPartOne();
RunPartTwo();
