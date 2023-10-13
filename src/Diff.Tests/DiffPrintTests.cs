using Xunit.Abstractions;

namespace Diff.Tests;

public class DiffPrintTests
{
    private readonly ITestOutputHelper _output;

    public DiffPrintTests(ITestOutputHelper output)
    {
        _output = output;
    }

    /// <summary>
    ///     코드의 출력을 검사하기 위한 확인용 테스트
    /// </summary>
    [Fact]
    public void DiffPrintTest()
    {
        // given 원본, 대상 문자열 설정
        using var writer = new TestOutputWriter(_output);
        const string srcString = "ABCDE";
        const string destString = "ACDEFG";
        _output.WriteLine(srcString);
        _output.WriteLine(destString);

        // when LCS 테이블 출력
        var diff = DiffAlgorithm.Create(srcString, destString);
        diff.Build(srcString, destString);
        diff.PrintLCS(writer);

        // then
        diff.PrintText(writer);
        Assert.True(true);
    }

    /// <summary>
    ///     <see cref="DiffSet.Diff" /> 메서드의 결과를 확인하기 위한 용도
    /// </summary>
    [Fact]
    public void RefactDiffPrint()
    {
        // given 원본, 대상 문자열 설정
        using var writer = new TestOutputWriter(_output);
        var source = (DiffString)"ABCDE";
        var dest = (DiffString)"ACDEFG";

        // when Diff 수행
        var diffSet = new DiffSet(source, dest);
        var algorithm = new LcsAlgorithm(diffSet);
        var diffChars = diffSet.Diff(algorithm);

        // then 결과 출력
        foreach (var c in diffChars)
        {
            writer.WriteLine($"{c.Result} {c}");
        }

        Assert.True(true);
    }
}
