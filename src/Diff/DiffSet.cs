namespace Diff;

/// <summary>
///     Diff 원본, 대상을 가지는 세트 클래스 입니다.
/// </summary>
public class DiffSet : IDiffSet
{
    /// <summary>
    ///     Diff 원본, 대상 문자열로 <see cref="DiffSet" /> 인스턴스를 생성합니다.
    /// </summary>
    /// <param name="source">원번 Diff 문자열</param>
    /// <param name="destination">사본 Diff 문자열</param>
    public DiffSet(DiffString source, DiffString destination)
    {
        Source = source;
        Destination = destination;
    }

    /// <inheritdoc />
    public DiffString Source { get; }

    /// <inheritdoc />
    public DiffString Destination { get; }

    /// <inheritdoc />
    public DiffResult Result { get; }

    /// <inheritdoc />
    public IEnumerable<DiffChar> Diff(IDiffAlgorithm algorithm)
    {
        return algorithm.Execute();
    }
}
