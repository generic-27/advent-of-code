var xmasData = File.ReadAllLines("input.txt");
var characterGrid = xmasData.Select(line => line.ToList()).ToList();

void PartOne()
{
    var directions = new int[][]
    {
        [0, 1],
        [1, 0],
        [0, -1],
        [-1, 0],
        [1, 1],
        [-1, 1],
        [1, -1],
        [-1, -1]
    };

    var xCharacterList = new List<(int, int)>();
    
    for (var y = 0; y < characterGrid.Count; y++)
    {
        for (var x = 0; x < characterGrid[y].Count; x++)
        {
            if (characterGrid[y][x] == 'X')
            {
                xCharacterList.Add((y, x));
            }
        }
    }
    
    var xmasStack = new Stack<char>(['X','M','A','S']);
    
    var count = GetNumberOfXmasFormed(xCharacterList, directions, xmasStack);
    
    Console.WriteLine(count);
}

void PartTwo()
{
    var aCharacterList = new List<(int, int)>();
    
    for (var y = 0; y < characterGrid.Count; y++)
    {
        for (var x = 0; x < characterGrid[y].Count; x++)
        {
            if (characterGrid[y][x] == 'A')
            {
                aCharacterList.Add((y, x));
            }
        }
    }

    var count = GetNumberOfMasFormed(aCharacterList);
    Console.WriteLine(count);
}

int GetNumberOfMasFormed(List<(int, int)> aCharacterList)
{
    var count = 0;
    
    foreach (var mCharacterNode in aCharacterList)
    {
        int row = mCharacterNode.Item1;
        int column = mCharacterNode.Item2;
        
        var topRow = row - 1;
        var leftColumn = column - 1;
        
        var bottomRow = row + 1;
        var rightColumn = column + 1;

        if (topRow < 0 || leftColumn < 0 ||
            bottomRow >= characterGrid.Count ||
            rightColumn >= characterGrid[0].Count)
        {
            continue;
        }

        var masString = $"{characterGrid[topRow][leftColumn]}{characterGrid[row][column]}{characterGrid[bottomRow][rightColumn]}";
        var masString2 = $"{characterGrid[topRow][rightColumn]}{characterGrid[row][column]}{characterGrid[bottomRow][leftColumn]}";

        if ((masString == "MAS" || masString == "SAM") && (masString2 == "MAS" || masString2 == "SAM"))
        {
            count++;
        }
    }

    return count;
}

int GetNumberOfXmasFormed(List<(int, int)> xCharacterList, int[][] directions, Stack<char> xmasStack)
{
    var count = 0;
    
    foreach (var node in xCharacterList)
    {
        var stack = new Stack<(int, int)>();
        stack.Push(node);

        foreach (var direction in directions)
        {
            var currentStack = new Stack<(int, int)>();
            currentStack.Push(stack.Peek());
            
            var xmasStackCopy = new Stack<char>(xmasStack);

            while (currentStack.Count > 0 && xmasStackCopy.Count > 0)
            {
                var currentNode = currentStack.Pop();
                var currentXmasNode = xmasStackCopy.Pop();

                if (characterGrid[currentNode.Item1][currentNode.Item2] == currentXmasNode)
                {
                    var nextNodeRow = currentNode.Item1 + direction[0];
                    var nextNodeColumn = currentNode.Item2 + direction[1];

                    if (currentXmasNode == 'S')
                    {
                        count++;
                        break;
                    }
                    
                    if (nextNodeRow >= 0 && nextNodeRow < characterGrid.Count &&
                        nextNodeColumn >= 0 && nextNodeColumn < characterGrid[nextNodeRow].Count)
                    {
                        currentStack.Push((nextNodeRow, nextNodeColumn));
                    }
                }
            }
        }

        stack.Pop();
    }

    return count;
}

// PartOne(); 
PartTwo();