namespace Day12_HillClimbingAlgorithm;

public static class Utility
{
    public static List<(int, int)> GetDirections()
    {
        return new List<(int, int)>
        {
            (0, -1),
            (0, 1),
            (-1, 0),
            (1, 0),
        };
    }
}
