namespace Day04_Scratchcards;

internal class Scratchcard
{
    public HashSet<int> winningNumbers;

    public List<int> scratchCardNumbers;

    public List<Scratchcard> scratchCardsWon;

    public Scratchcard()
    {
        scratchCardsWon = new List<Scratchcard>();

        winningNumbers = new HashSet<int>();

        scratchCardNumbers = new List<int>();
    }
}
