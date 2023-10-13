namespace Diff;

public interface IDiffSet
{
    /// <summary>
    ///     Diff 원본 입니다.
    /// </summary>
    DiffString Source { get; }

    /// <summary>
    ///     Diff 대상 입니다.
    /// </summary>
    DiffString Destination { get; }

    /// <summary>
    ///     Diff 원본과 대상의 Diff 결과입니다.
    /// </summary>
    public DiffResult Result { get; }

    /// <summary>
    ///     Diff 를 수행합니다.
    /// </summary>
    /// <param name="algorithm">Diff 알고리즘 인스턴스</param>
    /// <returns>Diff 결과를 순서대로 반환</returns>
    IEnumerable<DiffChar> Diff(IDiffAlgorithm algorithm);
}
