using System.Numerics;

namespace Day08_TreetopTreeHouse;

public class Utility
{
    public static List<Vector2> Get2DDirections()
    {
        return new List<Vector2>
        {
            new Vector2(-1, 0), // TOP
            new Vector2(0, 1),  // RIGHT
            new Vector2(1, 0),  // BOTTOM
            new Vector2(0, -1)  // LEFT
        };
    }
}
