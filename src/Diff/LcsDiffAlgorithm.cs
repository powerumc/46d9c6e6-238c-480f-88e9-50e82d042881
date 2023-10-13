using System.Diagnostics;

namespace Diff;

/// <summary>
///     LCS 알고리즘 클래스 입니다.
/// </summary>
public class LcsAlgorithm : IDiffAlgorithm
{
    private readonly IDiffSet _diffSet;
    private readonly DiffChar[,] _metrics; // LCS 테이블

    /// <summary>
    ///     <see cref="DiffSet" /> 정보로 알고리즘 인스턴스를 생성합니다.
    /// </summary>
    /// <param name="diffSet"><see cref="IDiffSet" /> 인스턴스</param>
    public LcsAlgorithm(IDiffSet diffSet)
    {
        _diffSet = diffSet;
        _metrics = new DiffChar[diffSet.Source.Length + 1, diffSet.Destination.Length + 1];
    }

    /// <inheritdoc />
    public IEnumerable<DiffChar> Execute()
    {
        var source = _diffSet.Source;
        var destination = _diffSet.Destination;

        for (var y = 1; y <= source.Length; y++)
        {
            var src = source[y - 1];
            for (var x = 1; x <= destination.Length; x++)
            {
                var dest = destination[x - 1];

                if (src == dest)
                {
                    _metrics[y, x] = _metrics[y - 1, x - 1].IncreaseValue();
                }
                else
                {
                    _metrics[y, x] = _metrics[y - 1, x].Value > _metrics[y, x - 1].Value
                        ? _metrics[y - 1, x]
                        : _metrics[y, x - 1];
                }
            }
        }

        return UpdateLcs(_diffSet, destination.Length, source.Length);
    }

    /// <summary>
    ///     LCS 테이블에 순서대로 <see cref="DiffResult" /> 를 업데이트하고, <see cref="DiffChar" /> 를 반환합니다.
    /// </summary>
    /// <param name="diffSet">Diff 원본과 사본 정보를 갖는 <see cref="DiffSet" /> 인스턴스</param>
    /// <param name="y">LCS 테이블 행 인덱스</param>
    /// <param name="x">LCS 테이블 열 인덱스</param>
    /// <returns>Diff 결과로 <see cref="DiffChar" /> 를 순서대로 반환</returns>
    private IEnumerable<DiffChar> UpdateLcs(IDiffSet diffSet, int y, int x)
    {
        if (x == 0 && y == 0)
        {
            yield break;
        }

        var location = _metrics[x, y];

        // 삭제됨
        if (x > 0 && (y == 0 || location.Value == _metrics[x - 1, y].Value))
        {
            foreach (var diffChar in UpdateLcs(diffSet, y, x - 1))
            {
                yield return diffChar;
            }

            _metrics[x, y] = location.Update(diffSet.Source[x - 1]).UpdateResult(DiffResult.Deleted);

            yield return _metrics[x, y];
        }
        // 추가됨
        else if (y > 0 && (x == 0 || location.Value == _metrics[x, y - 1].Value))
        {
            foreach (var diffChar in UpdateLcs(diffSet, y - 1, x))
            {
                yield return diffChar;
            }

            _metrics[x, y] = location.Update(diffSet.Destination[y - 1]).UpdateResult(DiffResult.Inserted);

            yield return _metrics[x, y];
        }
        // 동일함
        else
        {
            foreach (var diffChar in UpdateLcs(diffSet, y - 1, x - 1))
            {
                yield return diffChar;
            }

            _metrics[x, y] = location.Update(diffSet.Source[x - 1]);

            yield return _metrics[x, y];
        }
    }

    [Conditional("DEBUG")]
    internal void PrintMetrics(TextWriter writer)
    {
        var yLength = _metrics.GetLength(0);
        var xLength = _metrics.GetLength(1);

        for (var y = 0; y < yLength; y++)
        {
            for (var x = 0; x < xLength; x++)
            {
                if (x > 0)
                {
                    writer.Write(" ");
                }

                writer.Write(_metrics[y, x].Value);
            }

            if (y < yLength - 1)
            {
                writer.WriteLine();
            }
        }
    }
}
