var datatStream = File.ReadAllText("input.txt");

int GetTotalCharactersProcessedToGetNDistinctPackets(int n)
{
    var charList = new List<char>();
    int totalCharacterProcessed = 0;

    for (int i = 0; i < datatStream.Length; i++)
    {
        if (charList.Count() == n)
        {
            totalCharacterProcessed = i;
            break;
        }

        int index = charList.IndexOf(datatStream[i]);
        if (index != -1)
        {
            for (int j = 0; j <= index; j++)
            {
                charList.RemoveAt(0);
            }
        }

        charList.Add(datatStream[i]);
    }

    return totalCharacterProcessed;
}

void RunPartOne()
{
    var totalCharacters = GetTotalCharactersProcessedToGetNDistinctPackets(4);

    Console.WriteLine(totalCharacters);
}

void RunPartTwo()
{
    var totalCharacters = GetTotalCharactersProcessedToGetNDistinctPackets(14);

    Console.WriteLine(totalCharacters);
}

RunPartOne();
RunPartTwo();