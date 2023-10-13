namespace Diff;

/// <summary>
///     Diff 처리 결과
/// </summary>
public enum DiffResult : sbyte
{
    /// <summary>
    ///     변경사항 없음
    /// </summary>
    None = 0,

    /// <summary>
    ///     삭제됨
    /// </summary>
    Deleted,

    /// <summary>
    ///     추가됨
    /// </summary>
    Inserted
}
