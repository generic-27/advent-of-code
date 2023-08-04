
int GetTotalCharactersProcessedToGetNDistinctPackets(int n, string dataStream)
{
    var charList = new List<char>();
    int totalCharacterProcessed = 0;

    for (int i = 0; i < dataStream.Length; i++)
    {
        if (charList.Count() == n)
        {
            totalCharacterProcessed = i;
            break;
        }

        int index = charList.IndexOf(dataStream[i]);
        if (index != -1)
        {
            for (int j = 0; j <= index; j++)
            {
                charList.RemoveAt(0);
            }
        }

        charList.Add(dataStream[i]);
    }

    return totalCharacterProcessed;
}

void RunPartOne()
{
    var dataStream = File.ReadAllText("input.txt");

    var totalCharacters = GetTotalCharactersProcessedToGetNDistinctPackets(4, dataStream);

    Console.WriteLine(totalCharacters);
}

void RunPartTwo()
{
    var dataStream = File.ReadAllText("input.txt");

    var totalCharacters = GetTotalCharactersProcessedToGetNDistinctPackets(14, dataStream);

    Console.WriteLine(totalCharacters);
}

RunPartOne();
RunPartTwo();