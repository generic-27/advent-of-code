using Day12_HillClimbingAlgorithm;

var elevationArray = File.ReadAllLines("input.txt");

int numberOfRows = elevationArray.Length;
int numberOfColumns = elevationArray[0].Length;


int startingRow = 0;
int startingColumn = 0;
int endRow = 0;
int endColumn = 0;

var gridData = new char[numberOfRows, numberOfColumns];

for (int row = 0; row < elevationArray.Length; row++)
{
    for (int col = 0; col < elevationArray[0].Length; col++)
    {
        gridData[row, col] = elevationArray[row][col];

        if (elevationArray[row][col] == 'S')
        {
            startingRow = row;
            startingColumn = col;
            gridData[startingRow, startingColumn] = 'a';
        }

        if (elevationArray[row][col] == 'E')
        {
            endRow = row;
            endColumn = col;

            gridData[row, col] = 'z';
        }
    }
}

var visited = new HashSet<(int, int)>();
visited.Add((startingRow, startingColumn));

var directions = Utility.GetDirections();

var queue = new Queue<(int, int, int)>();

queue.Enqueue((startingRow, startingColumn, 0));

while (queue.Count > 0)
{
    var (parentRow, parentColumn, distance) = queue.Dequeue();

    foreach (var direction in directions)
    {
        int row = parentRow + direction.Item1;
        int column = parentColumn + direction.Item2;

        if (row < 0 || row > numberOfRows - 1 || column < 0 || column > numberOfColumns - 1)
        {
            continue;
        }

        if (visited.Contains((row, column)))
        {
            continue;
        }

        var difference = gridData[row, column] - gridData[parentRow, parentColumn];

        if (difference > 1)
        {
            continue;
        }

        if (row == endRow && column == endColumn)
        {
            Console.WriteLine(distance + 1);
            break;
        }

        visited.Add((row, column));
        queue.Enqueue((row, column, distance + 1));
    }
}
