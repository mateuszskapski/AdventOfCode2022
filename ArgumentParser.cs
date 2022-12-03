using System.Reflection;

public class ArgumentParser
{
    private string _featureFlag;
    private string[] _args;

    public ArgumentParser(string featureFlag, string[] args)
    {
        _featureFlag = featureFlag;
        _args = args;
    }

    public T1 GetArguments<T1>()
    {
        var args = GetArguments(1);
        return (T1)Convert.ChangeType(args[0], typeof(T1));
    }

    public (T1 A1, T2 A2) GetArguments<T1, T2>()
    {
        var args = GetArguments(2);
        return ((T1)Convert.ChangeType(args[0], typeof(T1)), (T2)Convert.ChangeType(args[1], typeof(T2)));
    }

    public (T1 A1, T2 A2, T3 A3) GetArguments<T1, T2, T3>()
    {
        var args = GetArguments(3);
        return ((T1)Convert.ChangeType(args[0], typeof(T1)), (T2)Convert.ChangeType(args[1], typeof(T2)), (T3)Convert.ChangeType(args[2], typeof(T3)));
    }

    string[] GetArguments(int count) =>
        _args.SkipWhile(x => x == _featureFlag).Take(count).ToArray();
        
}