using static Day07_NoSpaceLeftOnDevice.ElfOsContants;

namespace Day07_NoSpaceLeftOnDevice;

internal class ElfOsDirectoryProcessor
{
    private Dictionary<string, ElfOsDirectory> _elfOsDirectoriesDictionary;

    public ElfOsDirectoryProcessor()
    {
        _elfOsDirectoriesDictionary = new Dictionary<string, ElfOsDirectory>();

        var homeDirectory = new ElfOsDirectory
        {
            DirectoryName = HOME_DIRECTORY,
            ElfOsFiles = new List<ElfOsFile>(),
            ParentDirectory = null
        };

        _elfOsDirectoriesDictionary.Add(HOME_DIRECTORY, homeDirectory);
    }

    public Dictionary<string, ElfOsDirectory> GetElfOsDirectories()
    {
        return this._elfOsDirectoriesDictionary;
    }

    public void BuildDirectoryDictionary(string[] fileCommands)
    {
        var searchedDirectoryName = "";

        var currentDirectory = _elfOsDirectoriesDictionary[HOME_DIRECTORY];

        for (int i = 1; i < fileCommands.Length; i++)
        {
            var commands = fileCommands[i].Split(' ');


            switch (commands[1])
            {
                case "cd":
                    searchedDirectoryName = commands[2];

                    if (searchedDirectoryName == "..")
                    {
                        var parentDirectory = currentDirectory.ParentDirectory;

                        if (parentDirectory != null)
                        {
                            currentDirectory = parentDirectory;
                        }
                    }
                    else
                    {
                        var searchedDirectory = FindDirectory(searchedDirectoryName, currentDirectory);

                        if (searchedDirectory is null)
                        {
                            AddDirectory(searchedDirectoryName, currentDirectory);
                        }
                        else
                        {
                            currentDirectory = searchedDirectory;
                        }
                    }

                    break;
                case "ls":
                    i += 1;
                    var innerCommand = fileCommands[i].Split(" ");


                    while (innerCommand[0] != "$")
                    {

                        if (innerCommand[0] == "dir")
                        {
                            AddDirectory(innerCommand[1], currentDirectory);
                        }
                        else
                        {
                            AddFile(int.Parse(innerCommand[0]), innerCommand[1], currentDirectory);
                        }

                        i += 1;
                        if (i >= fileCommands.Length)
                        {
                            break;
                        }
                        innerCommand = fileCommands[i].Split(" ");
                    }

                    i -= 1;
                    break;
            }
        }
    }

    public Dictionary<string, ElfOsDirectory> GetDirectories()
    {
        return _elfOsDirectoriesDictionary;
    }

    public int GetTotalFileSizeOfDirectory(ElfOsDirectory elfOsDirectory)
    {
        int total = 0;

        Stack<ElfOsDirectory> stack = new Stack<ElfOsDirectory>();

        var fileSize = elfOsDirectory.ElfOsFiles.Count();

        for (int i = 0; i < fileSize; i++)
        {
            total += elfOsDirectory.ElfOsFiles[i].FileSize;
        }

        foreach (var directory in elfOsDirectory.ElfOSDirectories.Values)
        {
            stack.Push(directory);
        }

        while (stack.Count > 0)
        {
            var directory = stack.Pop();

            var files = directory.ElfOsFiles;

            for (int i = 0; i < files.Count(); i++)
            {
                total += files[i].FileSize;
            }

            var directories = directory.ElfOSDirectories.Values;

            foreach (var dir in directories)
            {
                stack.Push(dir);
            }
        }

        return total;
    }

    public void AddFile(int fileSize, string fileName, ElfOsDirectory currentDirectory)
    {
        var file = new ElfOsFile
        {
            FileName = fileName,
            FileSize = fileSize
        };

        currentDirectory.ElfOsFiles.Add(file);
    }

    private void AddDirectory(string directoryName, ElfOsDirectory parentDirectory)
    {
        var elfOsDirectory = new ElfOsDirectory
        {
            DirectoryName = directoryName,
            ElfOsFiles = new List<ElfOsFile>(),
            ParentDirectory = parentDirectory
        };

        var elfOsDirectoriesDictionary = parentDirectory.ElfOSDirectories;

        if (!elfOsDirectoriesDictionary.ContainsKey(directoryName))
        {
            elfOsDirectoriesDictionary.Add(directoryName, elfOsDirectory);
        }
    }

    private ElfOsDirectory? FindDirectory(string directoryName, ElfOsDirectory currentDirectory)
    {
        var directories = currentDirectory.ElfOSDirectories;

        return directories[directoryName];
    }
}
