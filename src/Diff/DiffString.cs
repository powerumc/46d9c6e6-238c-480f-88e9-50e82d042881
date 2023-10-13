namespace Diff;

/// <summary>
///     Diff 문자열 클래스 입니다.
/// </summary>
public record class DiffString
{
    /// <summary>
    ///     입력받은 문자열로 <see cref="DiffString" /> 인스턴스를 생성합니다.
    /// </summary>
    /// <param name="value">Diff 문자열</param>
    public DiffString(string value)
    {
        Value = value.Select(o => new DiffChar(o)).ToArray();
    }

    /// <summary>
    ///     Diff 문자열의 <see cref="DiffChar" /> 배열
    /// </summary>
    public DiffChar[] Value { get; }

    /// <summary>
    ///     Diff 문자열의 Diff 결과
    /// </summary>
    public DiffResult Result { get; }

    /// <summary>
    ///     Diff 문자열의 길이
    /// </summary>
    public int Length => Value.Length;

    public DiffChar this[int index] => Value[index];

    public static implicit operator string(DiffString str)
    {
        return string.Join("", str.Value.Select(o => o.Char).ToArray());
    }

    public static explicit operator DiffString(string str)
    {
        return new DiffString(str);
    }
}
