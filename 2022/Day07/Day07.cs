using System.Text;

public record DirFile(string Name, int Size);
public record Dir
{
    public string Name {get; } 
    public Dir? Parent {get; set;} 
    public List<Dir> ChildDirs {get; set;} 
    public List<DirFile> ChildFiles {get; set;}
    public int Size => ChildDirs.Sum(x => x.Size) + ChildFiles.Sum(x => x.Size);
    public Dir(string name, Dir? parent, List<Dir> childDirs, List<DirFile> childFiles)
    {
        Name = name;
        Parent = parent;
        ChildDirs = childDirs;
        ChildFiles = childFiles;
    }
}
public enum CommandType
{
    ChangeDir,
    List,
    IsDirectory,
    IsFile
};
public record Command(CommandType CommandType, string? Value);


public class Day07 : Problem<Dir>
{
    Dir topDir = new Dir("/", null, null, null);
    public Day07(int year, int day, string fileName) : base(year, day, fileName)
    {
    }

    protected override object PartOne(List<Dir> input)
    {
        var dirsMatchingCriteria = new List<Dir>();

        CheckDirSize(input.First(), dirsMatchingCriteria, 100000);

        return dirsMatchingCriteria.Sum(x => x.Size);
    }

    protected override object PartTwo(List<Dir> input)
    {
        var dirsMatchingCriteria = new List<Dir>();

        var topDir = input.First();
        CheckDirSize(topDir, dirsMatchingCriteria);
        var freeSpace = 70000000 - topDir.Size;
        var neededSpace = 30000000 - freeSpace;
        
        return dirsMatchingCriteria.Select(x => x.Size).Where(x => x >= neededSpace).OrderBy(x => x).First();
    }

    void CheckDirSize(Dir currentDir, List<Dir> dirsMatchingCriteria, int? sizeLimit = null)
    {
        if (currentDir.Size <= sizeLimit || sizeLimit is null)
        {
            dirsMatchingCriteria.Add(currentDir);
        }  

        if (currentDir.ChildDirs.Count == 0)
            return;

        foreach (var dir in currentDir.ChildDirs)
        {
            CheckDirSize(dir, dirsMatchingCriteria, sizeLimit);
        }
    }

    protected override async Task<List<Dir>> GetInput(string path, bool includeEmptyLine = false)
    {
        var inputLines = new List<Dir>();
        // Setup top directory
        inputLines.Add(new Dir("/",null,new List<Dir>(),new List<DirFile>()));
        var topDir = inputLines[0];
        var currentDir = topDir;
        using var fileStream = File.OpenRead(path);
        using var reader = new StreamReader(fileStream, Encoding.UTF8, true, 512);
        string line = string.Empty;
        while ((line = await reader.ReadLineAsync()) is not null)
        {
            if (string.IsNullOrEmpty(line) && includeEmptyLine)
                continue;

            var command = ParseCommand(line);
            switch (command.CommandType)
            {
                case CommandType.ChangeDir: 
                {
                    if (command.Value == "/")
                    {
                        currentDir = topDir;
                    }
                    else if (command.Value == "..")
                    {
                        currentDir = currentDir?.Parent ?? topDir;
                    }
                    else 
                    {
                        currentDir = currentDir.ChildDirs.First(x => x.Name == command.Value);
                    }
                    break;
                }
                case CommandType.List: break;
                case CommandType.IsDirectory:
                {
                    currentDir.ChildDirs.Add(new Dir(command.Value, currentDir, new List<Dir>(), new List<DirFile>()));
                    break;
                }
                case CommandType.IsFile:
                {
                    var fileData = command.Value.Split(" ");
                    currentDir.ChildFiles.Add(new DirFile(fileData[1], int.Parse(fileData[0])));
                    break;
                }
                default:
                    throw new InvalidOperationException("Command not supported.");
            }
        }

        return inputLines;
    }

    protected override Dir? ProcessLine(string line) => null;

    Command ParseCommand(string line)
    {
        if (line.StartsWith("$ ls"))
        {
            return new Command(CommandType.List, null);
        }
        else if (line.StartsWith("$ cd"))
        {
            return new Command(CommandType.ChangeDir, line.Split(" ").Last());
        }
        else
        {
            var command = line.Split(" ");
            if (command[0] == "dir")
            {
                return new Command(CommandType.IsDirectory, command[1]);
            }
            else
            {
                return new Command(CommandType.IsFile, line);
            }
        }
    }
}
