public class Day02 : Resolver
{
    Dictionary<char, int> Elf = new ()
    {
        { 'A', 1 },
        { 'B', 2 },
        { 'C', 3 }
    };

    Dictionary<char, int> Me = new ()
    {
        { 'X', 1 },
        { 'Y', 2 },
        { 'Z', 3 }
    };

    enum Result
    {
        Loss = 0,
        Drew = 3,
        Win = 6
    };

    public Day02(int year, int day) : base(year, day) {}
    public override object PartOne(string input) => CalculateScore(input).Select(x => new char[] {x[0][0], x[1][0]}).Sum(GetMyGameScore);
    public override object PartTwo(string input) => CalculateScore(input).Sum(FollowElfInstruction);

    IEnumerable<string[]> CalculateScore(string input) =>
        input.Split("\n").Select(x => x.Split(" ").Where(x => !string.IsNullOrEmpty(x)).ToArray());

    int GetMyGameScore(char[] game)
    {
        var elfMove = game[0];
        var myMove = game[1];

        int score = 0;
        if (!IsDraw(myMove, elfMove))
        {
            if (IsMyWin(myMove, elfMove))
            {
                score = (int)Result.Win + Me[myMove];
            }
            else
            {
                score = (int)Result.Loss + Me[myMove];  
            }
        }
        else
        {
            score = (int)Result.Drew + Me[myMove];
        }

        Console.WriteLine($"Elf {game[0]}, My {game[1]}, Score: {score}");

        return score;
    }

    int FollowElfInstruction(string[] game)
    {
        var elfMove = game[0][0];
        var expectedGameResult = game[1][0];

        switch (expectedGameResult)
        {
            case 'X': return GetMyGameScore(new char[] { elfMove, GetMoveToLose(elfMove) }); // Loss
            case 'Y': return GetMyGameScore(new char[] { elfMove, GetMoveToDraw(elfMove) }); // Drew
            case 'Z': return GetMyGameScore(new char[] { elfMove, GetMoveToWin(elfMove) }); // Win
        }

        return 0;
    }
    
    bool IsMyWin(char myMove, char elfMove) => 
        (myMove == 'X' && elfMove == 'C') || 
        (myMove == 'Y' && elfMove == 'A') ||
        (myMove == 'Z' && elfMove == 'B');

    bool IsDraw(char myMove, char elfMove) => myMove == (char)(elfMove+23);

    char GetMoveToWin(char elfMove) => elfMove switch
    {
        'A' => 'Y',
        'B' => 'Z',
        'C' => 'X',
        _ => throw new ArgumentException("Move not supported")
    };

    char GetMoveToLose(char elfMove) => elfMove switch
    {
        'A' => 'Z',
        'B' => 'X',
        'C' => 'Y',
        _ => throw new ArgumentException("Move not supported")
    };

    char GetMoveToDraw(char elfMove) => elfMove switch
    {
        'A' => 'X',
        'B' => 'Y',
        'C' => 'Z',
        _ => throw new ArgumentException("Move not supported")
    };
}