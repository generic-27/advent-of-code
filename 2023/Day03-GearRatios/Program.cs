using Day03_GearRatios;
using System.Text;

var engineSchematic = File.ReadAllLines("input.txt");


void PartOne()
{
    int total = 0;

    var symbols = new List<char>
    {
        '!', '"', '#', '$', '%', '&', '\'', '(', ')', '*', '+', '-', ',', '/'
    };

    var rowColumNumberStringBuilderList = GetRowColumnAndNumberStringBuilderList();

    foreach (var numberStringBuilder in rowColumNumberStringBuilderList)
    {
        if (numberStringBuilder.Item3.Length > 0)
        {

            var position = GetNumberNeighborSymbolPosition(numberStringBuilder.Item1, numberStringBuilder.Item2, symbols);
            if (position.Item1 != -1 && position.Item2 != -1)
            {
                var value = int.Parse(numberStringBuilder.Item3.ToString());

                total += value;
            }
        }
    }

    Console.WriteLine(total);
}

(int, int) GetNumberNeighborSymbolPosition(int row, List<int> columnIndices, List<char> symbols)
{
    foreach (var column in columnIndices)
    {
        var position = GetPositionNeighborSymbol(row, column, symbols);

        if (position.Item1 != -1 && position.Item2 != -1)
        {
            return position;
        }
    }

    return (-1, -1);
}

(int, int) GetPositionNeighborSymbol(int row, int col, List<char> symbols)
{
    int zeroAscii = '0';
    int nineAscii = '9';

    var directions = new List<(int, int)>
    {
        ( 1, 0 ), // Up
        ( 0, 1 ), // Right
        ( -1, 0 ), // Down
        ( 0, -1 ), // Left
        ( -1, -1 ), // Top Left
        ( -1, 1 ), // Top Right
        ( 1, -1 ), // Bottom Left
        ( 1, 1 )// Bottom Right
    };

    int numberOfRows = engineSchematic.Length;
    int numberOfColumns = engineSchematic[0].Length;

    foreach (var direction in directions)
    {
        int newRow = row + direction.Item1;
        int newCol = col + direction.Item2;

        if (newRow < 0 || newRow > numberOfRows - 1 ||
            newCol < 0 || newCol > numberOfRows - 1)
        {
            continue;
        }

        var currentChar = engineSchematic[newRow][newCol];

        if (symbols.Contains(currentChar) && currentChar < zeroAscii || currentChar > nineAscii)
        {
            return (newRow, newCol);
        }
    }

    return (-1, 1);
}

List<(int, List<int>, StringBuilder)> GetRowColumnAndNumberStringBuilderList()
{
    int zeroAscii = '0';
    int nineAscii = '9';

    var list = new List<(int, List<int>, StringBuilder)>();

    for (int row = 0; row < engineSchematic.Length; row++)
    {
        for (int j = 0; j < engineSchematic[row].Length; j++)
        {
            var currentChar = engineSchematic[row][j];

            var stringBuilder = new StringBuilder();
            var columnIndicesList = new List<int>();

            if (currentChar >= zeroAscii && currentChar <= nineAscii)
            {
                while (j < engineSchematic[row].Length)
                {
                    currentChar = engineSchematic[row][j];

                    if (currentChar >= zeroAscii && currentChar <= nineAscii)
                    {
                        stringBuilder.Append(currentChar);
                        columnIndicesList.Add(j);
                        j++;
                        continue;
                    }

                    j--;
                    break;
                }
            }

            if (stringBuilder.Length > 0)
            {
                list.Add((row, columnIndicesList, stringBuilder));
            }
        }
    }

    return list;
}

//PartOne();

void PartTwo()
{
    long total = 0;

    var symbols = new List<char>
    {
        '*'
    };

    var rowColumNumberStringBuilderList = GetRowColumnAndNumberStringBuilderList();

    var gearPartDictionary = new Dictionary<Node, List<int>>();

    foreach (var rowColumnNumberStringBuilder in rowColumNumberStringBuilderList)
    {
        if (rowColumnNumberStringBuilder.Item3.Length > 0)
        {
            var position = GetNumberNeighborSymbolPosition(rowColumnNumberStringBuilder.Item1, rowColumnNumberStringBuilder.Item2, symbols);

            if (position.Item1 != -1 & position.Item2 != -1)
            {
                var value = int.Parse(rowColumnNumberStringBuilder.Item3.ToString());

                var node = new Node(position.Item1, position.Item2);

                if (gearPartDictionary.ContainsKey(node))
                {
                    gearPartDictionary[node].Add(value);
                }
                else
                {
                    gearPartDictionary.Add(node, new List<int> { value });
                }
            }
        }
    }

    foreach (var values in gearPartDictionary.Values)
    {
        if (values.Count == 2)
        {
            long multipliedValue = values[0] * values[1];

            total += multipliedValue;
        }
    }

    Console.WriteLine(total);
}

PartTwo();