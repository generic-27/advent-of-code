using Day09_RopeBridge;
using System.Numerics;

var movementData = File.ReadAllLines("input.txt");

Vector2 MoveTailCloseToHeadAndGetTailPosition(string movementDirection, Vector2 headPosition, Vector2 tailPosition)
{
    var xDifference = headPosition.X - tailPosition.X;
    var yDifference = headPosition.Y - tailPosition.Y;

    if (headPosition.X == tailPosition.X)
    {
        var movementVectorX = 0;
        var movementVectorY = yDifference < 0 ? -1 : 1;

        var movementVector = new Vector2(movementVectorX, movementVectorY);

        tailPosition = tailPosition + movementVector;
    }
    else if (headPosition.Y == tailPosition.Y)
    {
        var movementVectorX = xDifference < 0 ? -1 : 1;
        var movementVectorY = 0;

        var movementVector = new Vector2(movementVectorX, movementVectorY);

        tailPosition = tailPosition + movementVector;
    }
    else if (headPosition.X != tailPosition.X && headPosition.Y != tailPosition.Y)
    {
        var movementVectorX = xDifference < 0 ? -1 : 1;
        var movementVectorY = yDifference < 0 ? -1 : 1;

        var movementVector = new Vector2(movementVectorX, movementVectorY);

        tailPosition = tailPosition + movementVector;
    }

    return tailPosition;
}

void RunPartOne()
{
    var headPosition = new Vector2(0, 0);
    var tailPosition = new Vector2(0, 0);

    var directionDictionary = Utility.DirectionDictionary;
    var diagonalDistance = Utility.DiagonalDistance;

    var tailVisitedPositionHashSet = new HashSet<Vector2>();
    tailVisitedPositionHashSet.Add(tailPosition);

    foreach (var movement in movementData)
    {
        var movementInfo = movement.Split(" ");

        var direction = directionDictionary[movementInfo[0]];
        var steps = int.Parse(movementInfo[1]);

        for (int i = 0; i < steps; i++)
        {
            headPosition = headPosition + direction;

            double distance = Math.Sqrt(Math.Pow(headPosition.X - tailPosition.X, 2) + Math.Pow(headPosition.Y - tailPosition.Y, 2));

            if (distance > diagonalDistance)
            {
                var newTailPosition = MoveTailCloseToHeadAndGetTailPosition(movementInfo[0], headPosition, tailPosition);

                if (!tailVisitedPositionHashSet.Contains(newTailPosition))
                {
                    tailVisitedPositionHashSet.Add(newTailPosition);
                }

                tailPosition = newTailPosition;
            }
        }
    }

    Console.WriteLine(tailVisitedPositionHashSet.Count());
}

RunPartOne();

void RunPartTwo()
{
    var headPosition = new Vector2(0, 0);
    var tailPositionList = new List<Vector2>();

    for (int i = 0; i < 9; i++)
    {
        tailPositionList.Add(new Vector2(0, 0));
    }

    var tailPositionListCount = tailPositionList.Count();

    var directionDictionary = Utility.DirectionDictionary;
    var diagonalDistance = Utility.DiagonalDistance;

    var tailVisitedPositionHashSet = new HashSet<Vector2>();
    tailVisitedPositionHashSet.Add(tailPositionList[8]);

    foreach (var movement in movementData)
    {
        var movementInfo = movement.Split(" ");

        var direction = directionDictionary[movementInfo[0]];
        var steps = int.Parse(movementInfo[1]);

        for (int i = 0; i < steps; i++)
        {
            headPosition = headPosition + direction;


            var currentHeadPosition = new Vector2(headPosition.X, headPosition.Y);

            for (int j = 0; j < 9; j++)
            {
                double distance = Math.Sqrt(Math.Pow(currentHeadPosition.X - tailPositionList[j].X, 2) + Math.Pow(currentHeadPosition.Y - tailPositionList[j].Y, 2));

                if (distance > diagonalDistance)
                {
                    var newTailPosition = MoveTailCloseToHeadAndGetTailPosition(movementInfo[0], currentHeadPosition, tailPositionList[j]);

                    tailPositionList[j] = newTailPosition;
                }

                currentHeadPosition = new Vector2(tailPositionList[j].X, tailPositionList[j].Y);
            }

            if (!tailVisitedPositionHashSet.Contains(tailPositionList[tailPositionListCount - 1]))
            {
                tailVisitedPositionHashSet.Add(tailPositionList[tailPositionListCount - 1]);
            }
        }
    }

    Console.WriteLine(tailVisitedPositionHashSet.Count());
}

RunPartTwo();