namespace Diff.Cli;

public class App
{
    public void Run(string[] arguments)
    {
        // 0번 인덱스의 프로세스의 이름은 제외
        var args = arguments[1..];

        if (args.Length is not 2)
        {
            throw new AppException("Please provide two file paths.");
        }

        var file1 = args[0];
        var file2 = args[1];

        CheckFileOrThrow(file1, file2);

        var diffChars = Diff(file1, file2);
        Print(diffChars);
    }

    private static IEnumerable<DiffChar> Diff(string file1, string file2)
    {
        var source = (DiffString)File.ReadAllText(file1);
        var destination = (DiffString)File.ReadAllText(file2);
        var diffSet = new DiffSet(source, destination);
        var algorithm = new LcsAlgorithm(diffSet);
        var diffChars = diffSet.Diff(algorithm);

        return diffChars;
    }

    private static void Print(IEnumerable<DiffChar> diffChars)
    {
        foreach (var ch in diffChars)
        {
            Console.WriteLine($"{GetDiffOperation(ch.Result)} {ch}");
        }

        static char GetDiffOperation(DiffResult result)
        {
            return result switch
            {
                DiffResult.None => ' ',
                DiffResult.Inserted => '+',
                DiffResult.Deleted => '-',
                _ => throw new ArgumentOutOfRangeException(nameof(result))
            };
        }
    }

    private static void CheckFileOrThrow(string file1, string file2)
    {
        if (!File.Exists(file1))
        {
            throw new AppException($"File not found: {file1}");
        }

        if (!File.Exists(file2))
        {
            throw new AppException($"File not found: {file2}");
        }
    }
}

public class AppException : Exception
{
    public AppException(string message) : base(message)
    {
    }
}
