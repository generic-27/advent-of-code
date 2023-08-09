using System.Numerics;

namespace Day09_RopeBridge;

public class Utility
{
    public static Dictionary<string, Vector2> DirectionDictionary = new Dictionary<string, Vector2>
    {
        { "R", new Vector2(0, 1) },
        { "L", new Vector2(0, -1) },
        { "U", new Vector2(-1, 0) },
        { "D", new Vector2(1, 0) },
    };

    public static double DiagonalDistance = Math.Sqrt(Math.Pow(1, 2) + Math.Pow(1, 2));
}
