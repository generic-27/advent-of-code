using Day04_Scratchcards;

var scratchcardsData = File.ReadAllLines("input.txt");

void PartOne()
{
    int total = 0;

    foreach (var scratchCardInfo in scratchcardsData)
    {
        var cardInfo = scratchCardInfo.Split(":")[1];

        var cardNumbersStringArray = cardInfo.Split("|").Select(c => c.Trim()).ToList();

        var winningNumbers = cardNumbersStringArray[0].Split(" ", StringSplitOptions.RemoveEmptyEntries)
                                                      .Select(c => int.Parse(c))
                                                      .ToHashSet();

        var cardNumbers = cardNumbersStringArray[1].Split(" ", StringSplitOptions.RemoveEmptyEntries)
                                                   .Select(c => int.Parse(c))
                                                   .ToList();

        var current = 0;
        foreach (var number in cardNumbers)
        {
            if (winningNumbers.Contains(number))
            {
                current = current == 0 ? 1 : current * 2;
            }
        }

        total += current;
    }

    Console.WriteLine(total);
}

//PartOne();

void PartTwo()
{
    var scratchCardDictionary = new Dictionary<int, Scratchcard>();

    foreach (var scratchCardInfo in scratchcardsData)
    {
        var cardId = int.Parse(scratchCardInfo.Split(":")[0].Split(" ", StringSplitOptions.RemoveEmptyEntries)[1]);

        var cardInfo = scratchCardInfo.Split(":")[1];

        var cardNumbersStringArray = cardInfo.Split("|").Select(c => c.Trim()).ToList();

        var winningNumbers = cardNumbersStringArray[0].Split(" ", StringSplitOptions.RemoveEmptyEntries)
                                                      .Select(c => int.Parse(c))
                                                      .ToHashSet();

        var cardNumbers = cardNumbersStringArray[1].Split(" ", StringSplitOptions.RemoveEmptyEntries)
                                                   .Select(c => int.Parse(c))
                                                   .ToList();

        var scratchCard = new Scratchcard();

        scratchCard.winningNumbers = new HashSet<int>(winningNumbers);
        scratchCard.scratchCardNumbers = cardNumbers;

        scratchCardDictionary.Add(cardId, scratchCard);
    }


    var winningCountDictionary = new Dictionary<int, List<int>>();

    foreach (var kvp in scratchCardDictionary)
    {
        var scratchCard = kvp.Value;

        var currentId = kvp.Key + 1;

        var winningCountList = new List<int>();

        foreach (var number in scratchCard.scratchCardNumbers)
        {
            if (scratchCard.winningNumbers.Contains(number))
            {
                winningCountList.Add(currentId);

                currentId++;
            }
        }

        winningCountDictionary.Add(kvp.Key, winningCountList);
    }

    int total = 0;

    var queue = new Queue<int>(winningCountDictionary.Keys);

    while (queue.Count > 0)
    {
        var number = queue.Dequeue();

        total += winningCountDictionary[number].Count;

        foreach (var value in winningCountDictionary[number])
        {
            queue.Enqueue(value);
        }
    }

    Console.WriteLine(total + winningCountDictionary.Count);

}

PartTwo();