using Day08_TreetopTreeHouse;
using System.Numerics;

var treeData = File.ReadAllLines("input.txt");

bool CheckTreeVisibleInDirection(List<List<int>> treeGrid, int treeCol, int treeRow, Vector2 direction)
{
    int col = treeCol + (int)direction.Y;
    int row = treeRow + (int)direction.X;

    while (col >= 0 && col < treeGrid[0].Count() &&
           row >= 0 && row < treeGrid.Count())
    {
        if (treeGrid[treeRow][treeCol] <= treeGrid[row][col])
        {
            return false;
        }

        col = col + (int)direction.Y;
        row = row + (int)direction.X;
    }

    return true;
}

bool CheckTreeIsVisibleFromAnyDirection(List<List<int>> treeGrid, int treeCol, int treeRow)
{
    var directions = Utility.Get2DDirections();

    for (int i = 0; i < directions.Count(); i++)
    {
        var isVisible = CheckTreeVisibleInDirection(treeGrid, treeCol, treeRow, directions[i]);
        if (isVisible)
        {
            return true;
        }
    }

    return false;
}

void RunPartOne()
{
    var treeGrid = new List<List<int>>();

    foreach (var trees in treeData)
    {
        var list = trees.ToCharArray().Select(c => int.Parse(c.ToString())).ToList();

        treeGrid.Add(list);
    }

    int visibleTrees = 0;

    // Number of rows
    visibleTrees += treeGrid.Count() * 2;
    // Remove the duplicates we counted in the rows
    visibleTrees += treeGrid[0].Count() * 2 - 4;

    for (int row = 1; row < treeGrid.Count() - 1; row++)
    {
        for (int col = 1; col < treeGrid[row].Count - 1; col++)
        {
            if (CheckTreeIsVisibleFromAnyDirection(treeGrid, col, row))
            {
                visibleTrees++;
            }
        }
    }

    Console.WriteLine(visibleTrees);
}

RunPartOne();

int GetScenicScoreinDirection(List<List<int>> treeGrid, int treeRow, int treeCol, Vector2 direction)
{
    int col = treeCol + (int)direction.Y;
    int row = treeRow + (int)direction.X;

    int scenicScore = 0;

    while (col >= 0 && col < treeGrid[0].Count() &&
           row >= 0 && row < treeGrid.Count())
    {
        scenicScore++;

        if (treeGrid[treeRow][treeCol] <= treeGrid[row][col])
        {
            break;
        }

        col = col + (int)direction.Y;
        row = row + (int)direction.X;
    }

    return scenicScore;
}

int GetScenicScoreOfTree(List<List<int>> treeGrid, int treeRow, int treeCol)
{
    var directions = Utility.Get2DDirections();

    int scenicScore = 1;

    foreach (var direction in directions)
    {
        int score = GetScenicScoreinDirection(treeGrid, treeRow, treeCol, direction);

        scenicScore *= score;
    }

    return scenicScore;
}

void RunPartTwo()
{
    var treeGrid = new List<List<int>>();

    foreach (var trees in treeData)
    {
        var list = trees.ToCharArray().Select(c => int.Parse(c.ToString())).ToList();

        treeGrid.Add(list);
    }

    int maxScore = int.MinValue;

    for (int row = 1; row < treeGrid.Count(); row++)
    {
        for (int col = 1; col < treeGrid[row].Count(); col++)
        {
            int score = GetScenicScoreOfTree(treeGrid, row, col);

            maxScore = score > maxScore ? score : maxScore;
        }
    }

    Console.WriteLine(maxScore);
}

RunPartTwo();