int year = 0;
int day = 0;

if (!ParseArguments())
    return;

var resolver = Activator.CreateInstance(Type.GetType($"Day{day.ToString("00")}"), year, day) as Resolver;
var results = resolver?.Run();

Console.WriteLine("*** Part One ***");
Console.WriteLine($"Answer: {results?.PartOne}");
Console.WriteLine("*** Part Two ***");
Console.WriteLine($"Answer: {results?.PartTwo}");

bool ParseArguments()
{
    try
    {
        year = int.Parse(args[0]);
        day = int.Parse(args[1]);
    }
    catch
    {
        Console.WriteLine("Invalid arguments.");
        return false;
    }

    return true;
}