namespace Day07_NoSpaceLeftOnDevice;

internal class ElfOsDirectory
{
    public string DirectoryName { get; set; } = string.Empty;

    public ElfOsDirectory? ParentDirectory { get; set; }

    public Dictionary<string, ElfOsDirectory> ElfOSDirectories { get; set; } = new Dictionary<string, ElfOsDirectory>();

    public List<ElfOsFile> ElfOsFiles { get; set; } = new List<ElfOsFile> { };

}
