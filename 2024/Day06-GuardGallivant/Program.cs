
using Day06_GuardGallivant;

PartOne();
PartTwo();
return;


void PartOne()
{
    var grid = File.ReadAllLines("input.txt");

    var (guardRow, guardColumn) = GetGuardRowAndColumn(grid);
    
    var (direction, movement) = GetNextDirection(Direction.UNKNOWN);

    var positionHashset = new HashSet<(int, int)>();
    while (guardRow + movement[0] >= 0 && guardRow + movement[0] < grid.Length && 
           guardColumn + movement[1] >= 0 && guardColumn + movement[1] < grid[0].Length)
    {
        positionHashset.Add((guardRow, guardColumn));
        if (grid[guardRow + movement[0]][guardColumn + movement[1]] == '#')
        {
            (direction, movement) = GetNextDirection(direction);
        }

        guardRow += movement[0];
        guardColumn += movement[1];
    }
    
    Console.WriteLine(positionHashset.Count + 1);
}

void PartTwo()
{
    var grid = File.ReadAllLines("input.txt");
    
    var (guardRow, guardColumn) = GetGuardRowAndColumn(grid);
    var (direction, movement) = GetNextDirection(Direction.UNKNOWN);

    var movementDictionary = new Dictionary<(int, int), Direction>();
    while (guardRow + movement[0] >= 0 && guardRow + movement[0] < grid.Length && 
           guardColumn + movement[1] >= 0 && guardColumn + movement[1] < grid[0].Length)
    { ;
        if (grid[guardRow + movement[0]][guardColumn + movement[1]] == '#')
        {
            (direction, movement) = GetNextDirection(direction);
        }

        guardRow += movement[0];
        guardColumn += movement[1];
    }
}

(int, int) GetGuardRowAndColumn(string[] grid)
{
    for (var row = 0; row < grid.Length; row++)
    {
        for (var column = 0; column < grid[row].Length; column++)
        {
            if (grid[row][column] == '^')
            {
                return (row, column);
            }
        }
    }

    return (-1, -1);
}

(Direction,int[]) GetNextDirection(Direction direction)
{
    return direction switch
    {
        Direction.UP => (Direction.RIGHT, [0, 1]),
        Direction.DOWN => (Direction.LEFT, [0, -1]),
        Direction.LEFT => (Direction.UP, [-1, 0]),
        Direction.RIGHT => (Direction.DOWN, [1, 0]),
        Direction.UNKNOWN => (Direction.UP, [-1,0]),
        _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
    };
}