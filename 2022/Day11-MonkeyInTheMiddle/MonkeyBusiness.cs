namespace Day11_MonkeyInTheMiddle;

internal class MonkeyBusiness
{
    public Queue<long> StartingItems { get; set; } = new Queue<long>();

    public string Operation { get; set; } = string.Empty;

    public int DivisibleBy { get; set; }

    public Dictionary<bool, int> ThrowObjectTo { get; set; } = new Dictionary<bool, int>();
}
