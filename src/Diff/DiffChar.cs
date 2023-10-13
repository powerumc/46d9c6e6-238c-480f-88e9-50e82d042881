namespace Diff;

/// <summary>
///     Diff 문자열
/// </summary>
public readonly struct DiffChar
{
    /// <summary>
    ///     Diff 결과
    /// </summary>
    public DiffResult Result { get; }

    /// <summary>
    ///     LCS 테이블의 값
    /// </summary>
    public int Value { get; }

    /// <summary>
    ///     Diff 문자열
    /// </summary>
    public char Char { get; }

    /// <summary>
    ///     <see cref="DiffChar" /> 구조체를 생성합니다.
    /// </summary>
    /// <param name="character"><see cref="char" /> 문자</param>
    /// <param name="result">Diff 결과, 기본값은 <see cref="DiffResult.None" /></param>
    /// <param name="value">LCS 테이블의 값. 기본값은 0</param>
    public DiffChar(char character, DiffResult result = DiffResult.None, int value = 0)
    {
        Result = result;
        Value = value;
        Char = character;
    }

    /// <summary>
    ///     LCS 테이블의 값을 업데이트 하고, <see cref="DiffChar" /> 를 반환합니다.
    /// </summary>
    /// <param name="addValue">추가할 값</param>
    /// <returns>업데이트 된 <see cref="DiffChar" /> 구조체</returns>
    public DiffChar IncreaseValue(int addValue = 1)
    {
        return new DiffChar(Char, Result, Value + addValue);
    }

    /// <summary>
    ///     Diff 문자를 업데이트 합니다.
    /// </summary>
    /// <param name="character"><see cref="char" /> 문자</param>
    /// <returns>업데이트 된 <see cref="DiffChar" /> 구조체</returns>
    public DiffChar Update(DiffChar character)
    {
        return new DiffChar(character.Char, Result, Value);
    }

    /// <summary>
    ///     Diff 결과를 업데이트 합니다.
    /// </summary>
    /// <param name="result">업데이트 할 Diff 결과</param>
    /// <returns>업데이트 된 <see cref="DiffChar" /> 구조체</returns>
    public DiffChar UpdateResult(DiffResult result)
    {
        return new DiffChar(Char, result, Value);
    }

    public override int GetHashCode()
    {
        return Char.GetHashCode();
    }

    public override bool Equals(object? obj)
    {
        if (obj is not DiffChar c)
        {
            return false;
        }

        return Char == c.Char;
    }

    public override string ToString()
    {
        return Char.ToString();
    }

    public static implicit operator char(DiffChar c)
    {
        return c.Char;
    }

    public static explicit operator DiffChar(char c)
    {
        return new DiffChar(c);
    }
}
