using System.Diagnostics;

namespace Diff;

[Obsolete("이 클래스는 더 이상 사용하지 않습니다. 대신 DiffSet 클래스를 사용하세요.")]
public class DiffAlgorithm
{
    private readonly int _destLength;
    private readonly string _destString;
    private readonly int[,] _metrics;
    private readonly int _srcLength;
    private readonly string _srcString;

    private DiffAlgorithm(string srcString, string destString)
    {
        _srcString = srcString;
        _destString = destString;
        _srcLength = srcString.Length;
        _destLength = destString.Length;
        _metrics = new int[_srcLength + 1, _destLength + 1];
    }

    public static DiffAlgorithm Create(string srcString, string destString)
    {
        return new DiffAlgorithm(srcString, destString);
    }

    public void Build(string srcString, string destString)
    {
        for (var y = 1; y <= _srcLength; y++)
        {
            var origin = srcString[y - 1];
            for (var x = 1; x <= _destLength; x++)
            {
                var dest = destString[x - 1];

                if (origin == dest)
                {
                    _metrics[y, x] += _metrics[y - 1, x - 1] + 1;
                }
                else
                {
                    _metrics[y, x] = Math.Max(_metrics[y - 1, x], _metrics[y, x - 1]);
                }
            }
        }
    }

    public void PrintText(TextWriter writer)
    {
        PrintInternal(_destLength, _srcLength, writer);
    }

    private void PrintInternal(int y, int x, TextWriter writer)
    {
        if (x == 0 && y == 0)
        {
            return;
        }

        if (x > 0 && (y == 0 || _metrics[x, y] == _metrics[x - 1, y]))
        {
            PrintInternal(y, x - 1, writer);
            writer.WriteLine($"- {_srcString[x - 1]}");
        }
        else if (y > 0 && (x == 0 || _metrics[x, y] == _metrics[x, y - 1]))
        {
            PrintInternal(y - 1, x, writer);
            writer.WriteLine($"+ {_destString[y - 1]}");
        }
        else
        {
            PrintInternal(y - 1, x - 1, writer);
            writer.WriteLine($"  {_srcString[x - 1]}");
        }
    }

    [Conditional("DEBUG")]
    public void PrintLCS(TextWriter writer)
    {
        for (var y = 0; y < _metrics.GetLength(0); y++)
        {
            for (var x = 0; x < _metrics.GetLength(1); x++)
            {
                writer.Write(_metrics[y, x] + " ");
            }

            writer.WriteLine();
        }
    }
}
