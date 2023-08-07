using Day07_NoSpaceLeftOnDevice;
using static Day07_NoSpaceLeftOnDevice.ElfOsContants;

var fileCommands = File.ReadAllLines("input.txt");

var elfOsDirectoryProcessor = new ElfOsDirectoryProcessor();

elfOsDirectoryProcessor.BuildDirectoryDictionary(fileCommands);

void RunPartOne()
{
    var elfOsDirectories = elfOsDirectoryProcessor.GetElfOsDirectories();

    Stack<ElfOsDirectory> stack = new Stack<ElfOsDirectory>();

    stack.Push(elfOsDirectories[HOME_DIRECTORY]);

    var list = new List<int>();

    while (stack.Count() > 0)
    {
        var currentDirectory = stack.Pop();

        var currentDirectoryTotalFileSize = elfOsDirectoryProcessor.GetTotalFileSizeOfDirectory(currentDirectory);

        if (currentDirectoryTotalFileSize <= 100000)
        {
            list.Add(currentDirectoryTotalFileSize);
        }

        foreach (var directory in currentDirectory.ElfOSDirectories.Values)
        {
            stack.Push(directory);
        }
    }

    var total = list.Aggregate((x, y) => x + y);

    Console.WriteLine(total);
}

RunPartOne();

void RunPartTwo()
{
    var list = new List<long>();

    var elfOsDirectories = elfOsDirectoryProcessor.GetElfOsDirectories();

    long totalFileSize = elfOsDirectoryProcessor.GetTotalFileSizeOfDirectory(elfOsDirectories[HOME_DIRECTORY]);

    Stack<ElfOsDirectory> stack = new Stack<ElfOsDirectory>();

    stack.Push(elfOsDirectories[HOME_DIRECTORY]);

    while (stack.Count() > 0)
    {
        var currentDirectory = stack.Pop();

        var currentDirectoryFileSize = elfOsDirectoryProcessor.GetTotalFileSizeOfDirectory(currentDirectory);

        list.Add(currentDirectoryFileSize);

        foreach (var directory in currentDirectory.ElfOSDirectories.Values)
        {
            stack.Push(directory);
        }
    }

    list.Sort();

    var availableSize = TOTAL_DISK_SPACE - totalFileSize;

    for (int i = 0; i < list.Count(); i++)
    {
        if (list[i] + availableSize >= UNUSED_SPACE_REQUIRED)
        {
            Console.WriteLine(list[i]);
            break;
        }
    }
}

RunPartTwo();