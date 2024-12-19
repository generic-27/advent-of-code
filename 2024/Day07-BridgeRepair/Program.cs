var fileInformation = File.ReadAllLines("input.txt");
PartOne();
PartTwo();
return;


void PartOne()
{
    long testValueTotal = 0;
    foreach (var line in fileInformation)
    {
        var parts = line.Split(":");
        var total = long.Parse(parts[0]);
        
        var equations = parts[1].Trim().Split(" ");

        var currentValueQueue = GetCurrentValueQueueForPartOne(equations);

        testValueTotal += GetTestValueTotal(currentValueQueue, total);
    }
    
    Console.WriteLine(testValueTotal);
}

void PartTwo()
{
    long testValueTotal = 0;
    foreach (var line in fileInformation)
    {
        var parts = line.Split(":");
        var total = long.Parse(parts[0]);
        
        var equations = parts[1].Trim().Split(" ");

        var currentValueQueue = GetCurrentValueQueueForPartTwo(equations);

        testValueTotal += GetTestValueTotal(currentValueQueue, total);
    }
    
    Console.WriteLine(testValueTotal);
}

Queue<long> GetCurrentValueQueueForPartOne(string[] equations)
{
    var currentValueQueue = new Queue<long>();
    currentValueQueue.Enqueue(long.Parse(equations[0]));
        
    for (var i = 1; i < equations.Length; i++)
    {
        var value = long.Parse(equations[i]);

        var stackCount = currentValueQueue.Count;
        while (stackCount > 0)
        {
            var currentValue = currentValueQueue.Dequeue();
            currentValueQueue.Enqueue(currentValue + value);
            currentValueQueue.Enqueue(currentValue * value);
            stackCount--;
        }
    }
    
    return currentValueQueue;
}

Queue<long> GetCurrentValueQueueForPartTwo(string[] equations)
{
    var currentValueQueue = new Queue<long>();
    currentValueQueue.Enqueue(long.Parse(equations[0]));
        
    for (var i = 1; i < equations.Length; i++)
    {
        var value = long.Parse(equations[i]);

        var stackCount = currentValueQueue.Count;
        while (stackCount > 0)
        {
            var currentValue = currentValueQueue.Dequeue();
            currentValueQueue.Enqueue(currentValue + value);
            currentValueQueue.Enqueue(currentValue * value);
            currentValueQueue.Enqueue(long.Parse(currentValue + "" + value));
            stackCount--;
        }
    }
    
    return currentValueQueue;
}

long GetTestValueTotal(Queue<long> currentValueQueue, long total)
{
    var testValueTotal = 0L;
    while (currentValueQueue.Count > 0)
    {
        var currentValue = currentValueQueue.Dequeue();
        if (currentValue == total)
        {
            testValueTotal += total;
            break;
        }
    }
    
    return testValueTotal;
}