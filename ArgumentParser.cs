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
        var argsCount = MethodInfo.GetCurrentMethod()?.GetGenericArguments()?.Length ?? 0;
        var args = _args.SkipWhile(x => x == _featureFlag).Take(argsCount).ToArray();

        return (T1)Convert.ChangeType(args[0], typeof(T1));
    }

    public (T1 A1, T2 A2) GetArguments<T1, T2>()
    {
        var argsCount = MethodInfo.GetCurrentMethod()?.GetGenericArguments()?.Length ?? 0;
        var args = _args.SkipWhile(x => x == _featureFlag).Take(argsCount).ToArray();

        return ((T1)Convert.ChangeType(args[0], typeof(T1)), (T2)Convert.ChangeType(args[1], typeof(T2)));
    }
}