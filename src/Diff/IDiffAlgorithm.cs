namespace Diff;

public interface IDiffAlgorithm
{
    /// <summary>
    ///     Diff 를 수행합니다.
    /// </summary>
    /// <returns>Diff 결과를 순서대로 반환</returns>
    IEnumerable<DiffChar> Execute();
}
