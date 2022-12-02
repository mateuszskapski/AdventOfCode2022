// See https://aka.ms/new-console-template for more information
foreach(var arg in args)
{
    Console.WriteLine(arg);
}

var path = "C:\\Users\\mateusz.skapski\\source\\repos\\AdventOfCode\\2022\\Day02\\input.txt";
var input = GetInput(path);

var resolver = new Day02(2022, 2);

Console.WriteLine($"Reading file {resolver.GetInputFilePath()}");

Console.WriteLine("*** Part One ***");
Console.WriteLine($"Answer: {resolver.PartOne(input)}");
Console.WriteLine("*** Part Two ***");
Console.WriteLine($"Answer: {resolver.PartTwo(input)}");

string GetInput(string path)
{
    var input = File.ReadAllText(path);
    if (input[input.Length - 1] == '\n')
        input = input.Substring(0, input.Length - 1);

    return input;
}