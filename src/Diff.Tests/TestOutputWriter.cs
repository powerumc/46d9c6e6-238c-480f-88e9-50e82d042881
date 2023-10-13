using System.Text;
using Xunit.Abstractions;

namespace Diff.Tests;

/// <summary>
///     <see cref="ITestOutputHelper" /> 출력을 <see cref="TextWriter" /> 를 리디렉트하는 클래스 입니다.
/// </summary>
internal class TestOutputWriter : TextWriter
{
    private readonly StringBuilder _buffer = new();
    private readonly char[] _newLineChars = Environment.NewLine.ToArray();
    private readonly ITestOutputHelper _output;

    public TestOutputWriter(ITestOutputHelper output)
    {
        _output = output;
    }

    public override Encoding Encoding => Encoding.UTF8;

    /// <summary>
    ///     개행 문자가 포함되면 _buffer 의 모든 문자열을 출력한 후 _buffer 를 비웁니다.
    /// </summary>
    /// <param name="value">char 문자</param>
    public override void Write(char value)
    {
        if (_newLineChars.Contains(value))
        {
            Flush();
        }

        base.Write(value);
    }

    /// <inheritdoc />
    public override void Write(string? value)
    {
        _buffer.Append(value);
    }

    public override void WriteLine(string? value)
    {
        _buffer.Append(value);
        Flush();
    }

    /// <inheritdoc />
    public override void Flush()
    {
        _output.WriteLine(_buffer.ToString());
        _buffer.Clear();
    }
}
